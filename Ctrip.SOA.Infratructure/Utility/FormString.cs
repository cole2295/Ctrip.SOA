using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Ctrip.SOA.Infratructure.Utility
{
    public class FormString
    {
        /// <summary>
        /// 接收字符串并转为Int型
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static int intsafeq(string key)
        {
            return intsafeq(key, 0);
        }

        /// <summary>
        /// 接收字符串并转为short型
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static short shortsafeq(string key)
        {
            return shortsafeq(key, 0);
        }

        /// <summary>
        /// 接收字符串并转为Int型
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static int intsafeq(string key, int defaultvale)
        {
            return StringUtils.SafeInt((HttpContext.Current.Request.Form[key]), defaultvale);
        }

        /// <summary>
        /// 接收字符串并转为short型
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static short shortsafeq(string key, short defaultvale)
        {
            return StringUtils.SafeShort((HttpContext.Current.Request.Form[key]), defaultvale);
        }

        /// <summary>
        /// 接收字符串转为String
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static string safeq(string key)
        {
            return safeq(key, 0, 100000);
        }

        /// <summary>
        /// 接收字符串转为String
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string safeq(string key, int len)
        {
            return safeq(key, 0, len);
        }

        /// <summary>
        /// 接收字符串转为String
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="type">是否转为小写 1转为小写</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string safeq(string key, int type, int len)
        {
            string obj = StringUtils.SafeStr(HttpContext.Current.Request.Form[key]).Trim();
            if (obj.Length > len)
            {
                return "";
            }
            if (type == 1)
            {
                obj = obj.ToString().ToLower();
            }
            obj = StringUtils.SafeCode(obj);
            return obj;
        }
    }
}