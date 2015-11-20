using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using HHInfratructure.Utility;
using System.IO;

namespace HHInfratructure.Logging
{
    /// <summary>
    /// SysLog旧版兼容，起到HHLogHelperV2适配器Adapter作用
    /// </summary>
    [Obsolete("建议使用HHLogHelperV2")]
    public class SysLog
    {
        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="e">异常</param>
        public static void WriteException(Exception e)
        {
            WriteException(null, e, null, null);
        }

        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="e">异常</param>
        public static void WriteException(string title, Exception e)
        {
            WriteException(title, e, null, null);
        }

        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="e">异常</param>
        /// <param name="remark">备注</param>
        public static void WriteException(string title, Exception e, string remark)
        {
            WriteException(title, e, null, remark);
        }

        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="e">异常</param>
        /// <param name="orderid">订单ID</param>
        /// <param name="remark">备注</param>
        public static void WriteException(string title, Exception e, string orderid, string remark)
        {
            var appId = AppSetting.AppID.ToString();

            Dictionary<string, string> addInfo = new Dictionary<string, string>();
            addInfo.Add("orderid", orderid);
            addInfo.Add("remark", remark);

            HHLogHelperV2.ERRORExecption(appId, "HHLogHelperV1_WriteException", e, addInfo);

            //WriteLocalLog(title, e.ToString());
        }

        ///// <summary>
        ///// 记录跟踪信息
        ///// </summary>
        ///// <param name="message">跟踪信息</param>
        //public static void WriteTrace(string message)
        //{
        //    WriteTrace(null, message, null, null);
        //}

        /// <summary>
        /// 记录跟踪信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">跟踪信息</param>
        public static void WriteTrace(string title, string message)
        {
            WriteTrace(title, message, null, null);
        }

        /// <summary>
        /// 记录跟踪信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">跟踪信息</param>
        /// <param name="remark">备注</param>
        public static void WriteTrace(string title, string message, string remark)
        {
            WriteTrace(title, message, null, remark);
        }

        public static void WriteLocalLog(string title, string message) 
        {
            string path = @"D:\Log\400305\Log.txt";
            if (!File.Exists(path)) {
                using (File.CreateText(path)) { }
            }            

            using (FileStream fs = File.Open(path, FileMode.Append, FileAccess.Write)) {
                using (StreamWriter sw = new StreamWriter(fs)) {
                    sw.Write(title + " " + message + Environment.NewLine);
                }
            }
        }

        /// <summary>
        /// 记录跟踪信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">跟踪信息</param>
        /// <param name="orderid">订单ID</param>
        /// <param name="remark">备注</param>
        public static void WriteTrace(string title, string message, string orderid, string remark)
        {
            var appId = AppSetting.AppID;
            Dictionary<string, string> addInfo = new Dictionary<string, string>();

            addInfo.Add("orderid", orderid);
            addInfo.Add("remark", remark);
            HHLogHelperV2.LOGWebSite(title, message, "HHLogHelperV1_WriteTrace", addInfo);

            //WriteLocalLog(title, message);
        }

        /// <summary>
        /// 记录业务日志
        /// </summary>
        /// <param name="resourceType">资源类型,订单,产品,客户,供应商,员工,其他</param>
        /// <param name="resourceID">资源ID</param>
        /// <param name="actionType">操作类型,登录|新增...|修改...|删除...</param>
        /// <param name="actionDetail">操作内容</param>
        /// <param name="empID"></param>
        public static void WriteBizLog(string resourceType, string resourceID, string actionType, string actionDetail)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 记录用户行为日志
        /// </summary>
        /// <param name="actionType">行为类型</param>
        /// <param name="actionDetail">行为描述</param>
        /// <param name="actionResult">行为结果</param>
        public static void WriteUserLog(string actionType, string actionDetail, string actionResult)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 记录事务日志
        /// 需要为对象的以下属性赋值：
        /// 1.LogType：0-Page,1-WS,2-DB,3-Other
        /// 2.ToApp:XXX-DB,XXX-Page,XXX-WS
        /// 3.Title:,,RequestType
        /// 4.Request:SQL,URL,Request
        /// 5.Response:RecordSet.Count,,Response
        /// 6.RequestTime:发出请求时间
        /// 7.ResponseTime:收到响应时间
        /// 8.Result:Success,Fail,Exception
        /// 9.ResultMsg:允许为空
        /// 10.OrderID:允许为空
        /// 11.ServerIP:允许为空
        /// 12.ServerName:DBServer,WebServer,WSServer
        /// </summary>
        /// <param name="log">事务日志对象</param>
        public static void WriteTransLog()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 记录计数器 - 应用程序的一些重要的指标，比如一段时间内某一事件发生次数
        /// </summary>
        /// <param name="ctr"></param>
        public static void WriteCounter()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 取得当前的会话ID
        /// </summary>
        /// <returns></returns>
        private static string GetSessionID()
        {
            try
            {
                if (HttpContext.Current.Session != null)
                    return HttpContext.Current.Session.SessionID;
            }
            catch
            {
                return "";
            }
            return "";
        }

        /// <summary>
        /// 取得当前的员工名
        /// </summary>
        /// <returns></returns>
        private static string GetUserName()
        {
            try
            {
                if (HttpContext.Current.Request.Cookies["userName"] != null)
                    return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies["userName"].Value);
            }
            catch
            {
                return "";
            }
            return "";
        }

        /// <summary>
        /// 取得当前的用户名
        /// </summary>
        /// <returns></returns>
        private static string GetCustomerName()
        {
            try
            {
                if (HttpContext.Current.Request.Cookies["CustomerName"] != null)
                    return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies["CustomerName"].Value);
            }
            catch
            {
                return "";
            }
            return "";
        }
    }
}