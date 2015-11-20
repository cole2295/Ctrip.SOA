using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Diagnostics;

namespace HHInfratructure.Logging
{
    /// <summary>
    /// ctrip central log类
    /// </summary>
    public class HHLogHelperV2
    {
        /// <summary>
        /// CentralLogging 2.0 API
        /// </summary>
        //private static ILog logger = LogManager.GetLogger("HHTravel");

        /// <summary>
        /// Metric API
        /// </summary>
        //private static IMetric metricLogger = Freeway.Metrics.MetricManager.GetMetricLogger();

        /// <summary>
        /// 日志是否开启，在本类中，只对INFO及以下级别日志有效，ERROR和WARN级别，始终会记录
        /// </summary>
        private static bool LogEnable = ConfigurationManager.AppSettings["LogEnable"] == null ? true : ConfigurationManager.AppSettings["LogEnable"].Trim().ToLower() == "true";
        
        /// <summary>
        /// ws服务是否开启日志记录
        /// </summary>
        private static bool LogWebServiceEnable = ConfigurationManager.AppSettings["LogWebServiceEnable"] == null ? true : ConfigurationManager.AppSettings["LogWebServiceEnable"].Trim().ToLower() == "true";



        #region protected方法，通过这些方法直接调用Central Logging 2.0 API
        /// <summary>
        /// 记录一条Error级别的日志
        /// </summary>
        /// <param name="title">日志标题</param>
        /// <param name="ex">Exception类型</param>
        /// <param name="addInfo">日志的AddInfo信息</param>
        public static void WriteError(string title, Exception ex, Dictionary<string, string> addInfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 记录一条Error级别的日志
        /// </summary>
        /// <param name="title">日志标题</param>
        /// <param name="message">日志内容</param>
        /// <param name="addInfo">日志的AddInfo信息</param>
        public static void WriteInfo(string title, string message, Dictionary<string, string> addInfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 记录一条Wran级别的日志
        /// </summary>
        /// <param name="title">日志标题</param>
        /// <param name="mesasge">日志内容</param>
        /// <param name="addInfo">日志的AddInfo信息</param>
        public static void WriteWran(string title, string message, Dictionary<string, string> addInfo)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 记录一条Wran级别的日志
        /// </summary>
        /// <param name="title">日志标题</param>
        /// <param name="ex">Exception类型</param>
        /// <param name="addInfo">日志的AddInfo信息</param>
        private static void WriteWran(string title, Exception ex, Dictionary<string, string> addInfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取TracingErrorFlagValue，如果没有，返回一个New GUID
        /// </summary>
        /// <returns></returns>
        protected static string GetTracingErrorFlagValue()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 记录日志
        /// 此方法跟根据 GlobalAppLogType类型的不同，记录不同类型的日志
        /// </summary>
        /// <param name="logType">GlobalAppLogType</param>
        /// <param name="title">日志标题，此项不能为空</param>
        /// <param name="ex">日志相关的Exception</param>
        /// <param name="message">日志内容</param>
        /// <param name="addInfos">日志的AddInfos</param>
        protected static void Log(GlobalAppLogType logType, string title, Exception ex, string message, Dictionary<string, string> addInfos)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 记录一条Metric度量数据
        /// </summary>
        /// <param name="metricName">Metric名称</param>
        /// <param name="value">度量值</param>
        /// <param name="tags">自定义Tag</param>
        /// <param name="time">记录时间</param>
        protected static void MetricLog(string metricName, long value, Dictionary<string, string> tags, DateTime time)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region public方式，提供给应用程序调用
        /// <summary>
        /// 记录一条全局错误日志，一般在Global.asax或者全局try Catch中捕获的异常，使用此方法记录Exception
        /// </summary>
        /// <param name="appid">应用的APPID，可以在APPSetting中读取</param>
        /// <param name="ex">具体的Exception</param>
        public static void ERRORGlobalException(string appid, Exception ex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 记录一条全局错误日志，一般在Global.asax或者全局try Catch中捕获的异常，使用此方法记录Exception
        /// </summary>
        /// <param name="ex">具体的Exception</param>
        public static void ERRORGlobalException(Exception ex)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 记录一条普通Exception日志，一般方法内捕获的异常，使用此方法记录Exception
        /// </summary>
        /// <param name="appid">应用的APPID，可以在APPSetting中读取</param>
        /// <param name="subName">命名空间.类.方法名</param>
        /// <param name="ex">具体的Exception</param>
        public static void ERRORExecption(string appid, string subName, Exception ex)
        {
            Dictionary<string, string> addInfo = new Dictionary<string, string>();
            addInfo.Add("SubType", "ERRORFunction");
            addInfo.Add("SubName", subName);
            addInfo.Add("ErrorFlag", GetTracingErrorFlagValue());

            Log(GlobalAppLogType.ERRORGlobal, string.Format("{0}.Exception", appid), ex, null, addInfo);
        }

        /// <summary>
        /// 记录一条普通Exception日志，一般方法内捕获的异常，使用此方法记录Exception
        /// </summary>
        /// <param name="subName">命名空间.类.方法名</param>
        /// <param name="ex">具体的Exception</param>
        public static void ERRORExecption(string subName, Exception ex)
        {
            var appId = System.Configuration.ConfigurationManager.AppSettings["appId"];
            ERRORExecption(appId, subName, ex);
        }
        /// <summary>
        /// 记录一条普通Exception日志，一般方法内捕获的异常，使用此方法记录Exception
        /// </summary>
        /// <param name="ex">具体的Exception</param>
        public static void ERRORExecption(Exception ex)
        {
            var appId = System.Configuration.ConfigurationManager.AppSettings["appId"];
            var subName = GetCurrentMethodFullName(2);
            ERRORExecption(appId, subName, ex);
        }
        /// <summary>
        /// 记录一条普通Exception日志，一般方法内捕获的异常，使用此方法记录Exception
        /// </summary>
        /// <param name="appid">应用的APPID，可以在APPSetting中读取</param>
        /// <param name="subName">命名空间.类.方法名</param>
        /// <param name="ex">具体的Exception</param>
        internal static void ERRORExecption(string appid, string subName, Exception ex, Dictionary<string, string> addInfo)
        {
            addInfo.Add("SubType", "ERRORFunction");
            addInfo.Add("SubName", subName);
            addInfo.Add("ErrorFlag", GetTracingErrorFlagValue());

            Log(GlobalAppLogType.ERRORGlobal, string.Format("{0}.Exception", appid), ex, null, addInfo);
        }


        /// <summary>
        /// 记录一条逻辑错误，一般出现MyException时，或者明确知道为什么错误时使用此类型
        /// 例：用户参数输入非法，参数输入不正确，酒店满房等
        /// </summary>
        /// <param name="title">错误标题，格式：appid.EnglishException中的错误类型枚举或ID</param>
        /// <param name="subName">EnglishException中的错误ID</param>
        /// <param name="message">自定义错误消息</param>
        public static void ERRORMyException(string title, string subName, string message = null)
        {
            ERRORMyException(GlobalAppLogType.WARNUser, title, subName, null, message);
        }

        public static void ERRORMyException(string title, string subName, Exception exp, string message = null)
        {
            ERRORMyException(GlobalAppLogType.ERRORDefault, title, subName, exp, message);
        }

        public static void ERRORMyException(GlobalAppLogType logType, string title, string subName, Exception exp, string message = null)
        {
            Dictionary<string, string> addInfo = new Dictionary<string, string>();
            addInfo.Add("SubType", "WARNUser");
            addInfo.Add("SubName", subName);
            addInfo.Add("ErrorFlag", GetTracingErrorFlagValue());

            Log(logType, title, exp, message, addInfo);
        }

        /// <summary>
        /// 记录一条页面访问日志
        /// </summary>
        /// <param name="message">当前页地址（完整的URL）</param>
        /// <param name="subName">上一页的URL</param>
        public static void LOGWebSiteReferrer(string message, string referrerUrl)
        {
            Dictionary<string, string> addInfo = new Dictionary<string, string>();
            addInfo.Add("SubType", "INFOWebSiteReferrer");
            addInfo.Add("SubName", referrerUrl);

            Log(GlobalAppLogType.LOGWebSiteReferrer, "ReferrerUrl", null, message, addInfo);
        }

        /// <summary>
        /// 记录一条站点普通日志
        /// </summary>
        /// <param name="title">自定义日志标题，格式:appid.自定义标题</param>
        /// <param name="message"></param>
        /// <param name="subName">命名空间.类.方法名</param>
        public static void LOGWebSite(string title, string message, string fullMethodName)
        {
            Dictionary<string, string> addInfo = new Dictionary<string, string>();
            addInfo.Add("SubType", "INFOWebSite");
            addInfo.Add("SubName", fullMethodName);

            Log(GlobalAppLogType.LOGWebSite, title, null, message, addInfo);
        }
        /// <summary>
        /// 记录一条站点普通日志
        /// </summary>
        /// <param name="title">自定义日志标题，格式:appid.自定义标题</param>
        /// <param name="message"></param>
        /// <param name="subName">命名空间.类.方法名</param>
        internal static void LOGWebSite(string title, string message, string subName, Dictionary<string, string> addInfo)
        {
            addInfo.Add("SubType", "INFOWebSite");
            addInfo.Add("SubName", subName);

            Log(GlobalAppLogType.LOGWebSite, title, null, message, addInfo);
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

            Log(GlobalAppLogType.LOGCallServiceDetail, title, null, message.ToString(), addInfo);
        }

        /// <summary>
        /// 记录一条接口请求详细日志
        /// **注意！此方法只有在Cookie中CentralLogSwitchStatus有值，且为true时，才会起作用**
        /// </summary>
        /// <param name="title">[appid].请求的接口方法名称</param>
        /// <param name="message">RequestXML:+\n+请求报文+\n+\n+ResponseXML:+\n+返回报文</param>
        /// <param name="requestType">完整的RequestType.Name</param>
        /// <param name="status">接口返回类型</param>
        /// <param name="groupid">每一次接口调用（包含步骤和详细日志），请使用同一个GUID</param>
        public static void LOGCallServiceDetail(string title, string message, string requestType, CallServiceStatus status, string groupid)
        {
            Dictionary<string, string> addInfo = new Dictionary<string, string>();
            addInfo.Add("SubType", "INFOCallServiceDetail");
            addInfo.Add("SubName", requestType);
            addInfo.Add("Status", status.ToString());
            addInfo.Add("GroupID", groupid);
            if (status != CallServiceStatus.Success && status != CallServiceStatus.None)
            {
                string tracingErrorFlagValue = GetTracingErrorFlagValue();
                addInfo.Add("ErrorFlag", tracingErrorFlagValue);
                CookieManage.WriteTracingErrorFlag(tracingErrorFlagValue, 1);
            }

            Log(GlobalAppLogType.LOGCallServiceDetail, title, null, message, addInfo);
        }

        /// <summary>
        /// 记录访问量日志 
        /// 如需在Metric报表中查询，请使用MetricName = appid.pageName的形式，tag中type=visits
        /// </summary>
        /// <param name="appid">应用ID</param>
        /// <param name="pageName">页面或者Controller名称</param>
        /// <param name="action">当前页面的动作名称</param>
        /// <param name="value">度量值</param>
        public static void MetricVisits(string appid, string pageName, string action, long value)
        {
            Dictionary<string, string> tags = new Dictionary<string, string>();
            tags.Add("type", action);
            tags.Add("page", pageName);           

            MetricLog(string.Format("{0}.Visits", appid), value, tags, DateTime.Now);
        }

        /// <summary>
        /// 记录订单提交量日志
        /// 如需在Metric报表中查询，请使用MetricName = appid.appname的形式，tag中type=submitorder
        /// </summary>
        /// <param name="appid">应用ID</param>
        /// <param name="appName">应用名称</param>
        /// <param name="value">度量值</param>
        public static void MetricSubmitOrder(string appid, string appName, long value)
        {
            Dictionary<string, string> tags = new Dictionary<string, string>();
            tags.Add("page", appName);

            MetricLog(string.Format("{0}.submitorder", appid), value, tags, DateTime.Now);
        }

        /// <summary>
        /// 记录错误量
        /// 如需在Metric报表中查询，请使用MetricName = appid.pageName的形式，tag中type=Error
        /// </summary>
        /// <param name="appid">应用ID</param>
        /// <param name="pageName">页面或者Controller名称</param>
        /// <param name="errorType">错误类型</param>
        /// <param name="value">度量值</param>
        public static void MetricError(string appid, string pageName, string errorType, long value)
        {
            Dictionary<string, string> tags = new Dictionary<string, string>();
            tags.Add("page", pageName);
            tags.Add("type", errorType);

            MetricLog(string.Format("{0}.error", appid, pageName), value, tags, DateTime.Now);
        }
        /// <summary>
        /// 方法执行时间
        /// </summary>
        /// <param name="appid">应用ID</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="value">度量值</param>
        public static void MetricElapsed(string appid, string methodName, long value)
        {
            Dictionary<string, string> tags = new Dictionary<string, string>();
            tags.Add("appid", appid);
            tags.Add("methodName", methodName);
            MetricLog(string.Format("{0}.Elapsed", appid), value, tags, DateTime.Now);
        }

        /// <summary>
        /// 获取当前方法名
        /// </summary>
        /// <param name="depth">回溯深度</param>
        /// <returns>方法名</returns>
        public static string GetCurrentMethodFullName(int depth)
        {
            try
            {
                StackTrace st = new StackTrace();
                string methodName = st.GetFrame(depth).GetMethod().Name;
                string className = st.GetFrame(depth).GetMethod().DeclaringType.ToString();
                return string.Format("{0}.{1}", className, methodName);
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
    }


    #region 自定义数据类型
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum GlobalAppLogType
    {
        /// <summary>
        /// 全局错误日志，一般在Global.asax或者全局try Catch中捕获的异常，使用此类型
        /// </summary>
        ERRORGlobal,

        /// <summary>
        /// 默认错误日志，一般方法内捕获到错误，使用此类型
        /// </summary>
        ERRORDefault,

        /// <summary>
        /// 逻辑警告日志，明确知道为什么错误时使用此类型
        /// 例：用户参数输入非法，参数输入不正确，酒店满房等
        /// </summary>
        WARNUser,

        /// <summary>
        /// 页面访问日志，用于记录和访问信息时使用此类型
        /// </summary>
        LOGWebSiteReferrer,

        /// <summary>
        /// 正常Web站点日志，一般的日志类型
        /// </summary>
        LOGWebSite,

        /// <summary>
        /// 接口请求日志，用户记录访问接口过程中的日志类型
        /// </summary>
        LOGCallService,

        /// <summary>
        /// 接口请求详细日志，一般用于记录向请口发送的详细请求和应答信息
        /// 如Message：RequestXML:+\n+请求报文+\n+\n+ResponseXML:+\n+返回报文
        /// </summary>
        LOGCallServiceDetail
    }

    /// <summary>
    /// 调用接口结果
    /// </summary>
    public enum CallServiceStatus
    {
        /// <summary>
        /// 无结果
        /// </summary>
        None,

        /// <summary>
        /// 成功
        /// </summary>
        Success,

        /// <summary>
        /// 成功但没有数据
        /// </summary>
        SuccessNoData,

        /// <summary>
        /// 失败
        /// </summary>
        Fail,

        /// <summary>
        /// 错误
        /// </summary>
        Error,

        /// <summary>
        /// 重试
        /// </summary>
        Retry
    }

    #endregion
}
