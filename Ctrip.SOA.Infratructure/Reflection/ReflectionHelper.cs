using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Reflection
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ReflectionHelper
    {
        /// <summary>
        /// 根据参数名字获取指定的构造方法
        /// </summary>
        /// <param name="target"></param>
        /// <param name="namedArgValues"></param>
        /// <returns></returns>
        public static ConstructorInfo GetConstructorByNamedArgumentValues(Type target, IDictionary<string, object> namedArgValues)
        {
            ConstructorInfo match = null;
            int matchCount = 0;

            ConstructorInfo[] constructorInfos = target.GetConstructors();
            foreach (ConstructorInfo constructorInfo in constructorInfos)
            {
                ParameterInfo[] parameters = constructorInfo.GetParameters();
                bool isMatch = true;
                IDictionary<string, object> paramNames = (namedArgValues == null) ? new Dictionary<string, object>() : namedArgValues;                                

                if (paramNames.Count != parameters.Length)
                {
                    isMatch = false;
                }
                else
                {
                    foreach (ParameterInfo parameterInfo in parameters)
                    {
                        // 如果存在参数不匹配，则退出
                        if (!namedArgValues.ContainsKey(parameterInfo.Name))
                        {
                            isMatch = false;
                            break;
                        }
                    }
                }

                if (isMatch)
                {
                    matchCount++;
                    if (matchCount == 1)
                    {
                        if (match == null)
                        {
                            match = constructorInfo;
                        }
                    }
                    else
                    {
                        throw new AmbiguousMatchException(
                            string.Format(
                                "Ambiguous match for {0} '{1}' for the specified names of arguments.", 
                                target.FullName,
                                constructorInfo.Name));
                    }
                }
            }

            return match;
        }

        /// <summary>
        /// From a given list of constructors, selects the constructor having an exact match on the given <paramref name="argValues"/>' types.
        /// </summary>
        /// <param name="methods">the list of constructors to choose from</param>
        /// <param name="argValues">the arguments to the method</param>
        /// <returns>the constructor matching exactly the passed <paramref name="argValues"/>' types</returns>
        /// <exception cref="AmbiguousMatchException">
        /// If more than 1 matching methods are found in the <paramref name="methods"/> list.
        /// </exception>
        public static ConstructorInfo GetConstructorByArgumentValues<T>(IList<T> methods, object[] argValues) where T : MethodBase
        {
            return (ConstructorInfo)GetMethodBaseByArgumentValues("constructor", methods, argValues);
        }

        /// <summary>
        /// From a given list of methods, selects the method having an exact match on the given <paramref name="argValues"/>' types.
        /// </summary>
        /// <param name="methodTypeName">the type of method (used for exception reporting only)</param>
        /// <param name="methods">the list of methods to choose from</param>
        /// <param name="argValues">the arguments to the method</param>
        /// <returns>the method matching exactly the passed <paramref name="argValues"/>' types</returns>
        /// <exception cref="AmbiguousMatchException">
        /// If more than 1 matching methods are found in the <paramref name="methods"/> list.
        /// </exception>
        private static MethodBase GetMethodBaseByArgumentValues<T>(string methodTypeName, IEnumerable<T> methods, object[] argValues) where T : MethodBase
        {
            MethodBase match = null;
            int matchCount = 0;

            foreach (MethodBase m in methods)
            {
                ParameterInfo[] parameters = m.GetParameters();
                bool isMatch = true;
                bool isExactMatch = true;
                object[] paramValues = (argValues == null) ? new object[0] : argValues;

                try
                {
                    if (parameters.Length > 0)
                    {
                        ParameterInfo lastParameter = parameters[parameters.Length - 1];
                        if (lastParameter.GetCustomAttributes(typeof(ParamArrayAttribute), false).Length > 0)
                        {
                            paramValues =
                                PackageParamArray(argValues, parameters.Length,
                                                  lastParameter.ParameterType.GetElementType());
                        }
                    }

                    if (parameters.Length != paramValues.Length)
                    {
                        isMatch = false;
                    }
                    else
                    {
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            Type paramType = parameters[i].ParameterType;
                            object paramValue = paramValues[i];

                            if ((paramValue == null && paramType.IsValueType && !TypeHelper.IsNullableType(paramType))
                                || (paramValue != null && !paramType.IsAssignableFrom(paramValue.GetType())))
                            {
                                isMatch = false;
                                break;
                            }

                            if (paramValue == null || paramType != paramValue.GetType())
                            {
                                isExactMatch = false;
                            }
                        }
                    }
                }
                catch (InvalidCastException)
                {
                    isMatch = false;
                }

                if (isMatch)
                {
                    if (isExactMatch)
                    {
                        return m;
                    }

                    matchCount++;
                    if (matchCount == 1)
                    {
                        match = m;
                    }
                    else
                    {
                        throw new AmbiguousMatchException(
                            string.Format("Ambiguous match for {0} '{1}' for the specified number and types of arguments.", methodTypeName,
                                          m.Name));
                    }
                }
            }

            return match;
        }

        /// <summary>
        /// Packages arguments into argument list containing parameter array as a last argument.
        /// </summary>
        /// <param name="argValues">Argument vaklues to package.</param>
        /// <param name="argCount">Total number of oarameters.</param>
        /// <param name="elementType">Type of the param array element.</param>
        /// <returns>Packaged arguments.</returns>
        public static object[] PackageParamArray(object[] argValues, int argCount, Type elementType)
        {
            object[] values = new object[argCount];
            int i = 0;

            // copy regular arguments
            while (i < argCount - 1)
            {
                values[i] = argValues[i];
                i++;
            }

            // package param array into last argument
            Array paramArray = Array.CreateInstance(elementType, argValues.Length - i);
            int j = 0;
            while (i < argValues.Length)
            {
                paramArray.SetValue(argValues[i++], j++);
            }
            values[values.Length - 1] = paramArray;

            return values;
        }
    }
}
