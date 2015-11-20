using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Ctrip.SOA.Infratructure.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 一个 <see cref="System.String"/> 实例的空数组。
        /// </summary>
        public static readonly string[] EmptyStrings = new string[] { };

        /// <summary>
        /// 获取一个值，该值指示指定的 <see cref="System.String"/> 对象不是 <b>null</b> 引用和 <b>Empty</b> 字符串。
        /// </summary>
        /// <param name="value">一个 <see cref="System.String"/> 引用，可能为 <b>null</b> 引用。</param>
        /// <returns>
        /// 如果 <paramref name="value"/> 不为 <b>null</b> 引用和 <b>Empty</b> 字符串, 则为 <b>true</b>；否则为 <b>false</b>。
        /// </returns>
        /// <remarks>
        /// <para>等同于 <b>!IsNullOrEmpty</b></para>
        /// <para>
        ///	参考：<a href="http://msdn.microsoft.com/zh-cn/library/ms182279(VS.80).aspx">使用字符串长度测试是否有空字符串</a>。
        /// </para>
        /// </remarks>
        /// <example>
        /// <code language="C#">
        /// string value = ...;
        ///	bool actual = StringHelper.HasLength(value);
        ///	
        /// string value = ...;
        ///	bool actual = value.HasLength();
        ///	</code>
        ///	
        /// <list type="table">
        /// <listheader><term>value</term><term>actual</term></listheader>
        /// <item><term>null</term><term>false</term></item>
        /// <item><term>""</term><term>false</term></item>
        /// <item><term>" "</term><term>true</term></item>
        /// <item><term>"Test"</term><term>true</term></item>
        /// </list>
        /// </example>
        public static bool HasLength(this string value)
        {
            return value != null && value.Length > 0;
        }

        /// <summary>
        /// 获取一个值，该值指示指定的 <see cref="System.String"/> 对象包含非空字符串。
        /// </summary>
        /// <param name="value">一个 <see cref="System.String"/> 引用，可能为 <b>null</b> 引用。</param>
        /// <returns>如果 <paramref name="value"/> 不为 <b>null</b> 引用和指定 <see cref="System.String"/> 字符串的长度大于零(不包括一串连续的空白字符串)， 则为 <b>true</b>；否则为 <b>false</b>。</returns>
        /// <example>
        /// <code language="C#">
        /// string value = ...;
        ///	bool actual = StringHelper.HasText(value);
        ///	
        /// string value = ...;
        ///	bool actual = value.HasText();
        ///	</code>
        ///	
        /// <list type="table">
        /// <listheader><term>value</term><term>actual</term></listheader>
        /// <item><term>null</term><term>false</term></item>
        /// <item><term>""</term><term>false</term></item>
        /// <item><term>" "</term><term>false</term></item>
        /// <item><term>"Test"</term><term>true</term></item>
        /// <item><term>" Test "</term><term>true</term></item>
        /// </list>
        /// </example>
        public static bool HasText(this string value)
        {
            if (value == null)
            {
                return false;
            }
            else
            {
                return HasLength(value.Trim());
            }
        }

        /// <summary>
        /// 获取一个值，该值指示指定的 <see cref="System.String"/> 对象是 <b>null</b> 引用或 <b>Empty</b> 字符串。
        /// </summary>
        /// <param name="value">一个 <see cref="System.String"/> 引用，可能为 <b>null</b> 引用。</param>
        /// <returns>如果 <paramref name="value"/> 为 <b>null</b> 引用或 <b>Empty</b> 字符串, 则为 <b>true</b>；否则为 <b>false</b>。</returns>
        /// <remarks>
        /// <para>
        ///	参考：<a href="http://msdn.microsoft.com/zh-cn/library/ms182279(VS.80).aspx">使用字符串长度测试是否有空字符串</a>。
        /// </para>
        /// </remarks>
        /// <example>
        /// <code language="C#">
        /// string value = ...;
        ///	bool actual = StringHelper.IsNullOrEmpty(value);
        ///	
        /// string value = ...;
        ///	bool actual = value.IsNullOrEmpty();
        ///	</code>
        ///	
        /// <list type="table">
        /// <listheader><term>value</term><term>actual</term></listheader>
        /// <item><term>null</term><term>true</term></item>
        /// <item><term>""</term><term>true</term></item>
        /// <item><term>" "</term><term>false</term></item>
        /// <item><term>"Test"</term><term>false</term></item>
        /// <item><term>" Test "</term><term>false</term></item>
        /// </list>
        /// </example>
        public static bool IsNullOrEmpty(this string value)
        {
            return String.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 判断一个字符串是否为空。
        /// </summary>
        /// <param name="s">要判断的字符串。</param>
        /// <returns>如果 <paramref name="s"/> 为空，则返回 <b>true</b>, 反之返回 <b>false</b>。</returns>
        public static bool IsEmpty(this string s)
        {
            return s == string.Empty;
        }

        /// <summary>
        /// 用 <b>Empty</b> 字符串替换 <b>null</b> 引用。
        /// </summary>
        /// <param name="value">一个 <see cref="System.String"/> 引用。</param>
        /// <returns>如果 <paramref name="value"/> 为 <b>null</b> 引用，则返回 <b>Empty</b> 字符串，否则返回 <paramref name="value"/></returns>
        /// <example>
        /// <code language="C#">
        /// string value = ...;
        ///	string actual = StringHelper.NullToEmpty(value);
        ///	
        /// string value = ...;
        ///	string actual = value.NullToEmpty();
        ///	</code>
        ///	
        /// <list type="table">
        /// <listheader><term>value</term><term>actual</term></listheader>
        /// <item><term>null</term><term>String.Empty</term></item>
        /// <item><term>""</term><term>""</term></item>
        /// <item><term>" "</term><term>" "</term></item>
        /// <item><term>"Test"</term><term>"Test"</term></item>
        /// <item><term>" Test "</term><term>" Test "</term></item>
        /// <item><term>" Te st "</term><term>" Te st "</term></item>
        /// </list>
        /// </example>
        public static string NullToEmpty(this string value)
        {
            return (value ?? string.Empty);
        }

        /// <summary>
        /// 用 <b>null</b> 引用替换 <b>Empty</b> 字符串。
        /// </summary>
        /// <param name="value">一个 <see cref="System.String"/> 引用。</param>
        /// <returns>如果 <paramref name="value"/> 为 <b>null</b> 引用，则返回 <b>Empty</b> 字符串，否则返回 <paramref name="value"/></returns>
        /// <example>
        /// <code language="C#">
        /// string value = ...;
        ///	string target = StringHelper.NullToEmpty(value);
        ///	
        /// string value = ...;
        ///	string target = value.NullToEmpty();
        ///	</code>
        ///	
        /// <list type="table">
        /// <listheader><term>value</term><term>target</term></listheader>
        /// <item><term>null</term><term>null</term></item>
        /// <item><term>string.Empty</term><term>string.Empty</term></item>
        /// <item><term>" "</term><term>" "</term></item>
        /// <item><term>"Test"</term><term>"Test"</term></item>
        /// <item><term>" Test "</term><term>" Test "</term></item>
        /// <item><term>" Te st "</term><term>" Te st "</term></item>
        /// </list>
        /// </example>
        public static string EmptyToNull(this string value)
        {
            return value.IsNullOrEmpty() ? null : value;
        }

        /// <summary>
        /// 从当前 <see cref="System.String"/> 对象移除所有前导空白字符和尾部空白字符。 
        /// </summary>
        /// <param name="value">一个 <see cref="System.String"/> 引用。</param>
        /// <returns>从当前 <see cref="System.String"/> 对象的开始和末尾移除所有空白字符后保留的字符串。如果当前 <see cref="System.String"/> 对象为空引用，则返回 <b>Empty</b> 字符串。</returns>
        /// <example>
        /// <code language="C#">
        /// string value = ...;
        ///	string actual = StringHelper.TrimNullToEmpty(value);
        ///	
        /// string value = ...;
        ///	string actual = value.TrimNullToEmpty();
        ///	</code>
        ///	
        /// <list type="table">
        /// <listheader><term>value</term><term>actual</term></listheader>
        /// <item><term>null</term><term>String.Empty</term></item>
        /// <item><term>""</term><term>String.Empty</term></item>
        /// <item><term>" "</term><term>String.Empty</term></item>
        /// <item><term>"Test"</term><term>"Test"</term></item>
        /// <item><term>" Test "</term><term>"Test"</term></item>
        /// <item><term>" Te st "</term><term>"Te st"</term></item>
        /// </list>
        /// </example>
        public static string TrimNullToEmpty(this string value)
        {
            return value == null ? string.Empty : value.Trim();
        }

        /// <summary>
        /// 从当前 <see cref="System.String"/> 对象移除所有前导空白字符和尾部空白字符。 
        /// </summary>
        /// <param name="value">一个 <see cref="System.String"/> 引用。</param>
        /// <returns>
        /// 从当前 <see cref="System.String"/> 对象的开始和末尾移除所有空白字符后保留的字符串。
        /// <para>如果当前 <see cref="System.String"/> 对象为空引用，则返回 <b>null</b> 引用。</para>
        /// <para>如果当前 <see cref="System.String"/> 对象在移除开始和末尾的所有空白字符后为 <b>Empty</b> 字符串，则返回 <b>null</b> 引用。</para>
        /// </returns>
        /// <example>
        /// <code language="C#">
        /// string value = ...;
        ///	string actual = StringHelper.TrimEmptyToNull(value);
        ///	
        /// string value = ...;
        ///	string actual = value.TrimEmptyToNull();
        ///	</code>
        ///	
        /// <list type="table">
        /// <listheader><term>value</term><term>actual</term></listheader>
        /// <item><term>null</term><term>null</term></item>
        /// <item><term>""</term><term>null</term></item>
        /// <item><term>" "</term><term>null</term></item>
        /// <item><term>"Test"</term><term>"Test"</term></item>
        /// <item><term>" Test "</term><term>"Test"</term></item>
        /// <item><term>" Te st "</term><term>"Te st"</term></item>
        /// </list>
        /// </example>
        public static string TrimEmptyToNull(this string value)
        {
            if (value == null)
            {
                return null;
            }
            string str = value.Trim();
            if (str == string.Empty)
            {
                return null;
            }
            return str;
        }

        /// <summary>
        /// 将指定 <see cref="System.String"/> 中的格式项替换为指定数组中相应 <see cref="System.Object"/> 实例的值的文本等效项。
        /// 采用固定区域格式。
        /// </summary>
        /// <param name="format"><a href="http://msdn.microsoft.com/zh-cn/library/txafckwd.aspx">复合格式字符串</a></param>
        /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象。</param>
        /// <returns>
        /// <paramref name="format"/> 的一个副本，
        /// 其中格式项已替换为 <paramref name="args"/> 中相应对象的字符串表示形式。
        /// </returns>
        /// <exception cref="System.FormatException">
        /// <paramref name="format"/> 无效。
        /// - 或 -
        /// 格式项的索引小于零或大于等于 <paramref name="args"/> 数组的长度。 
        /// </exception>
        /// <example>
        /// <code language="C#">
        /// string format = "The field:{0} is invlaid.";
        /// format.FormatWith("EmailAddress");
        /// </code>
        /// </example>
        public static string FormatWith(this string format, params object[] args)
        {
            string val = string.Empty;
            if (string.IsNullOrWhiteSpace(format))
            {
                val = format;
            }
            else
            {
                if (args == null || args.Length == 0)
                {
                    val = format;
                }
                else
                {
                    val = string.Format(CultureInfo.InvariantCulture, format, args);
                }
            }

            return val;
        }

        /// <summary>
        /// 获取字符串中从左边开始指定个数的字符。
        /// </summary>
        /// <param name="value">一个 <see cref="System.String"/> 引用。</param>
        /// <param name="length">子字符串中的字符数。</param>
        /// <returns>
        /// 一个 String，它等于指定字符串从左边开始长度为 length 的子字符串。
        /// </returns>
        /// <example>
        /// <code language="C#">
        /// string value = ...;
        ///	string target = StringHelper.Left(value, 2);
        ///	
        /// string value = ...;
        ///	string target = value.Left(2);
        ///	</code>
        ///	
        /// <list type="table">
        /// <listheader><term>value</term><term>length</term><term>target</term></listheader>
        /// <item><term>null</term><term>任意值</term><term>String.Empty</term></item>
        /// <item><term>""</term><term>任意值</term><term>String.Empty</term></item>
        /// <item><term>"This is a string."</term><term>-1</term><term>String.Empty</term></item>
        /// <item><term>"This is a string."</term><term>6</term><term>"This i"</term></item>
        /// <item><term>"This is a string."</term><term>17(value.Length)</term><term>"This is a string."</term></item>
        /// <item><term>"This is a string."</term><term>100</term><term>"This is a string."</term></item>
        /// </list>
        /// </example>
        public static string Left(this string value, int length)
        {
            if (value == null)
            {
                return String.Empty;
            }

            int leftLength = length < 0 ? 0 : length;
            if (leftLength == 0)
            {
                return String.Empty;
            }

            return value.Substring(0, leftLength > value.Length ? value.Length : leftLength);
        }

        /// <summary>
        /// 获取字符串中从右边开始指定个数的字符。
        /// </summary>
        /// <param name="value">一个 <see cref="System.String"/> 引用。</param>
        /// <param name="length">子字符串中的字符数。</param>
        /// <returns>
        /// 一个 String，它等于指定字符串从右边开始长度为 length 的子字符串。
        /// </returns>
        /// <example>
        /// <code language="C#">
        /// string value = ...;
        ///	string target = StringHelper.Right(value, 2);
        ///	
        /// string value = ...;
        ///	string target = value.Right(2);
        ///	</code>
        ///	
        /// <list type="table">
        /// <listheader><term>value</term><term>length</term><term>target</term></listheader>
        /// <item><term>null</term><term>任意值</term><term>String.Empty</term></item>
        /// <item><term>""</term><term>任意值</term><term>String.Empty</term></item>
        /// <item><term>"This is a string."</term><term>-1</term><term>String.Empty</term></item>
        /// <item><term>"This is a string."</term><term>6</term><term>"tring."</term></item>
        /// <item><term>"This is a string."</term><term>17(value.Lenght)</term><term>"This is a string."</term></item>
        /// <item><term>"This is a string."</term><term>100</term><term>"This is a string."</term></item>
        /// </list>
        /// </example>
        public static string Right(this string value, int length)
        {
            if (value == null)
            {
                return String.Empty;
            }

            int rightLength = length < 0 ? 0 : length;
            if (rightLength == 0)
            {
                return String.Empty;
            }

            rightLength = rightLength > value.Length ? value.Length : rightLength;

            return value.Substring(value.Length - rightLength, rightLength);
        }

        /// <summary>
        /// 移除一个字符串的前缀。
        /// </summary>
        /// <param name="value">字符串。</param>
        /// <param name="prefix">前缀。</param>
        /// <returns>移除了前缀 <paramref name="prefix"/> 的子字符串。</returns>
        /// <example>
        /// <code language="C#">
        /// string value = ...;
        ///	string target = StringHelper.RemovePrefix(value, "...");
        ///	
        /// string value = ...;
        ///	string target = value.RemovePrefix("...");
        ///	</code>
        ///	
        /// <list type="table">
        /// <listheader><term>value</term><term>token</term><term>target</term></listheader>
        /// <item><term>null</term><term>任意值</term><term>null</term></item>
        /// <item><term>String.Empty</term><term>任意值</term><term>String.Empty</term></item>
        /// <item><term>"This is a string."</term><term>null</term><term>"This is a string."</term></item>
        /// <item><term>"This is a string."</term><term>"This"</term><term>" is a string."</term></item>
        /// <item><term>"This is a string."</term><term>"This is a string."</term><term>""</term></item>
        /// </list>
        /// </example>
        public static string RemovePrefix(this string value, string prefix)
        {
            if (value.HasLength()
                && prefix.HasLength()
                && value.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
            {
                return value.Substring(prefix.Length);
            }
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string StringAfterFirst(this string s, string token)
        {
            if (s.Contains(token))
            {
                return s.Substring(s.ToLower().IndexOf(token.ToLower()) + token.Length);
            }
            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="masterString"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string StringAfterLast(this string s, string token)
        {
            if (s.Contains(token))
            {
                return s.Substring(s.ToLower().LastIndexOf(token.ToLower()) + 1);
            }
            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string StringBeforeFirst(this string s, string token)
        {
            if (s.Contains(token))
            {
                return s.Substring(0, s.ToLower().IndexOf(token.ToLower()));
            }
            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string StringBeforeLast(this string value, string token)
        {
            if (value.Contains(token))
            {
                return value.Substring(0, value.ToLower().LastIndexOf(token.ToLower()));
            }
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringToTrim"></param>
        /// <param name="prefixPattern"></param>
        /// <returns></returns>
        public static string WithoutPrefixPattern(this string stringToTrim, string prefixPattern)
        {
            return Regex.Replace(stringToTrim, "^" + prefixPattern, string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringToTrim"></param>
        /// <param name="suffixPattern"></param>
        /// <returns></returns>
        public static string WithoutSuffixPattern(this string value, string suffixPattern)
        {
            return Regex.Replace(value, suffixPattern + "$", string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringToAlter"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string WithPrefix(this string value, string prefix)
        {
            string str = value;
            if (!str.StartsWith(prefix))
            {
                str = prefix + str;
            }
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string WithSuffix(this string value, string suffix)
        {
            string str = value;
            if (!str.EndsWith(suffix))
            {
                str = str + suffix;
            }

            return str;
        }

        public static string Indent(this string value, int count)
        {
            return "".PadLeft(count) + value;
        }

        #region RemoveHtml

        /// <summary>
        /// 移除字符串中的Html标签。
        /// </summary>
        /// <param name="s">要移除Html标签的字符串。</param>
        /// <returns>移除了Html标签后的字符串。</returns>
        /// <example>
        /// <code>
        /// RemoveHtml("&lt;b&gt;Test&lt;/b&gt;") = "Test"
        /// RemoveHtml("&lt;bTest&gt;") = ""
        /// RemoveHtml("&lt;b&gt;Test&gt;") = "Test&gt;"
        /// RemoveHtml("&lt;b&gt;Test&lt;") = "Test&lt;"
        /// </code>
        /// </example>
        public static string RemoveHtml(this string value)
        {
            return RemoveHtml(value, null);
        }

        /// <summary>
        /// 移除字符串中的特定Html标签。
        /// </summary>
        /// <param name="value">输入字符串。</param>
        /// <param name="removeTags">要移除的Html标签。</param>
        /// <returns>移除了特定Html标签的字符串。</returns>
        /// <example>
        /// <code>
        /// List&lt;string&gt; removedTags = new List&lt;string&gt;();
        ///	removedTags.Add("font");
        ///	string source = "&lt;font&gt;&lt;b&gt;Test&lt;/b&gt;&lt;/font&gt;";
        ///	string target = source.RemoveHtml(removedTags);
        /// target == "&gt;&lt;b&gt;Test&lt;/b&gt;"
        /// </code>
        /// <code>
        /// List&lt;string&gt; removedTags = new List&lt;string&gt;();
        ///	removedTags.Add("font");
        ///	string source = "&lt;font&gt;&lt;b>Test&lt;/b&gt;&lt;/font";
        ///	string expected = "&lt;b&gt;Test&lt;/b&gt;&lt;/font";
        ///	string target = source.RemoveHtml(removedTags);
        /// </code>
        /// </example>
        public static string RemoveHtml(this string value, IList<string> removeTags)
        {
            if (!value.HasText())
            {
                return string.Empty;
            }

            List<string> removeTagsUpper = null;

            if (removeTags != null)
            {
                removeTagsUpper = new List<string>(removeTags.Count);

                foreach (string tag in removeTags)
                {
                    removeTagsUpper.Add(tag.ToUpper(CultureInfo.InvariantCulture));
                }
            }

            value = value.Replace("&nbsp;", " ");

            Regex anyTag = new Regex(@"<"
                    + @"(?<endTag>/)?"    //Captures the / if this is an end tag.
                    + @"(?<tagname>\w+)"    //Captures TagName
                    + @"("                //Groups tag contents
                    + @"(\s+"            //Groups attributes
                    + @"(?<attName>\w+)"  //Attribute name
                    + @"("                //groups =value portion.
                    + @"\s*=\s*"            // = 
                    + @"(?:"        //Groups attribute "value" portion.
                    + @"""(?<attVal>[^""]*)"""    // attVal='double quoted'
                    + @"|'(?<attVal>[^']*)'"        // attVal='single quoted'
                    + @"|(?<attVal>[^'"">\s]+)"    // attVal=urlnospaces
                    + @")"
                    + @")?"        //end optional att value portion.
                    + @")+\s*"        //One or more attribute pairs
                    + @"|\s*"            //Some white space.
                    + @")"
                    + @"(?<completeTag>/)?>" //Captures the "/" if this is a complete tag.
                    );

            return anyTag.Replace(
                value,
                delegate(Match match)
                {
                    string tag = match.Groups["tagname"].Value.ToUpper(CultureInfo.InvariantCulture);

                    if (removeTagsUpper == null)
                    {
                        return string.Empty;
                    }
                    else if (removeTagsUpper.Contains(tag))
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return match.Value;
                    }
                });
        }

        #endregion

        public static string[] SplitString(string strContent, string strSplit)
        {
            if (string.IsNullOrEmpty(strContent))
                return new string[0];
            if (strContent.IndexOf(strSplit) >= 0)
                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            return new string[1]
              {
                strContent
              };
        }

        public static string[] SplitString(string strContent, string strSplit, int count)
        {
            string[] strArray1 = new string[count];
            string[] strArray2 = SplitString(strContent, strSplit);
            for (int index = 0; index < count; ++index)
                strArray1[index] = index >= strArray2.Length ? string.Empty : strArray2[index];
            return strArray1;
        }
    }
}
