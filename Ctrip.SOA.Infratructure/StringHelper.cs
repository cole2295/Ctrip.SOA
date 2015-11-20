using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;

namespace Ctrip.SOA.Infratructure
{
    public class StringHelper
    {
        /// <summary>
        /// 生成短号唯一标识
        /// </summary>
        /// <returns></returns>
        public static string GetGenerateStringID()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        /// <summary>
        /// 验证字符串是否为数字（正则表达式）（true = 是数字, false = 不是数字）
        /// </summary>
        /// <param name="validatedString">被验证的字符串</param>
        /// <returns>true = 是数字, false = 不是数字</returns>
        /// <remarks>
        /// 2008-4-16        傅林浩     创建
        /// </remarks>
        public static bool IsNumeric(string validatedString) {
            const string NumericPattern = @"^[-]?\d+[.]?\d*$";

            return Regex.IsMatch(validatedString, NumericPattern);
        }

        /// <summary>
        /// 设置多语言邮件模板前缀规则
        /// </summary>
        public static string MultiLanguageEmailTemplateID {
            get {
                Dictionary<string, string> dicEmailTemplateID = new Dictionary<string, string>();
                dicEmailTemplateID.Add("english", "901");
                dicEmailTemplateID.Add("jp", "902");
                dicEmailTemplateID.Add("kr", "903");
                dicEmailTemplateID.Add("fr", "904");
                dicEmailTemplateID.Add("de", "905");
                dicEmailTemplateID.Add("es", "906");
                dicEmailTemplateID.Add("ru", "907");
                //Add by xiaoliangwang 2013-09-06 永安易游 start
                dicEmailTemplateID.Add("tc", "910");
                //Add by xiaoliangwang 2013-09-06 永安易游 end

                dicEmailTemplateID.Add("vn", "909");
                //Add by xiaoliangwang 香港站点 2013-12-05 sart
                dicEmailTemplateID.Add("hk", "910");
                //Add by xiaoliangwang 香港站点 2013-12-05 end
                string langu = GetProcessLanguage();
                if (dicEmailTemplateID.ContainsKey(langu)) {
                    return dicEmailTemplateID[langu];
                }
                else {
                    return dicEmailTemplateID["english"];
                }
            }
        }

        /// <summary>
        /// 按字节长度对字符串进行拆分
        /// 方法性能：PC/CPU i5-2400/4G，10M数据按32K长度拆分，执行时间为100毫秒左右
        /// </summary>
        /// <param name="str">要拆分的字符串</param>
        /// <param name="byteLength">要拆分的字节长度</param>
        /// <returns>拆分好的字符串数组</returns>
        public static ArrayList SplitString(string str, int byteLength)
        {
            if (byteLength < 2)
            {
                throw new Exception("为了支持对汉字进行拆分，byteLength最小值为2");
            }

            // 先将字符串转换成byte
            byte[] strbyte = System.Text.Encoding.Default.GetBytes(str);

            // 返回的数组
            ArrayList returnString = new ArrayList();

            // 定义截取的开始位置和结束位置
            int startPos = 0, endPos = 0, currentLength = byteLength;

            while (endPos < strbyte.Length)
            {
                currentLength = byteLength;

                // 是否已经超过了结尾
                if (currentLength + endPos > strbyte.Length)
                {
                    currentLength -= currentLength + endPos - strbyte.Length;
                }
                endPos += currentLength;

                // 如果截断处字节是非英文，看是否需要减位
                if (strbyte[endPos - 1] < 0 || strbyte[endPos - 1] > 127)
                {
                    // 非英文字符所占用的字节
                    int flag = 0;

                    // 倒序遍历字符串
                    for (int j = endPos - 1; j >= startPos; j--)
                    {
                        // 找到第一个英文字符
                        if (strbyte[j] > 0 && strbyte[j] <= 127)
                        {
                            break;
                        }
                        else
                        {
                            flag++;
                        }
                    }

                    // 判断如果末尾到第一个英文字符的字节数是奇数
                    if (flag % 2 != 0)
                    {
                        // 截断处往前移一位
                        endPos -= 1;
                        currentLength -= 1;
                    }
                }

                // 截取相应长度放入数组
                returnString.Add(System.Text.Encoding.Default.GetString(strbyte, startPos, currentLength));
                startPos = endPos;
            }


            return returnString;
        }

        /// <summary>
        /// 简体转繁体：如果当前问TC，则根据chsStr参数翻译成繁体；否则返回enStr
        /// </summary>
        /// <param name="enStr">英文文字</param>
        /// <param name="chsStr">简体中文</param>
        /// <returns>如果当前问TC，则根据chsStr参数翻译成繁体；否则返回enStr</returns>
        public static string CHS2CHT(string enStr, string chsStr) {
            //判断是否为繁体
            if (IsTraditional_Chinese_Character() && !string.IsNullOrEmpty(chsStr)) {
                return ChineseConverter.Convert(HttpUtility.HtmlDecode(chsStr), ChineseConversionDirection.SimplifiedToTraditional);
            }
            else {
                return HttpUtility.HtmlDecode(enStr);
            }
        }

        /// <summary>
        /// 繁体转简体
        /// </summary>
        /// <param name="str">繁体文字</param>
        /// <returns></returns>
        public static string CHT2CHS(string str) {
            //判断是否为繁体
            if (IsTraditional_Chinese_Character() && !string.IsNullOrEmpty(str)) {
                return ChineseConverter.Convert(str, ChineseConversionDirection.TraditionalToSimplified);
            }
            else {
                return str;
            }
        }

        /// <summary>
        /// 是否繁体站点
        /// </summary>
        /// <returns></returns>
        public static bool IsTraditional_Chinese_Character() {
            bool returnValue = false;
            if (IsTC_Site() || IsHongKongSite()) {
                returnValue = true;
            }
            return returnValue;
        }

        /// <summary>
        /// 是否TC站点
        /// </summary>
        /// <returns></returns>
        public static bool IsTC_Site() {
            bool returnValue = false;
            if (GetProcessLanguageForDB() == "tc") {
                returnValue = true;
            }
            return returnValue;
        }

        /// <summary>
        /// 判断是否香港站点
        /// </summary>
        /// <returns></returns>
        public static bool IsHongKongSite() {
            bool returnValue = false;
            try {
                using (var fakePage = new Page()) {
                    if (fakePage != null && !string.IsNullOrEmpty(fakePage.Culture) && !string.IsNullOrEmpty(fakePage.UICulture)) {
                        if ((fakePage.Culture == "Chinese (Traditional, Hong Kong S.A.R.)" && fakePage.UICulture == "Chinese (Traditional, Hong Kong S.A.R.)")
                            || GetProcessLanguageForDB() == "hk") {
                            returnValue = true;
                        }
                    }
                    else {
                        //兼容jobws...
                        returnValue = true;
                    }
                }

            }
            catch (Exception ex) {
                //兼容jobws...
                returnValue = true;                
            }
            return returnValue;
        }

        /// <summary>
        /// 获取当前进程语言(english,jp,kr,vn)
        /// </summary>
        /// <returns>当前语言</returns>
        public static string GetProcessLanguage() {
            if (HttpContext.Current == null)
                return string.Empty;

            //Modfiy by xiaoliangwang 2013-08-20 永安易游 start
            if (HttpContext.Current.Items["lang"] != null)
                return HttpContext.Current.Items["lang"].ToString().ToLower();
            else
                return string.Empty;
            //Modfiy by xiaoliangwang 2013-08-20 永安易游 end
        }

        /// <summary>
        /// 返回数据库中使用的语言信息(en,jp,kr,vn)
        /// </summary>
        /// <returns>语言标记</returns>
        public static string GetProcessLanguageForDB() {
            //Modfiy by xiaoliangwang 2013-08-20 永安易游 start
            return GetProcessLanguage().Replace("english", "en").Replace("zh-tw", "tc");
            //Modfiy by xiaoliangwang 2013-08-20 永安易游 end
        }
    }
}
