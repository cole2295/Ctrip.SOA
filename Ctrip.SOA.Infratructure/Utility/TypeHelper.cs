using System;
using System.Collections.Generic;
using System.Text;
using Ctrip.SOA.Infratructure.TypeResolution;

namespace Ctrip.SOA.Infratructure.Utility
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class TypeHelper
    {
        private static readonly ITypeResolver _internalTypeResolver = new CachedTypeResolver(new TypeResolver());

        /// <summary>
        /// Resolves the supplied type name into a <see cref="System.Type"/>
        /// instance.
        /// </summary>
        /// <param name="typeName">
        /// The (possibly partially assembly qualified) name of a
        /// <see cref="System.Type"/>.
        /// </param>
        /// <returns>
        /// A resolved <see cref="System.Type"/> instance.
        /// </returns>
        /// <exception cref="System.TypeLoadException">
        /// If the type cannot be resolved.
        /// </exception>
        public static Type ResolveType(string typeName)
        {
            Type type = TypeRegistry.ResolveType(typeName);
            if (type == null)
            {
                type = _internalTypeResolver.Resolve(typeName);
            }
            return type;
        }

        /// <summary>
        /// 返回指定类型的基础类型。
        /// </summary>
        /// <param name="type">类型。</param>
        /// <returns>基础类型。</returns>
        public static Type GetUnderlyingType(Type type)
        {
            Type underlyingType = type;
            if (IsNullableType(type))
            {
                underlyingType = Nullable.GetUnderlyingType(underlyingType);
            }

            return underlyingType ?? type;
        }

        /// <summary>
        /// 判断是否是基本类型。
        /// </summary>
        public static bool IsBasicType(Type type)
        {
            Type dataType = type;
            dataType = TypeHelper.GetUnderlyingType(dataType);

            if (dataType.Equals(typeof(bool))
                || dataType.Equals(typeof(byte))
                || dataType.Equals(typeof(DateTime))
                || dataType.Equals(typeof(DateTimeOffset))
                || dataType.Equals(typeof(decimal))
                || dataType.Equals(typeof(double))
                || dataType.Equals(typeof(Guid))
                || dataType.Equals(typeof(Int16))
                || dataType.Equals(typeof(Int32))
                || dataType.Equals(typeof(Int64))
                || dataType.Equals(typeof(sbyte))
                || dataType.Equals(typeof(Single))
                || dataType.Equals(typeof(string))
                || dataType.Equals(typeof(UInt16))
                || dataType.Equals(typeof(UInt32))
                || dataType.Equals(typeof(UInt64))
                || dataType.IsEnum)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks, if the specified type is a nullable
        /// </summary>
        public static bool IsNullableType(Type type)
        {
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Returns an array of Type objects corresponding
        /// to the type of parameters provided.
        /// </summary>
        /// <param name="args">
        /// Parameter values.
        /// </param>
        public static Type[] GetParameterTypes(params object[] args)
        {
            List<Type> result = new List<Type>();

            if (args != null)
            {
                foreach (object item in args)
                {
                    if (item == null)
                    {
                        result.Add(typeof(object));
                    }
                    else
                    {
                        result.Add(item.GetType());
                    }
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GetTypesString(params object[] args)
        {
            StringBuilder sb = new StringBuilder();
            Type[] argsTypes = GetParameterTypes(args);
            foreach (Type type in argsTypes)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }

                sb.Append(type.Name);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 确定当前的实例是否可以从指定 Type 的实例分配。
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="requiredType">与当前实例的 Type 进行比较的 Type。</param>
        /// <returns>
        /// 如果满足下列任一条件，则为 true：
        /// 1) requiredType 和当前实例 Type 表示同一类型；
        /// 2) 当前实例 Type 位于 requiredType 的继承层次结构中；
        /// 3) 当前实例 Type 是 requiredType 实现的接口；
        /// 4) requiredType 是泛型类型参数且当前实例 Type 表示 requiredType 的约束之一。
        /// 5) 当前实例是MarshalByRefObject实例。
        /// 如果不满足上述任何一个条件或者 requiredType 为 null 引用，则为 false。
        /// </returns>
        public static bool IsAssignableFrom(this object newValue, Type requiredType)
        {
            if (newValue is MarshalByRefObject)
            {
                //TODO see what type of type checking can be done.  May need to 
                //preserve information when proxy was created by SaoServiceExporter.
                return true;
            }
            if (requiredType == null)
            {
                return false;
            }
            return requiredType.IsAssignableFrom(newValue.GetType());
        }
    }
}
