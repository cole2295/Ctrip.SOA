using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI;
using Ctrip.SOA.Infratructure.Logging;
using Ctrip.SOA.Infratructure.IOCFactory;
using Ctrip.SOA.Infratructure.Logging;
using Ctrip.SOA.Infratructure.Permission;
using Ctrip.SOA.Infratructure.Utility;
using Ctrip.SOA.Infratructure.Utility.IPAnalyzer.Interface;
using Ctrip.SOA.Infratructure.Utility.IPAnalyzer.Model;
using Ctrip.SOA.Infratructure.Extension;

namespace Ctrip.SOA.Infratructure.MVC
{
    public class BaseController : Controller
    {

        //protected readonly IUserService userService = ServiceProxyFactory.CreateChannel<IUserService>();
        //protected readonly IOrderService userService = ServiceProxyFactory.CreateChannel<IOrderService>();
        //protected readonly IProductService userService = ServiceProxyFactory.CreateChannel<IProductService>();

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding)
        {

            return new JsonNewResult { Data = data, ContentType = contentType, ContentEncoding = contentEncoding, FormateStr = "yyyy-MM-dd HH:mm:ss" };
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNewResult { Data = data, ContentType = contentType, ContentEncoding = contentEncoding, JsonRequestBehavior = behavior, FormateStr = "yyyy-MM-dd HH:mm:ss" };
        }

        /// <summary>
        /// 当前登录用户的ID
        /// </summary>
        protected string UID = string.Empty;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            
            //GetUserProfile(requestContext);

            base.Initialize(requestContext);

            //UID = requestContext.HttpContext.Request.Cookies["ticket_hhtravel"] != null ? requestContext.HttpContext.Request.Cookies["ticket_hhtravel"].Value : string.Empty;
        }    

        //protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        //{
        //    //GetUserProfile();
        //    base.Initialize();
        //}

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    string clientId = Arch.Framework.Utility.IPHelper.GetClientIP();
        //    SysLog.WriteTrace("IP", clientId);
        //    //白名单
        //    if (!string.IsNullOrEmpty(AppSetting.WhiteAccountsList))
        //    {
        //        string[] regctrl = AppSetting.WhiteAccountsList.Split('|');
        //        if (!HHUtility.InIPArray(clientId, regctrl))
        //        {
        //            RedirectToFailedPage(filterContext);
        //            return;
        //        }
        //    }

        //    //黑名单
        //    if (!string.IsNullOrEmpty(AppSetting.BlackAccountsList))
        //    {
        //        string[] regctrl = AppSetting.BlackAccountsList.Split('|');
        //        if (HHUtility.InIPArray(clientId, regctrl))
        //        {
        //            RedirectToFailedPage(filterContext);
        //            return;
        //        }
        //    }
        //    base.OnActionExecuting(filterContext);
        //}

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //SysLog.WriteTrace("HHTRAVEL - IP", Arch.Framework.Utility.IPHelper.GetClientIP());
            LogHelper.WriteLog("OnActionExecuting - ip", Arch.Framework.Utility.IPHelper.GetClientIP());
            //IPAnalyzerRegistHelper.RegistInst();

            //var ipAnalyzer = Factory.GetInst().Get<IIPAnalyzer>();

            //string clientIp = Arch.Framework.Utility.IPHelper.GetClientIP();
            //if (AppSetting.IsUseWhiteIPAccess)
            //{
            //    //白名单
            //    if (!string.IsNullOrEmpty(AppSetting.WhiteAccountsList))
            //    {
            //        if (!ipAnalyzer.DoAnalyze(clientIp, AppSetting.WhiteAccountsList))
            //        {
            //            RedirectToFailedPage(filterContext);
            //            return;
            //        }
            //    }
            //}
            //if (AppSetting.IsUseBlackIPAccess)
            //{
            //    //黑名单
            //    if (!string.IsNullOrEmpty(AppSetting.BlackAccountsList))
            //    {
            //        if (!ipAnalyzer.DoAnalyze(clientIp, AppSetting.BlackAccountsList))
            //        {
            //            RedirectToFailedPage(filterContext);
            //            return;
            //        }
            //    }
            //}
            base.OnActionExecuting(filterContext);
        }

        private void RedirectToFailedPage(ActionExecutingContext filterContext)
        {
            filterContext.Result = new RedirectResult(AppSetting.ValidateFailRedirectPath);
            return;
        }

        /// <summary>
        /// 设置用户信息
        /// </summary>
        private void GetUserProfile()
        {
#if DEBUG
            //开发环境绕开登录Yfeizhang
            UID = "wwwwww";
            Session["UID"] = UID;
            //UserInfo = new UserModel { UID = UID, IsQuickBooking = true, UserName = "long" };
            //Session["UserInfo"] = UserInfo;
            return;
#else

			//登录代理页需要到sso平台验证
            //if (IsAgent) {
            //    GetLoginUserInfo();
            //}
            //else {
            //    GetLoginUserInfoNew();
            //}
#endif
        }

        ///// <summary>
        ///// 是否开启白名单支付流程
        ///// </summary>
        ///// <returns></returns>
        //public static bool IsUseWhiteIPOrderWizard()
        //{
        //    bool isUseWhiteIPOrderWizard = false;
        //    //如果开启白名单预订流程，先判断白名单是否为空，非空的情况下再判断当前IP是否是白名单，如果是则走白名单新的预订流程，否则全部走现有的老流程。
        //    if (AppSetting.IsUseWhiteIPOrderWizard)
        //    {
        //        SysLog.WriteTrace("UseWhiteIPOrderWizard - IP", Arch.Framework.Utility.IPHelper.GetClientIP());
        //        IPAnalyzerRegistHelper.RegistInst();

        //        var ipAnalyzer = Factory.GetInst().Get<IIPAnalyzer>();

        //        string clientIp = Arch.Framework.Utility.IPHelper.GetClientIP();
        //        //白名单
        //        if (!string.IsNullOrEmpty(AppSetting.WhiteAccountsList))
        //        {
        //            if (!ipAnalyzer.DoAnalyze(clientIp, AppSetting.WhiteAccountsList))
        //            {
        //                isUseWhiteIPOrderWizard = false;
        //            }
        //            else
        //                isUseWhiteIPOrderWizard = true;
        //        }
        //    }

        public static bool IsInWhiteList()
        {
            //SysLog.WriteTrace("HHTRAVEL - IP", Arch.Framework.Utility.IPHelper.GetClientIP());
            LogHelper.WriteLog("IsInWhiteList - ip", Arch.Framework.Utility.IPHelper.GetClientIP());
            //IPAnalyzerRegistHelper.RegistInst();

            var ipAnalyzer = Factory.GetInst().Get<IIPAnalyzer>();

            string clientIp = Arch.Framework.Utility.IPHelper.GetClientIP();
            if (AppSetting.IsUseWhiteIPAccess)
            {
                //白名单
                if (!string.IsNullOrEmpty(AppSetting.WhiteAccountsList))
                {
                    if (!ipAnalyzer.DoAnalyze(clientIp, AppSetting.WhiteAccountsList))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}