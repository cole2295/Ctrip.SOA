using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using HHInfratructure.Data;
using HHInfratructure.Logging;

namespace HHInfratructure.Permission
{
    public class PermissionModule : IHttpModule
    {
        const string PERMISSION_MODULE = "PERMISSION_MODULE";
        const int DEFAULT_MINUTE = 1;
        private Page page;
        private HttpApplication application;

        static PermissionModule()
        {
            if (!CacheHelper.CacheIsExist(PERMISSION_MODULE))
            {
                SetCache();
            }
        }

        public void Init(HttpApplication httpApplication)
        {
            /*可扩展点：
             *url匹配模式:前端匹配、完全匹配、正则匹配
             *运维时增加url和按钮：当前页面下根据控件名来控制按钮的显示
             *控件与流程相关，可在页面中调用VisibleFunction方法
             *页面与按钮可采用树状设计，现在可采用页面与按钮冗余方式
             */
            this.application = httpApplication;
            application.AuthorizeRequest += new EventHandler(this.Application_AuthorizeRequest);
            this.application.PreRequestHandlerExecute += new EventHandler(OnPreRequestHandlerExecute);
        }

        public void Dispose()
        {
        }

        private void Application_AuthorizeRequest(Object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return;

            if (!this.ValidateUrl())
            {
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.AddHeader("Content-Type", "text/html; charset=gb2312");
                HttpContext.Current.Response.Charset = "gb2312";
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("gb2312"); ;
                HttpContext.Current.Response.Write(@"<html>
<head>
<meta http-equiv=""Content-Type"" content=""text/html; charset=gb2312"" />
</head>
<body>
没有操作权限!
</body>
</html>");
                HttpContext.Current.Response.End();
            }
        }

        private void OnPreRequestHandlerExecute(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return;

            this.page = this.application.Context.Handler as Page;
            if (this.page != null)
                this.page.PreRenderComplete += new EventHandler(PreRenderCompleteHandler);
        }

        private void PreRenderCompleteHandler(object sender, EventArgs e)
        {
            this.ValidateFunction();
        }

        private bool ValidateUrl()
        {
            //DataRow[] drUser = GetCache().Tables["Emp_User"]
            //    .Select(string.Format(@"UserId='{0}'", string.IsNullOrEmpty(AuthUser.GetCurrentUser().userID) ? "0" : AuthUser.GetCurrentUser().userID));
            //if (drUser.Length == 0) return false;

            //changed by Pluto Mei 2014-1-6
            //to avoid some spcific condition cause userId will not be integar and throw exceptions
            //improve program's robustness
            var userId = AuthUser.GetCurrentUser().userID;
            var intUserId = 0;
            int.TryParse(userId, out intUserId);

            if (GetCache().Tables["Emp_User"] == null)
            {
                //return false;
                SetCache();
            }

            DataRow[] drUser = GetCache().Tables["Emp_User"].Select(string.Format(@"UserId={0}", intUserId));
            if (drUser.Length == 0) return false;

            string requestUrl = HttpContext.Current.Request.Path;
            DataTable dtAllModule = GetCache().Tables["Sys_Module"];
            DataTable dtAllFunction = GetCache().Tables["Sys_Function"];
            bool existUrl = dtAllModule.Select(string.Format(@"ModuleUrl='{0}'", requestUrl)).Length == 0;
            existUrl &= dtAllFunction.Select(string.Format(@"FunctionUrl='{0}'", requestUrl)).Length == 0;
            if (existUrl) return true;

            //List<string> userUrlList = GetUserUrl(AuthUser.GetCurrentUser().userID);
            //if (userUrlList.Contains(requestUrl, StringComparer.OrdinalIgnoreCase)) return true;
            //return false;

            var userUrlList = GetUrlListByUserId(intUserId);
            return userUrlList.Contains(requestUrl, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// find user url list by module list of one's permission
        /// </summary>
        private static IEnumerable<string> GetUrlListByUserId(int intUserId)
        {
            var roleId = 0;
            DataRow[] userRows = GetCache().Tables["Emp_User"].Select(string.Format(@"UserId='{0}'", intUserId));
            if (userRows.Length > 0)
            {
                int groupId = Convert.ToInt32(userRows[0]["GroupID"]);
                DataRow[] roleRows = GetCache().Tables["Admin_Role_Group"].Select(string.Format("GroupID={0}", groupId));
                foreach (DataRow roleItem in roleRows)
                {
                    roleId = Convert.ToInt32(roleItem["RoleId"]);
                }
            }
            var urlList = new List<string>();
            var Sqls = new StringBuilder();
            Sqls.Append("SELECT Url FROM Admin_Permission WITH(NOLOCK) WHERE PermissionID in (");
            Sqls.AppendFormat("SELECT permissionid FROM Admin_RolePermissionRel WITH(NOLOCK) WHERE ROLEID={0}) and permissiontype=4", roleId);
            try
            {
                var ds = new CacheSelectDB().GetDataSet(Sqls.ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var url = ds.Tables[0].Rows[i]["Url"].ToString().Trim();
                    urlList.Add(url);
                }
            }
            catch (Exception ex)
            {
                //Logging.SysLog.WriteException("Common.Framework.GetUrlListByUserId", ex);
                HHLogHelperV2.ERRORExecption("Common.Framework.GetUrlListByUserId", ex);
            }
            return urlList;
        }

        private void ValidateFunction()
        {
            string requestUrl = HttpContext.Current.Request.Path;
            DataRow[] moduleRows = GetCache().Tables["Sys_Module"].Select(string.Format(@"ModuleUrl='{0}'", requestUrl));
            if (moduleRows.Length > 0)
            {
                int moduleId = Convert.ToInt32(moduleRows[0]["ModuleId"]);
                DataView dvFunction = new DataView(GetCache().Tables["Sys_Function"]);
                dvFunction.RowFilter = string.Format("ModuleId={0}", moduleId);
                if (dvFunction.Count > 0)
                {
                    ControlCollection cc = this.page.Form.Controls;
                    if (cc.Count > 0)
                    {
                        this.ValidateFunction(cc);
                    }
                }
            }
        }

        private void ValidateFunction(ControlCollection cc)
        {
            foreach (Control ct in cc)
            {
                //标记btnSubmit_FID_1004010101
                const string flag = "_FID_";
                string regTxt = @"^\w+_FID_(\d){10}\w*$";
                Regex reg = new Regex(regTxt);
                if (!string.IsNullOrEmpty(ct.ID) && reg.Match(ct.ID).Success)
                {
                    int startIndex = ct.ID.IndexOf(flag);
                    int functionId = int.Parse(ct.ID.Substring(startIndex + flag.Length, 10));
                    ct.Visible = VisibleFunction(functionId, AuthUser.GetCurrentUser().userID);
                }
                if (ct.Controls.Count > 0)
                {
                    ValidateFunction(ct.Controls);
                }
            }
        }

        /// <summary>
        /// 是否显示功能按钮
        /// </summary>
        /// <param name="functionId">功能编号</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public static bool VisibleFunction(int functionId, string userId)
        {
            DataTable dtAllFunction = GetCache().Tables["Sys_Function"];
            if (dtAllFunction.Select(string.Format("FunctionID={0}", functionId)).Length == 0)
            {
                return true;
            }
            List<int> userFunctionIdList = GetUserPermission("Function", userId);
            if (userFunctionIdList.Contains(functionId))
            {
                return true;
            }
            return false;
        }

        public static bool ValidateUrl(string requestUrl, string userId)
        {
            var intUserId = 0;
            int.TryParse(userId, out intUserId);
            DataRow[] drUser = GetCache().Tables["Emp_User"].Select(string.Format(@"UserId='{0}'", userId));
            if (drUser.Length == 0) return false;

            DataTable dtAllModule = GetCache().Tables["Sys_Module"];
            DataTable dtAllFunction = GetCache().Tables["Sys_Function"];
            bool existUrl = dtAllModule.Select(string.Format(@"ModuleUrl='{0}'", requestUrl)).Length == 0;
            existUrl &= dtAllFunction.Select(string.Format(@"FunctionUrl='{0}'", requestUrl)).Length == 0;
            if (existUrl) return true;

            //List<string> userUrlList = GetUserUrl(userId);
            //if (userUrlList.Contains(requestUrl, StringComparer.OrdinalIgnoreCase)) return true;
            //return false;

            var userUrlList = GetUrlListByUserId(intUserId);
            return userUrlList.Contains(requestUrl, StringComparer.OrdinalIgnoreCase);
        }

        private static List<string> GetUserUrl(string userId)
        {
            List<string> urlList = new List<string>();
            List<int> userModuleIdList = GetUserPermission("Module", userId);
            foreach (int moduleId in userModuleIdList)
            {
                DataRow[] moduleRows = GetCache().Tables["Sys_Module"].Select(string.Format(@"ModuleID={0}", moduleId));
                if (moduleRows.Length > 0)
                {
                    string moduleUrl = moduleRows[0]["ModuleUrl"].ToString();
                    if (!urlList.Contains(moduleUrl, StringComparer.OrdinalIgnoreCase))
                    {
                        urlList.Add(moduleUrl);
                    }
                }
            }
            List<int> userFunctionIdList = GetUserPermission("Function", userId);
            foreach (int functionId in userFunctionIdList)
            {
                DataRow[] functionRows = GetCache().Tables["Sys_Function"].Select(string.Format(@"FunctionId={0}", functionId));
                if (functionRows.Length > 0)
                {
                    string functionUrl = functionRows[0]["FunctionUrl"].ToString();
                    if (!urlList.Contains(functionUrl, StringComparer.OrdinalIgnoreCase))
                    {
                        urlList.Add(functionUrl);
                    }
                }
            }
            return urlList;
        }

        private static List<int> GetUserPermission(string resourceType, string userId)
        {
            List<int> idList = new List<int>();
            DataRow[] userRows = GetCache().Tables["Emp_User"].Select(string.Format(@"UserId='{0}'", userId));
            if (userRows.Length > 0)
            {
                int groupId = Convert.ToInt32(userRows[0]["GroupID"]);
                DataRow[] roleRows = GetCache().Tables["Admin_Role_Group"].Select(string.Format("GroupID={0}", groupId));
                foreach (DataRow roleItem in roleRows)
                {
                    int roleId = Convert.ToInt32(roleItem["RoleId"]);

                    DataRow[] resourceRows = GetCache().Tables["Admin_Role_Permission"].Select(string.Format("RoleId={0} AND ResourceType='{1}'", roleId, resourceType));
                    foreach (DataRow resourceItem in resourceRows)
                    {
                        int resoureId = Convert.ToInt32(resourceItem["ResourceID"]);
                        if (!idList.Contains(resoureId))
                        {
                            idList.Add(resoureId);
                        }
                    }
                }
            }
            return idList;
        }

        private static void SetCache()
        {
            /*去掉Emp_User表方案:
             * Session["GroupID"];
             * HttpContext.Current.User.Identity.Name=UserId|GroupId;
             * EmpUserDAL.Select(userId)，取一次即可;
             */
            string permissionSql = @"SELECT UserID,GroupID FROM Emp_User;
SELECT RoleID, GroupID FROM Admin_Role_Group;
SELECT RoleID, ResourceType, ResourceID FROM Admin_Role_Permission WHERE ResourceType='Module' OR ResourceType='Function'
select PermissionID as ModuleId, Url as ModuleUrl from HHGovDB..Admin_Permission
SELECT FunctionId, ModuleID, FunctionUrl FROM Sys_Function;";
            string permissionTbls = @"Emp_User;Admin_Role_Group;Admin_Role_Permission;Sys_Module;Sys_Function;";
            CacheHelper.CreateCache(PERMISSION_MODULE, DBConsts.HHGovDB_SELECT, permissionSql, permissionTbls, TimeInterval.Minute, DEFAULT_MINUTE);
        }

        private static DataSet GetCache()
        {
            DataSet ds = CacheHelper.GetCache(PERMISSION_MODULE);
            ds.CaseSensitive = false;
            return ds;
        }
    }
}