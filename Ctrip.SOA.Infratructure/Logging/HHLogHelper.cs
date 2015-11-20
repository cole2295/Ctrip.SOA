using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using HHInfratructure.Utility;

namespace HHInfratructure.Logging
{
    [Obsolete("建议使用HHLogHelperV2")]
    class HHLogHelper
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
            //try
            //{
                //LogSvcRef.AppLogEntity log = new LogSvcRef.AppLogEntity();
                //log.LogApp = AppSetting.AppID;
                //log.LogDate = DateTime.Now;
                //log.LogTime = DateTime.Now;
                //log.LogType = true;
                //log.ClientIP = Arch.Framework.Utility.IPHelper.GetClientIP();
                //log.ServerIP = log.ClientIP;
                //log.ServerName = log.ClientIP;
                //log.SessionID = GetSessionID();

                //log.Title = title;
                //log.Message = e.ToString();
                //log.OrderID = orderid;
                //log.Remark = remark;

                //LogSvcRef.LogSvcClient client = new LogSvcRef.LogSvcClient();
                //client.SendAppLog(log);
            //}
            //catch (Exception ee)
            //{
            //    //FileLog.WriteLocalLog("WriteExceptionFail", string.Format("Title:{0}；OrderID:{1}；Remark:{2}\r\nException:{3}\r\nFail:{4}", title, orderid, remark, e.ToString(), ee.ToString()));
            //}
        }

        /// <summary>
        /// 记录跟踪信息
        /// </summary>
        /// <param name="message">跟踪信息</param>
        public static void WriteTrace(string message)
        {
            WriteTrace(null, message, null, null);
        }

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

        /// <summary>
        /// 记录跟踪信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">跟踪信息</param>
        /// <param name="orderid">订单ID</param>
        /// <param name="remark">备注</param>
        public static void WriteTrace(string title, string message, string orderid, string remark)
        {
            //try
            //{
                //LogSvcRef.AppLogEntity log = new LogSvcRef.AppLogEntity();
                //log.LogApp = AppSetting.AppID;
                //log.LogDate = DateTime.Now;
                //log.LogTime = DateTime.Now;
                //log.LogType = false;
                //log.ClientIP = Arch.Framework.Utility.IPHelper.GetClientIP();
                //log.ServerIP = log.ClientIP;
                //log.ServerName = log.ClientIP;
                //log.SessionID = GetSessionID();

                //log.Title = title;
                //log.Message = message;
                //log.OrderID = orderid;
                //log.Remark = remark;

                //LogSvcRef.LogSvcClient client = new LogSvcRef.LogSvcClient();
                //client.SendAppLog(log);
            //}
            //catch (Exception ee)
            //{
            //    FileLog.WriteLocalLog("WriteTraceFail", string.Format("Title:{0}；OrderID:{1}；Remark:{2}\r\nMessage:{3}\r\nFail:{4}", title, orderid, remark, message, ee.ToString()));
            //}
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
            //try
            //{
                //LogSvcRef.BizLogEntity log = new LogSvcRef.BizLogEntity();
                //log.LogApp = AppSetting.AppID;
                //log.LogDate = DateTime.Now;
                //log.LogTime = DateTime.Now;
                //log.LogUrl = HttpContext.Current.Request.Url.ToString();
                //log.ClientIP = Arch.Framework.Utility.IPHelper.GetClientIP();
                //log.ServerIP = log.ClientIP;
                //log.ServerName = log.ClientIP;

                //log.ResourceType = resourceType;
                //log.ResourceID = resourceID;
                //log.ActionType = actionType;
                //log.ActionDetail = actionDetail;
                //log.UserName = GetUserName();

                //LogSvcRef.LogSvcClient client = new LogSvcRef.LogSvcClient();
                //client.SendBizLog(log);
            //}
            //catch (Exception ee)
            //{
            //    FileLog.WriteLocalLog("WriteBizLogFail", string.Format("resourceType:{0}；resourceID:{1}；actionType:{2}\r\nactionDetail:{3}\r\nFail:{4}", resourceType, resourceID, actionType, actionDetail, ee.ToString()));
            //}
        }

        /// <summary>
        /// 记录用户行为日志
        /// </summary>
        /// <param name="actionType">行为类型</param>
        /// <param name="actionDetail">行为描述</param>
        /// <param name="actionResult">行为结果</param>
        public static void WriteUserLog(string actionType, string actionDetail, string actionResult)
        {
            //try
            //{
                //LogSvcRef.UserLogEntity log = new LogSvcRef.UserLogEntity();
                //log.LogApp = AppSetting.AppID;
                //log.LogDate = DateTime.Now;
                //log.LogTime = DateTime.Now;
                //log.LogUrl = HttpContext.Current.Request.Url.ToString();
                //log.ClientIP = Arch.Framework.Utility.IPHelper.GetClientIP();
                //log.ServerIP = log.ClientIP;
                //log.ServerName = log.ClientIP;
                //log.ClientName = HttpContext.Current.Request.UserAgent;
                //log.SessionID = GetSessionID();

                //log.ActionType = actionType;
                //log.ActionDetail = actionDetail;
                //log.ActionResult = actionResult;
                //log.CustomerName = GetCustomerName();

                //LogSvcRef.LogSvcClient client = new LogSvcRef.LogSvcClient();
                //client.SendUserLog(log);
            //}
            //catch (Exception ee)
            //{
            //    FileLog.WriteLocalLog("WriteUserLogFail", string.Format("actionType:{0}；actionDetail:{1}；\r\nFail:{2}", actionType, actionDetail, ee.ToString()));
            //}
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
            //try
            //{
                //log.FromApp = AppSetting.AppID;
                //log.LogDate = DateTime.Now;
                //log.LogTime = DateTime.Now;
                //log.SessionID = GetSessionID();
                //log.ClientIP = Arch.Framework.Utility.IPHelper.GetClientIP();
                //log.ClientServer = log.ClientIP;

                //TimeSpan ts = log.ResponseTime - log.RequestTime;
                //log.ProcTimeLength = (int)ts.TotalMilliseconds;

                //LogSvcRef.LogSvcClient client = new LogSvcRef.LogSvcClient();
                //client.SendTransLog(log);
            //}
            //catch (Exception ee)
            //{
            //    FileLog.WriteLocalLog("WriteTransLog", string.Format("Request:{0}；Response:{1}；\r\nFail:{2}", log.Request, log.Response, ee.ToString()));
            //}
        }

        /// <summary>
        /// 记录计数器 - 应用程序的一些重要的指标，比如一段时间内某一事件发生次数
        /// </summary>
        /// <param name="ctr"></param>
        public static void WriteCounter()
        {
            //try
            //{
                //counter.CounterApp = AppSetting.AppID;
                //counter.CounterDate = DateTime.Now;
                //counter.CounterTime = DateTime.Now;
                //counter.ServerIP = Arch.Framework.Utility.IPHelper.GetClientIP();
                //counter.ServerName = counter.ServerIP;

                //LogSvcRef.LogSvcClient client = new LogSvcRef.LogSvcClient();
                //client.SendCounter(counter);
            //}
            //catch (Exception ee)
            //{
            //    FileLog.WriteLocalLog("WriteCounter", ee.ToString());
            //}
        }

        /// <summary>
        /// 取得当前的会话ID
        /// </summary>
        /// <returns></returns>
        public static string GetSessionID()
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
        public static string GetUserName()
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
        public static string GetCustomerName()
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

        /// <summary>
        /// 此方法为SOA代理类专用，用于记录SOA请求过程中关键步骤的时长日志。
        /// </summary>
        /// <param name="title">[appid].请求的接口方法名称</param>
        /// <param name="requestType">完整的RequestType</param>
        /// <param name="status">接口返回类型</param>
        /// <param name="startTime">发起请求的时间，请使用DateTime.Now.Ticks获取</param>
        /// <param name="requestXMLSerializeEndTime">requestXML序列话完毕时间，请使用DateTime.Now.Ticks获取</param>
        /// <param name="requestEndTime">SOA响应完毕时间，请使用DateTime.Now.Ticks获取</param>
        /// <param name="responseDeSerializeEndTime">responseXML序列话完毕时间，请使用DateTime.Now.Ticks获取</param>
        /// <param name="endTime">请求完毕时间，请使用DateTime.Now.Ticks获取</param>
        /// <param name="groupid">每一次接口调用（包含步骤和详细日志），请使用同一个GUID</param>
        public static void LOGCallService(string title, string requestType, CallServiceStatus status, long startTime, long requestXMLSerializeEndTime, long requestEndTime, long responseDeSerializeEndTime, long endTime, string groupid)
        {
            Dictionary<string, string> addInfo = new Dictionary<string, string>();
            addInfo.Add("SubType", "INFOCallService");
            addInfo.Add("SubName", requestType);
            addInfo.Add("Status", status.ToString());
            addInfo.Add("GroupID", groupid);

            // 记录接口请求步骤中各关键点的响应时长
            StringBuilder message = new StringBuilder();
            message.Append("请求发起时间:").Append((new DateTime(startTime)).ToString("HH:mm:ss.fff"))
                .Append("RequestXML序列化完毕时间: +").Append((requestXMLSerializeEndTime - startTime) / 10000).Append("毫秒")
                .Append("服务器应答完毕时间: +").Append((requestEndTime - startTime) / 10000).Append("毫秒")
                .Append("ResponseXML序列化完毕时间: +").Append((responseDeSerializeEndTime - startTime) / 10000).Append("毫秒")
                .Append("请求结束时间: +").Append((endTime - startTime) / 10000).Append("毫秒");

            message.Append("\n\n时间源记录:\n")
                .Append(startTime)
                .Append("|").Append(requestXMLSerializeEndTime)
                .Append("|").Append(requestEndTime)
                .Append("|").Append(responseDeSerializeEndTime)
                .Append("|").Append(endTime);

            WriteTrace(title, message.ToString());

            //Log(GlobalAppLogType.LOGCallServiceDetail, title, null, message.ToString(), addInfo);
        }
    }
}
