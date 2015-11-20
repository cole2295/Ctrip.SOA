using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Ctrip.SOA.Infratructure
{
    public class CookieManage
    {
        //跟踪用户信息标识
        private static readonly string TracingUserFlag = "TracingUserFlag";
        //跟踪程序异常标识
        private static readonly string TracingErrorFlag = "TracingErrorFlag";
        //用户信息标识目标
        private static readonly string CentralLogSwitchStatus = "CentralLogSwitchStatus";
        //域名
        private static readonly string CookieDomain = "hhtravel.com";

        public static string GetCentralLogStatus()
        {
            if (HttpContext.Current == null)
                return string.Empty;
            return HttpContext.Current.Request.Cookies[CentralLogSwitchStatus] == null ? "" : HttpContext.Current.Request.Cookies[CentralLogSwitchStatus].Value;
        }

        public static void SetCentralLogStatus(out string value)
        {
            if (HttpContext.Current == null)
            {
                value = "";
                return;
            }
            if (HttpContext.Current.Request.Cookies[CentralLogSwitchStatus] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[CentralLogSwitchStatus].Value))
            {
                HttpCookie cookie = new HttpCookie(CentralLogSwitchStatus);
                value = cookie.Value = "true";
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Domain = CookieDomain;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else
            {
                //HttpContext.Current.Response.Cookies[TargetTracingUserFlag].Value = value;
                HttpContext.Current.Response.Cookies[CentralLogSwitchStatus].Expires = DateTime.Now.AddDays(1);
                //HttpContext.Current.Response.Cookies[TargetTracingUserFlag].Domain = CookieDomain;
                value = HttpContext.Current.Request.Cookies[CentralLogSwitchStatus].Value;
            }
        }

        /// <summary>
        /// 获得TracingUserFlag的cookie值，如果有就直接取出，否则新增。
        /// </summary>
        public static string GetTracingUserFlag()
        {
       
           
            if (HttpContext.Current == null)
            {
                return "";
            }
            if (HttpContext.Current.Request.Cookies[TracingUserFlag] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[TracingUserFlag].Value))
            {
                if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                {
                    HttpCookie cookie = new HttpCookie(TracingUserFlag);
                    cookie.Value = GenerateStringID();
                    cookie.Expires = DateTime.Now.AddDays(1);
                    cookie.Domain = CookieDomain;

                    HttpContext.Current.Response.Cookies.Add(cookie);
                    return cookie.Value;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                //HttpContext.Current.Response.Cookies[TracingErrorFlag].Value = value;
                HttpContext.Current.Response.Cookies[TracingUserFlag].Expires = DateTime.Now.AddDays(1);
                //HttpContext.Current.Response.Cookies[TracingErrorFlag].Domain = CookieDomain;
                return HttpContext.Current.Request.Cookies[TracingUserFlag].Value;
            }
        }
        /// <summary>
        /// 获得TracingErrorFlag的cookie值，如果有就直接取出，否则新增。
        /// </summary>
        public static string GetTracingErrorFlag()
        {
            if (HttpContext.Current == null)
                return "";

            if (HttpContext.Current.Request.Cookies[TracingErrorFlag] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[TracingErrorFlag].Value))
            {
                HttpCookie cookie = new HttpCookie(TracingErrorFlag);
                cookie.Value = GenerateStringID();
                cookie.Expires = DateTime.Now.AddDays(1);
                cookie.Domain = CookieDomain;

                HttpContext.Current.Response.Cookies.Add(cookie);
                return cookie.Value;
            }
            else
            {
                //HttpContext.Current.Response.Cookies[TracingErrorFlag].Value = value;
                HttpContext.Current.Response.Cookies[TracingErrorFlag].Expires = DateTime.Now.AddDays(1);
                //HttpContext.Current.Response.Cookies[TracingErrorFlag].Domain = CookieDomain;
                return HttpContext.Current.Request.Cookies[TracingErrorFlag].Value;
            }
        }

        /// <summary>
        /// 获取Cookie中的ErrorFlag值
        /// </summary>
        /// <returns></returns>
        public static string GetTracingErrorFlagNotAdd()
        {
            string returnResult = string.Empty;
            if (HttpContext.Current != null
                && HttpContext.Current.Request.Cookies[TracingErrorFlag] != null
                && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[TracingErrorFlag].Value)
                )
            {
                returnResult = HttpContext.Current.Request.Cookies[TracingErrorFlag].Value;
            }

            return returnResult;
        }

        /// <summary>
        /// 写入ErrorFlagCookie
        /// </summary>
        /// <returns></returns>
        public static void WriteTracingErrorFlag(string value, double day)
        {
            if (
                HttpContext.Current != null
                && (HttpContext.Current.Request.Cookies[TracingErrorFlag] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[TracingErrorFlag].Value))
                )
            {
                HttpCookie cookie = new HttpCookie(TracingErrorFlag);
                cookie.Value = value;
                cookie.Expires = DateTime.Now.AddDays(day);
                cookie.Domain = CookieDomain;

                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }


        /// <summary>
        /// 清除TracingUserFlag的cookie
        /// </summary>
        public static void ClearTracingUserFlag()
        {
            if (HttpContext.Current == null)
                return;
            if (HttpContext.Current.Request.Cookies[TracingUserFlag] != null)
            {
                HttpCookie cookie = new System.Web.HttpCookie(TracingUserFlag);
                cookie.Value = "";
                cookie.Domain = CookieDomain;
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// 清除TracingErrorFlag的cookie
        /// </summary>
        public static void ClearTracingErrorFlag()
        {
            if (HttpContext.Current == null)
                return;
            if (HttpContext.Current.Request.Cookies[TracingErrorFlag] != null)
            {
                HttpCookie cookie = new System.Web.HttpCookie(TracingErrorFlag);
                cookie.Value = "";
                cookie.Domain = CookieDomain;
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// 清除TargetTracingUserFlag的cookie
        /// </summary>
        public static void ClearCentralLogStatus()
        {
            if (HttpContext.Current == null)
                return;
            if (HttpContext.Current.Request.Cookies[CentralLogSwitchStatus] != null)
            {
                HttpCookie cookie = new System.Web.HttpCookie(CentralLogSwitchStatus);
                cookie.Value = "";
                cookie.Domain = CookieDomain;
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// 生成短号唯一标识
        /// </summary>
        /// <returns></returns>
        private static string GenerateStringID()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

    }
}
