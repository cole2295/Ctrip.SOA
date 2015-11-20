using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ctrip.SOA.Infratructure.TypeConversion {
    public class EntityTypeConverter{
        /// <summary>
        /// 将一个实体转换成另一个实体，只转换相应的属性/字段值
        /// </summary>
        /// <typeparam name="TSource">源实体类型</typeparam>
        /// <typeparam name="TResult">目标实体类型</typeparam>
        /// <param name="source">源实体</param>
        /// <returns>目标实体</returns>
        public static TResult ConvertTo<TSource, TResult>(TSource source) where TResult : new() {
            return ConvertTo<TSource, TResult>(source, null);
        }

        /// <summary>
        /// 将一个实体转换成另一个实体，只转换相应的属性/字段值
        /// </summary>
        /// <typeparam name="TSource">源实体类型</typeparam>
        /// <typeparam name="TResult">目标实体类型</typeparam>
        /// <param name="source">源实体</param>
        /// <param name="map">自定义属性名称映射</param>
        /// <returns>目标实体</returns>
        public static TResult ConvertTo<TSource, TResult>(TSource source, Dictionary<string, string> map) where TResult : new() {
            TResult result = new TResult();
            if (source != null) {
                string name = string.Empty;

                //如果是枚举
                if (source.GetType().IsEnum && result.GetType().IsEnum) {
                    Array rst = Enum.GetValues(result.GetType());
                    for (int j = 0; j < rst.Length; j++)
                        if (source.ToString() == rst.GetValue(j).ToString())
                            return (TResult)rst.GetValue(j);
                }

                // 取目标类型的所有属性
                PropertyInfo[] pis = result.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                // 遍历属性
                foreach (PropertyInfo pi in pis) {
                    if (map == null || !map.TryGetValue(pi.Name, out name)) {
                        name = pi.Name;
                    }

                    PropertyInfo pit = source.GetType().GetProperty(name);

                    // 如果能在源中找到相同的属性
                    if (pit != null && pi.PropertyType == pit.PropertyType) {
                        pi.SetValue(result, pit.GetValue(source, null), null);
                    }
                    else {
                        // 找不到，看源中是否有同名同类型的字段
                        FieldInfo fit = source.GetType().GetField(pi.Name);
                        if (fit != null && fit.FieldType == pi.PropertyType) {
                            pi.SetValue(result, fit.GetValue(source), null);
                        }
                    }
                }

                // 取目标类型的所有字段
                FieldInfo[] fis = result.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

                // 遍历字段
                foreach (FieldInfo fi in fis) {
                    if (map == null || !map.TryGetValue(fi.Name, out name)) {
                        name = fi.Name;
                    }

                    FieldInfo fit = source.GetType().GetField(fi.Name);

                    // 如果在源中能找到相同的字段
                    if (fit != null && fi.FieldType == fit.FieldType) {
                        fi.SetValue(result, fit.GetValue(source));
                    }
                    // 找不到，看源中是否有同名同类型的属性
                    else {
                        PropertyInfo pit = source.GetType().GetProperty(fi.Name);
                        if (pit != null && pit.PropertyType == fi.FieldType) {
                            fi.SetValue(result, pit.GetValue(source, null));
                        }
                    }
                }
            }
            return result;
        }
    }
}
