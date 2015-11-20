using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace Ctrip.SOA.Infratructure.Utility
{
    public class StringUtils
    {
        /// <summary>
        /// 获取URL内容　UTF8编码
        /// </summary>
        /// <param name="ContentURL">URL地址</param>
        /// <returns></returns>
        public static string GetContent(string ContentURL)
        {
            try
            {
                Encoding enc = Encoding.UTF8;
                //Encoding enc = Encoding.Default;
                Uri uri = new Uri(ContentURL);

                HttpWebRequest hwreq = (HttpWebRequest)WebRequest.Create(uri);
                HttpWebResponse hwrsp = (HttpWebResponse)hwreq.GetResponse();

                byte[] bts = new byte[(int)hwrsp.ContentLength];
                Stream s = hwrsp.GetResponseStream();
                for (int i = 0; i < bts.Length; )
                {
                    i += s.Read(bts, i, bts.Length - i);
                }
                string content = enc.GetString(bts);
                return content;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 编码　默认编码
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public static string GBKUrlEncode(string k)
        {
            return System.Web.HttpUtility.UrlEncode(k, System.Text.Encoding.Default);
        }

        /// <summary>
        /// 解码　默认编码
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public static string GBKUrlDecode(string k)
        {
            return System.Web.HttpUtility.UrlDecode(k, System.Text.Encoding.Default);
        }

        /// <summary>
        /// 检查字符串是否是电子邮件地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsEmail(string request)
        {
            if (request == null)
                return false;
            Regex regex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            return regex.IsMatch(request.Trim());
        }

        /// <summary>
        /// 值是否为手机号
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsMobile(string request)
        {
            //Regex reg = new Regex(@"^1(?:3|5|8)\d{9}$");
            Regex reg = new Regex(@"^1[3,5,8]{1}[0-9]{1}[0-9]{8}$");
            if (reg.IsMatch(request))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 转为Bool类型
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool SafeBool(string text, bool defaultValue)
        {
            bool flag;
            if (bool.TryParse(text, out flag))
            {
                defaultValue = flag;
            }
            return defaultValue;
        }

        /// <summary>
        /// 转为时间类型
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTime SafeDateTime(string text, DateTime defaultValue)
        {
            DateTime time;
            if (DateTime.TryParse(text, out time))
            {
                defaultValue = time;
            }
            return defaultValue;
        }

        /// <summary>
        /// 转为Decimal类型
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal SafeDecimal(string text, decimal defaultValue)
        {
            decimal num;
            if (decimal.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        /// <summary>
        /// 转为Int类型
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int SafeInt(string text)
        {
            return SafeInt(text, 0);
        }

        /// <summary>
        /// 转为Int类型
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int SafeInt(string text, int defaultValue)
        {
            int num;
            if (int.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        /// <summary>
        /// 转为short类型
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static short SafeShort(string text, short defaultValue)
        {
            short num;
            if (short.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        /// <summary>
        /// 转为字符串
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string SafeStr(string text)
        {
            return SafeStr(text, "");
        }

        /// <summary>
        /// 转为字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string SafeStr(string text, string defaultValue)
        {
            if (String.IsNullOrEmpty(text))
            {
                return defaultValue;
            }
            return text.ToString();
        }

        /// <summary>
        /// 替换不安全字符
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string SafeCode(string Str)
        {
            string text1 = "" + Str;
            if (text1 != "")
            {
                text1 = text1.Replace("<", "&lt");
                text1 = text1.Replace(">", "&gt");
                //text1 = text1.Replace(",", "，");
                text1 = text1.Replace("'", "‘");
                text1 = text1.Replace("\"", "＂");
                text1 = text1.Replace("update", "");
                text1 = text1.Replace("insert", "");
                text1 = text1.Replace("delete", "");
                text1 = text1.Replace("--", "");
                text1 = text1.Replace("%", "");
                text1 = text1.Replace(";", "");
                //text1 = text1.Replace(",", "");

                text1 = text1.Replace("alert", "");
                text1 = text1.Replace("javascript", "");
            }
            return text1;
        }

        /// <summary>
        /// 组合数组
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string ToDelimitedString(ICollection collection, string delimiter)
        {
            if (collection == null)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            if (collection is Hashtable)
            {
                foreach (object obj2 in ((Hashtable)collection).Keys)
                {
                    builder.Append(obj2.ToString() + delimiter);
                }
            }
            if (collection is ArrayList)
            {
                foreach (object obj3 in (ArrayList)collection)
                {
                    builder.Append(obj3.ToString() + delimiter);
                }
            }
            if (collection is string[])
            {
                foreach (string str in (string[])collection)
                {
                    builder.Append(str + delimiter);
                }
            }
            if (collection is MailAddressCollection)
            {
                foreach (MailAddress address in (MailAddressCollection)collection)
                {
                    builder.Append(address.Address + delimiter);
                }
            }
            return builder.ToString().TrimEnd(new char[] { Convert.ToChar(delimiter, CultureInfo.InvariantCulture) });
        }

        /// <summary>
        /// 返回整数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetDbInt(object obj)
        {
            if (obj == null || obj == System.DBNull.Value || obj.ToString() == "")
            {
                return 0;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 返回整数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetDbInt(string obj)
        {
            int temp;
            if (string.IsNullOrEmpty(obj))
            {
                return 0;
            }
            else if (!int.TryParse(obj.Replace(",", ""), out temp))
            {
                return 0;
            }
            return int.Parse(obj.Replace(",", ""));
        }

        /// <summary>
        /// 返回字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetDbString(object obj)
        {
            if (obj == null || obj == System.DBNull.Value)
            {
                return String.Empty;
            }
            else
            {
                return obj.ToString().Trim();
            }
        }

        /// <summary>
        /// 返回日期
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime GetDbDateTime(object obj)
        {
            if (obj == null || obj == System.DBNull.Value)
            {
                return DateTime.MinValue;
            }
            else
            {
                return Convert.ToDateTime(obj);
            }
        }

        /// <summary>
        /// 返回Decimal类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Decimal GetDbDecimal(object obj)
        {
            if (obj == null || obj == System.DBNull.Value || obj.ToString() == "")
            {
                return 0;
            }
            else
            {
                string tempd = obj.ToString().TrimEnd('%');
                return decimal.Parse(tempd);
            }
        }

        /// <summary>
        /// 返回Decimal类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static Decimal GetDbDecimal(object obj, decimal defaultValue)
        {
            if (obj == null || obj == System.DBNull.Value || obj.ToString() == "")
            {
                return defaultValue;
            }
            else
            {
                string tempd = obj.ToString();
                return decimal.Parse(tempd);
            }
        }

        /// <summary>
        /// 返回Decimal类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Decimal GetDbDecimal(string obj)
        {
            decimal dec;
            if (obj == null || !decimal.TryParse(obj, out dec))
            {
                return 0;
            }
            else
            {
                return dec;
            }
        }

        /// <summary>
        /// 返回长整型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long GetDbLong(object obj)
        {
            if (obj == null || obj == System.DBNull.Value)
            {
                return 0;
            }
            else
            {
                long lId = 0;
                long.TryParse(obj.ToString(), out lId);
                return lId;
            }
        }

        /// <summary>
        /// 返回日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string date)
        {
            DateTime dt;
            if (DateTime.TryParse(date, out dt))
                return dt;
            return DateTime.MinValue;
        }

        /// <summary>
        /// 返回日期
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(object obj)
        {
            DateTime dt;
            if (obj == null)
            {
                return DateTime.MinValue;
            }
            else
            {
                if (DateTime.TryParse(obj.ToString(), out dt))
                {
                    return dt;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        /// <summary>
        /// 截取字符串　按字节计算
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="nBytes"></param>
        /// <param name="type">0 添加...</param>
        /// <returns></returns>
        public static string TrimByteStr(object obj, int nBytes, int type)
        {
            nBytes = nBytes * 2;
            if (obj == null)
            {
                return "";
            }
            if (nBytes <= 0)
                return "";

            if (nBytes % 2 != 0)
                nBytes++;

            byte[] blist = System.Text.Encoding.GetEncoding("Gb2312").GetBytes(obj.ToString());
            if (blist.Length > nBytes)
            {
                if (type == 0)
                {
                    return System.Text.Encoding.GetEncoding("Gb2312").GetString(blist, 0, nBytes).Replace("?", "") + "...";
                }
                else
                {
                    return System.Text.Encoding.GetEncoding("Gb2312").GetString(blist, 0, nBytes).Replace("?", "");
                }
            }
            else
            {
                return obj.ToString();
            }
            /*if (nBytes > blist.Length)
                nBytes = blist.Length;
            if (type == 0)
            {
                return System.Text.Encoding.GetEncoding("Gb2312").GetString(blist, 0, nBytes) + "...";
            }
            else
            {
                return System.Text.Encoding.GetEncoding("Gb2312").GetString(blist, 0, nBytes);
            }*/
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="len"></param>
        /// <param name="type">0 添加...</param>
        /// <returns></returns>
        public static string TrimStr(object obj, int len, int type)
        {
            if (obj != null)
            {
                if (obj.ToString().Length > len)
                {
                    if (type == 0)
                    {
                        return obj.ToString().Substring(0, len) + "...";
                    }
                    else
                    {
                        return obj.ToString().Substring(0, len);
                    }
                }
                else
                {
                    return obj.ToString();
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 返回本对象的Json序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        /// <summary>
        /// 截取EMAIL
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string SetReviewEmail(string email)
        {
            string[] arremail = email.Split('@');
            string tempstr = "";
            try
            {
                tempstr = arremail[0].Substring(0, arremail[0].Length - 2) + "**" + "@**";
                tempstr += arremail[1].Substring(2);
            }
            catch
            {
                tempstr = email;
            }
            return tempstr;
        }

        /// <summary>
        /// 去除HTML代码
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string StripHT(object strHtml)
        {
            Regex regex = new Regex("<.+?>", RegexOptions.IgnoreCase);
            string strOutput = regex.Replace(strHtml.ToString(), "");
            return strOutput.Replace("&nbsp;", "");
        }

        /// <summary>
        /// 检查字符串是否是日期
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsDateTimeAvailable(string request)
        {
            return IsDateTimeAvailable(request, DateTime.MinValue, DateTime.MaxValue);
        }

        /// <summary>
        /// 检查字符串是否是指定的日期
        /// </summary>
        /// <param name="request"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool IsDateTimeAvailable(string request, DateTime minValue, DateTime maxValue)
        {
            DateTime req_Time;
            try
            {
                req_Time = DateTime.Parse(request);
            }
            catch
            {
                return false;
            }
            if (req_Time > maxValue || req_Time < minValue)
                return false;

            return true;
        }

        /// <summary>
        /// 验证身份证号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsUserIdentityNumAvailable(string id)
        {
            if (id == null)
                return false;
            id = id.Trim();

            Match match = Regex.Match(id, @"\d{18}|\d{17}\w");
            if (!match.Success)
                match = Regex.Match(id, @"\d{15}");
            if (!match.Success)
                return false;
            if (match.Value != id)
                return false;

            return true;
        }
    }
}