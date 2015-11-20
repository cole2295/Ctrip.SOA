using System;
using System.Collections.Generic;
using System.Globalization;

namespace Ctrip.SOA.Infratructure.Utility
{   
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Converter
    {
        /// <summary>
        /// 将布尔值 true 表示为可转换的字符串。此字段为只读。 
        /// </summary>
        /// <remarks>该字段包含字符串“TRUE”，“YES”，“ON”，“1”。</remarks>
        public static readonly List<string> TrueStringList = new List<string>();

        /// <summary>
        /// 将布尔值 false 表示为可转换的字符串。此字段为只读。 
        /// </summary>
        /// <remarks>该字段包含字符串“FALSE”，“NO”，“OFF”，“0”。</remarks>
        public static readonly List<string> FalseStringList = new List<string>();

        static Converter()
        {
            TrueStringList.Add("T");
            TrueStringList.Add("TRUE");
            TrueStringList.Add("YES");
            TrueStringList.Add("ON");
            TrueStringList.Add("1");

            FalseStringList.Add("F");
            FalseStringList.Add("FALSE");
            FalseStringList.Add("NO");
            FalseStringList.Add("OFF");
            FalseStringList.Add("0");
            FalseStringList.Add("");
        }

        /// <summary>
        /// 将字符串表示转换成等效的对象。
        /// </summary>
        /// <typeparam name="T">要把 <paramref name="value"/> 表示的 <see cref="System.String"/> 转换成的类型。</typeparam>
        /// <param name="value">包含要转换的 <see cref="String"/>。</param>
        /// <returns>
        /// <paramref name="conversionType"/> 类型的对象，其值由 value 表示。
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> 为空引用。</exception>
        /// <exception cref="FormatException"><paramref name="value"/> 的格式不符合 <paramref name="T"/>的 style。</exception>
        /// <exception cref="OverflowException"><paramref name="value"/> 超出允许范围。</exception>
        /// <remarks>
        /// 对于 <paramref name="T"/> 为 <see cref="System.DateTime"/> 的转换需做日期校验，日期值不能比 <see cref="DateTimeHelper.MinValue"/> 还要小
        /// <seealso cref="CheckTypeValue"/>
        /// </remarks>
        public static T ToType<T>(object value)
        {
            return ToType<T>(value, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 将字符串表示转换成等效的对象。
        /// </summary>
        /// <typeparam name="T">要把 <paramref name="value"/> 表示的 <see cref="System.String"/> 转换成的类型。</typeparam>
        /// <param name="value">包含要转换的 <see cref="String"/>。</param>
        /// <param name="defaultValue">转换不成功后替代的默认值</param>
        /// <returns>
        /// <paramref name="conversionType"/> 类型的对象，其值由 value 表示。
        /// 如果转换不成功，则返回 <pararef name="defaultValue">。
        /// </returns>
        /// <remarks>
        /// 对于 <paramref name="T"/> 为 <see cref="System.DateTime"/> 的转换需做日期校验，日期值不能比 <see cref="DateTimeHelper.MinValue"/> 还要小
        /// <seealso cref="CheckTypeValue"/>
        /// </remarks>
        public static T ToType<T>(object value, T defaultValue)
        {
            return ToType<T>(value, defaultValue, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 通过使用指定的区域性特定格式设置信息，将字符串表示转换成等效的对象。
        /// </summary>
        /// <typeparam name="T">要把 <paramref name="value"/> 表示的 <see cref="System.String"/> 转换成的类型。</typeparam>
        /// <param name="value">包含要转换的 <see cref="String"/>。</param>
        /// <param name="defaultValue">转换不成功后替代的默认值</param>
        /// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定的格式设置信息。</param>
        /// <returns>
        /// <paramref name="conversionType"/> 类型的对象，其值由 value 表示。
        /// 如果转换不成功，则返回 <pararef name="defaultValue">。
        /// </returns>
        /// <remarks>
        /// 对于 <paramref name="T"/> 为 <see cref="System.DateTime"/> 的转换需做日期校验，日期值不能比 <see cref="DateTimeHelper.MinValue"/> 还要小
        /// <seealso cref="CheckTypeValue"/>
        /// </remarks>
        public static T ToType<T>(object value, T defaultValue, IFormatProvider provider)
        {
            T targetValue = defaultValue;
            try
            {
                targetValue = ToType<T>(value, provider);
            }
            catch
            {
                targetValue = defaultValue;
            }

            return targetValue;
        }

        /// <summary>
        /// 将字符串表示转换成等效的对象。
        /// </summary>
        /// <typeparam name="T">要把 <paramref name="value"/> 表示的 <see cref="System.String"/> 转换成的类型。</typeparam>
        /// <param name="value">包含要转换的 <see cref="String"/>。</param>
        /// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定的格式设置信息。</param>
        /// <returns>
        /// <paramref name="conversionType"/> 类型的对象，其值由 value 表示。
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> 为空引用。</exception>
        /// <exception cref="FormatException"><paramref name="value"/> 的格式不符合 <paramref name="T"/>的 style。</exception>
        /// <exception cref="OverflowException"><paramref name="value"/> 超出允许范围。</exception>
        /// <remarks>
        /// 对于 <paramref name="T"/> 为 <see cref="System.DateTime"/> 的转换需做日期校验，日期值不能比 <see cref="DateTimeHelper.MinValue"/> 还要小
        /// <seealso cref="CheckTypeValue"/>
        /// </remarks>
        public static T ToType<T>(object value, IFormatProvider provider)
        {
            return (T)ToType(value, typeof(T), provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static object ToType(object value, Type conversionType, object defaultValue)
        {
            return ToType(value, conversionType, defaultValue, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <param name="defaultValue"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static object ToType(object value, Type conversionType, object defaultValue, IFormatProvider provider)
        {
            object targetValue = defaultValue;
            try
            {
                targetValue = ToType(value, conversionType, provider);
            }
            catch
            {
                targetValue = defaultValue;
            }

            return targetValue;
        }

        /// <summary>
        /// 将字符串表示转换成等效的对象。
        /// </summary>
        /// <param name="value">包含要转换的 <see cref="String"/>。</param>
        /// <param name="conversionType">要转换成的 <see cref="System.Type"/>。</param>
        /// <returns><paramref name="conversionType"/> 类型的对象，其值由 value 表示。</returns>
        public static object ToType(object value, Type conversionType)
        {
            return ToType(value, conversionType, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 通过使用指定的区域性特定格式设置信息，将字符串表示转换成等效的对象。
        /// </summary>
        /// <param name="value">包含要转换的 <see cref="String"/>。</param>
        /// <param name="conversionType">要转换成的 <see cref="System.Type"/>。</param>
        /// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定的格式设置信息。</param>
        /// <returns><paramref name="conversionType"/> 类型的对象，其值由 value 表示。</returns>
        public static object ToType(object value, Type conversionType, IFormatProvider provider)
        {
            object targetValue = null;

            if (TypeHelper.IsNullableType(conversionType))
            {
                if (value == null
                    || (value.GetType().Equals(typeof(string)) && string.IsNullOrEmpty((string)value)))
                {
                    return null;
                }

                conversionType = TypeHelper.GetUnderlyingType(conversionType);
            }

            targetValue = ChangeType(value, conversionType, provider);

            // 检查类型转换的合法性
            CheckTargetValue(ref targetValue);

            return targetValue;
        }

        public static T DataRowToType<T>(object value)
        {
            if (value == null || value == DBNull.Value)
                return default(T);

            return ToType<T>(value);
        }

        #region Helper

        /// <summary>
        /// 通过使用指定的区域性特定格式设置信息，将逻辑值的指定 <see cref="String"/> 表示形式转换为它的等效布尔值。
        /// </summary>
        /// <param name="value">包含 <see cref="TrueStringList"/> 或 <see cref="FalseStringList"/> 值的字符串。</param>
        /// <param name="provider">（保留）<see cref="IFormatProvider"/> 接口实现，它提供区域性特定的格式设置信息。</param>
        /// <returns>
        /// 如果 <paramref name="value"/> 存在于 <see cref="TrueStringList"/> 中，则为 <b>true</b>；
        /// 如果 <paramref name="value"/> 存在于 <see cref="FalseStringList"/> 中，则为 <b>false</b>。
        /// </returns>
        /// <exception cref="FormatException"><paramref name="value"/> 不等于 <see cref="TrueStringList"/> 或 <see cref="FalseStringList"/>。</exception>
        /// <remarks>
        /// <para>
        /// 若要成功执行转换，<paramref name="value"/> 参数必须存在于 <see cref="TrueStringList"/> 或 <see cref="FalseStringList"/>。
        /// 在对 value 与 <see cref="TrueStringList"/> 和 <see cref="FalseStringList"/> 中的布尔字符串进行比较时，该方法忽略大小写以及前导和尾随空白。
        /// </para>
        /// <para><paramref name="provider"/> 被忽略，它不参与此操作。</para>
        /// </remarks>
        private static bool ToBoolean(object value, IFormatProvider provider)
        {
            if (value == null)
            {
                return false;
            }

            if (value.GetType().Equals(typeof(string)))
            {
                string upperedValue = value.ToString().Trim().ToUpper();
                if (TrueStringList.Contains(upperedValue))
                {
                    return true;
                }
                else if (FalseStringList.Contains(upperedValue))
                {
                    return false;
                }
                else
                {
                    //ThrowHelper.ThrowFormatException(CoreSR.Format_BadBoolean, value);
                }
            }

            return Convert.ToBoolean(value, provider);
        }

        /// <summary>
        /// 通过使用指定的区域性特定格式设置信息，将数字的指定 <see cref="String"/> 表示形式转换为等效的 <see cref="System.DateTime"/>。
        /// </summary>
        /// <param name="value">包含要转换的日期和时间的 <see cref="String"/>。</param>
        /// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定的格式设置信息。</param>
        /// <returns>
        /// 等效于 <paramref name="value"/> 的值的一个 <see cref="System.DateTime"/>。 
        /// <br />- 或 -<br />
        /// 如果 <paramref name="value"/> 为 null 引用，则 <see cref="System.DateTime"/> 等效于 <see cref="DateTimeHelper.MinValue"/>。 
        /// </returns>
        /// <exception cref="FormatException"><paramref name="value"/> 不是格式正确的日期和时间字符串。</exception>
        /// <remarks>
        /// 返回值是对 <paramref name="value"/> 调用 <see cref="System.DateTime.Parse"/> 方法的结果。 
        /// <paramref name="provider"/> 是获取 <see cref="System.Globalization.DateTimeFormatInfo"/> 对象的 <see cref="System.IFormatProvider"/> 实例。
        /// <see cref="System.Globalization.DateTimeFormatInfo"/> 对象提供有关 <paramref name="value"/> 格式的区域性特定信息。
        /// 如果 <paramref name="provider"/> 为 null 引用，则使用当前区域性的 <see cref="System.Globalization.DateTimeFormatInfo"/>。 
        /// </remarks>
        private static DateTime ToDateTime(object value, IFormatProvider provider)
        {
            if (value == null)
            {
                return DateTimeHelper.MinValue;
            }
            return Convert.ToDateTime(value, provider);
        }

        /// <summary>
        /// 通过使用指定的区域性特定格式设置信息，将数字的指定 <see cref="String"/> 表示形式转换为等效的 <see cref="System.Guid"/>。
        /// </summary>
        /// <param name="value">包含要转换的<see cref="System.DateTime"/> 的 <see cref="String"/>。</param>
        /// <param name="provider">（保留）<see cref="IFormatProvider"/> 接口实现，提供区域性特定的格式设置信息。</param>
        /// <returns>
        /// 等效于 <paramref name="value"/> 的值的一个 <see cref="System.Guid"/>。 
        /// <br />- 或 -<br />
        /// 如果 <paramref name="value"/> 为 null 引用，则 <see cref="System.Guid"/> 等效于 <see cref="System.Guid.Empty"/>。 
        /// </returns>
        /// <exception cref="FormatException"><paramref name="value"/> 不是格式正确的 <see cref="System.Guid"> 字符串。</exception>
        /// <exception cref="OverflowException"><paramref name="value"/> 不是格式正确的 <see cref="System.Guid"> 字符串。</exception>
        /// <remarks>
        /// <para><paramref name="provider"/> 被忽略，它不参与此操作。</para>
        /// </remarks>
        private static Guid ToGuid(object value, IFormatProvider provider)
        {
            if (value == null)
            {
                return Guid.Empty;
            }

            if (value.GetType().Equals(typeof(Guid)))
            {
                return (Guid)value;
            }

            if (value.GetType().Equals(typeof(string)))
            {
                return new Guid((string)value);
            }

            return Guid.Empty;
        }

        /// <summary>
        /// 通过使用指定的区域性特定格式设置信息，将一个或多个枚举常数的名称或数字值的字符串表示转换成等效的枚举对象。
        /// </summary>
        /// <param name="value">包含要转换的<see cref="System.Enum"/> 的值或名称的 <see cref="String"/>。</param>
        /// <param name="conversionType">枚举的 <see cref="System.Type"/></param>
        /// <param name="provider">（保留）<see cref="IFormatProvider"/> 接口实现，提供区域性特定的格式设置信息。</param>
        /// <returns><paramref name="conversionType"/> 类型的对象，其值由 value 表示。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="conversionType"/> 或 <paramref name="value"/> 为 null 引用。</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="conversionType"/> 不是 <see cref="System.Enum"/>。 
        /// <br />- 或 - <br />
        /// <paramref name="value"/> 为空字符串或只包含空白。 
        /// <br />- 或 - <br />
        /// <paramref name="value"/> 是一个名称，但不是为该枚举定义的命名常量之一。 
        /// </exception>
        private static object ToEnum(object value, Type conversionType, IFormatProvider provider)
        {
            if (value == null)
            {
                return Enum.ToObject(conversionType, 0);
            }

            Type valueType = value.GetType();
            if (conversionType.IsEnum && valueType.Equals(conversionType))
            {
                return value;
            }

            if (valueType.Equals(typeof(string)))
            {
                return Enum.Parse(conversionType, (string)value, true);
            }
            else
            {
                return Enum.ToObject(conversionType, value);
            }
        }

        /// <summary>
        /// 通过使用指定的区域性特定格式设置信息，将字符串表示转换成等效的对象。
        /// </summary>
        /// <param name="value">包含要转换的 <see cref="String"/>。</param>
        /// <param name="conversionType">要转换成的 <see cref="System.Type"/>。</param>
        /// <param name="provider"><see cref="IFormatProvider"/> 接口实现，提供区域性特定的格式设置信息。</param>
        /// <returns><paramref name="conversionType"/> 类型的对象，其值由 value 表示。</returns>
        private static object ChangeType(object value, Type conversionType, IFormatProvider provider)
        {
            Guard.ArgumentNotNull(conversionType, "conversionType");

            if (!conversionType.IsValueType)
            {
                if (value == null)
                {
                    return null;
                }
                else if (value.GetType().Equals(conversionType) || conversionType.IsAssignableFrom(value.GetType()))
                {
                    return value;
                }
            }

            if (conversionType == typeof(bool))
            {
                return ToBoolean(value, provider);
            }
            if (conversionType == typeof(DateTime))
            {
                return ToDateTime(value, provider);
            }
            if (typeof(Enum).IsAssignableFrom(conversionType))
            {
                return ToEnum(value, conversionType, provider);
            }
            if (conversionType == typeof(Guid))
            {
                return ToGuid(value, provider);
            }

            if (value != null)
            {
                if (conversionType.IsValueType && (conversionType.IsPrimitive || conversionType.Equals(typeof(decimal))))
                {
                    if (value == null || (value.GetType().Equals(typeof(string)) && string.IsNullOrEmpty((string)value)))
                    {
                        return 0;
                    }
                }
            }

            if (conversionType == typeof(char))
            {
                return Convert.ToChar(value, provider);
            }
            if (conversionType == typeof(sbyte))
            {
                return Convert.ToSByte(value, provider);
            }
            if (conversionType == typeof(byte))
            {
                return Convert.ToByte(value, provider);
            }
            if (conversionType == typeof(short))
            {
                return Convert.ToInt16(value, provider);
            }
            if (conversionType == typeof(ushort))
            {
                return Convert.ToUInt16(value, provider);
            }
            if (conversionType == typeof(int))
            {
                return Convert.ToInt32(value, provider);
            }
            if (conversionType == typeof(uint))
            {
                return Convert.ToUInt32(value, provider);
            }
            if (conversionType == typeof(long))
            {
                return Convert.ToInt64(value, provider);
            }
            if (conversionType == typeof(ulong))
            {
                return Convert.ToUInt64(value, provider);
            }
            if (conversionType == typeof(float))
            {
                return Convert.ToSingle(value, provider);
            }
            if (conversionType == typeof(double))
            {
                return Convert.ToDouble(value, provider);
            }
            if (conversionType == typeof(decimal))
            {
                return Convert.ToDecimal(value, provider);
            }
            if (conversionType == typeof(string))
            {
                return Convert.ToString(value);
            }

            object obj = null;

            try
            {
                obj = Convert.ChangeType(value, conversionType, provider);
            }
            catch
            {
                ThrowHelper.ThrowNotSupportedException("不支持该类型", value, conversionType.FullName);
            }

            return obj;
        }

        /// <summary>
        /// 检查类型转换的合法性
        /// </summary>
        /// <param name="typeValue">包含需要校验的转换类型值。</param>
        /// <remarks>
        /// 包含以下类型校验:
        /// <para>
        /// 对于<see cref="System.DateTime"/>类型，日期值不能比 <see cref="DateTimeHelper.MinValue"/> 还要小。
        /// </para>
        /// </remarks>
        private static void CheckTargetValue(ref object targetValue)
        {
            if (targetValue == null)
                return;

            if (targetValue.GetType().Equals(typeof(DateTime)))
            {
                // 日期值不能比DateTimeHelper.MinValue还要小
                if (targetValue != null
                    && ((DateTime)targetValue).CompareTo(DateTimeHelper.MinValue) == -1)
                    targetValue = DateTimeHelper.MinValue;
            }
        }

        #endregion
    }
}
