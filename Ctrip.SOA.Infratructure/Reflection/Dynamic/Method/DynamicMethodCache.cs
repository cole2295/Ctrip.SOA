using System;
using System.Collections.Generic;
using System.Reflection;

using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Reflection.Dynamic
{
	/// <summary>
	/// 
	/// </summary>
	internal class DynamicMethodCache
	{
		private const BindingFlags allLevelFlags = BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
		private const BindingFlags oneLevelFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
		
		private static readonly Dictionary<MethodCacheKey, IDynamicMethod> _dynamicMethods
			= new Dictionary<MethodCacheKey, IDynamicMethod>();

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static IDynamicMethod GetDynamicMethod<T>(string methodName, params object[] parameters)
			where T : class
		{
			return GetDynamicMethod(typeof(T), methodName, parameters);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="objectType"></param>
		/// <param name="types"></param>
		/// <returns></returns>
		public static IDynamicMethod GetDynamicMethod(Type objectType, string methodName, params object[] parameters)
		{
			MethodCacheKey key = MethodCacheKey.Create(objectType, methodName, parameters);
			IDynamicMethod dynamicMethod = null;
			if (!_dynamicMethods.TryGetValue(key, out dynamicMethod))
			{
				lock (_dynamicMethods)
				{
					if (!_dynamicMethods.TryGetValue(key, out dynamicMethod))
					{
						MethodInfo methodInfo = GetMethod(objectType, methodName, parameters);
						if (methodInfo != null)
						{
							dynamicMethod = DynamicMethod.Create(methodInfo);
							if (dynamicMethod != null)
							{
								_dynamicMethods.Add(key, dynamicMethod);
							}
						}
					}
				}
			}

			return dynamicMethod;
		}

		#region Get/Find Method

		public static MethodInfo GetMethod(Type objectType, string method, params object[] parameters)
		{
			return GetMethod(objectType, method, true, parameters);
		}

		/// <summary>
		/// Uses reflection to locate a matching method
		/// on the target object.
		/// </summary>
		/// <param name="objectType">
		/// Type of object containing method.
		/// </param>
		/// <param name="method">
		/// Name of the method.
		/// </param>
		/// <param name="parameters">
		/// Parameters to pass to method.
		/// </param>
		private static MethodInfo GetMethod(Type objectType, string method, bool hasParameters, params object[] parameters)
		{
			MethodInfo result = null;

			object[] inParams = null;
            if (!hasParameters)
                inParams = new object[] { };
			else if (parameters == null)
				inParams = new object[] { null };
			else
				inParams = parameters;

			// try to find a strongly typed match

			// first see if there's a matching method
			// where all params match types
            Type[] parameterTypes = TypeHelper.GetParameterTypes(inParams);
            result = FindMethod(objectType, method, parameterTypes);

			if (result == null)
			{
				// no match found - so look for any method
				// with the right number of parameters
				try
				{
					result = FindMethod(objectType, method, inParams.Length);
				}
				catch (AmbiguousMatchException)
				{
					// we have multiple methods matching by name and parameter count
					result = FindMethodUsingFuzzyMatching(objectType, method, inParams);
				}
			}

			// no strongly typed match found, get default based on name only
			if (result == null)
			{
				result = objectType.GetMethod(method, allLevelFlags);
			}
			return result;
		}

		private static MethodInfo FindMethodUsingFuzzyMatching(Type objectType, string method, object[] parameters)
		{
			MethodInfo result = null;
			Type currentType = objectType;
			do
			{
				MethodInfo[] methods = currentType.GetMethods(oneLevelFlags);
				int parameterCount = parameters.Length;
				// Match based on name and parameter types and parameter arrays
				foreach (MethodInfo m in methods)
				{
					if (m.Name == method)
					{
						ParameterInfo[] infoParams = m.GetParameters();
						int pCount = infoParams.Length;
                        if (pCount > 0)
                        {
                            if (pCount == 1 && infoParams[0].ParameterType.IsArray)
                            {
                                // only param is an array
                                if (parameters.GetType().Equals(infoParams[0].ParameterType))
                                {
                                    // got a match so use it
                                    result = m;
                                    break;
                                }
                            }
                            else if (infoParams[pCount - 1].GetCustomAttributes(typeof(ParamArrayAttribute), true).Length > 0)
                            {
                                // last param is a param array
                                if (parameterCount == pCount)
                                {
                                    bool matched = true;
                                    for (int i = 0; i <= pCount - 2; i++)
                                    {
                                        var param = parameters[i];
                                        if (param != null)
                                        {
                                            Type paramType = param.GetType();
                                            Type infoParamType = infoParams[i].ParameterType;
                                            if (infoParamType.IsArray)
                                            {
                                                if (infoParamType != paramType)
                                                {
                                                    matched = false;
                                                }
                                            }
                                            else
                                            {
                                                if (!infoParamType.IsInstanceOfType(paramType))
                                                {
                                                    matched = false;
                                                }
                                            }
                                        }
                                    }
                                    
                                    if (!parameters[pCount - 1].GetType().Equals(infoParams[pCount - 1].ParameterType))
                                    {
                                        matched = false;                                        
                                    }

                                    if (matched)
                                    {
                                        // got a match so use it
                                        result = m;
                                        break;
                                    }
                                }
                            }
                        }
					}
				}

				if (result != null)
					break;
				currentType = currentType.BaseType;
			} while (currentType != null);
            
			return result;
		}

		/// <summary>
		/// Returns information about the specified
		/// method, even if the parameter types are
		/// generic and are located in an abstract
		/// generic base class.
		/// </summary>
		/// <param name="objectType">
		/// Type of object containing method.
		/// </param>
		/// <param name="method">
		/// Name of the method.
		/// </param>
		/// <param name="types">
		/// Parameter types to pass to method.
		/// </param>
		public static MethodInfo FindMethod(Type objectType, string method, Type[] types)
		{
			MethodInfo info = null;
			do
			{
				// find for a strongly typed match
				info = objectType.GetMethod(method, oneLevelFlags, null, types, null);
				if (info != null)
				{
					break; // match found
				}

				objectType = objectType.BaseType;
			} while (objectType != null);

			return info;
		}

		/// <summary>
		/// Returns information about the specified
		/// method, finding the method based purely
		/// on the method name and number of parameters.
		/// </summary>
		/// <param name="objectType">
		/// Type of object containing method.
		/// </param>
		/// <param name="method">
		/// Name of the method.
		/// </param>
		/// <param name="parameterCount">
		/// Number of parameters to pass to method.
		/// </param>
		public static MethodInfo FindMethod(Type objectType, string method, int parameterCount)
		{
			// walk up the inheritance hierarchy looking
			// for a method with the right number of
			// parameters
			MethodInfo result = null;
			Type currentType = objectType;
			do
			{
				MethodInfo info = currentType.GetMethod(method, oneLevelFlags);
				if (info != null)
				{
					ParameterInfo[] infoParams = info.GetParameters();
					int pCount = infoParams.Length;
					if (pCount > 0 &&
					   ((pCount == 1 && infoParams[0].ParameterType.IsArray) ||
					   (infoParams[pCount - 1].GetCustomAttributes(typeof(ParamArrayAttribute), true).Length > 0)))
					{
						// last param is a param array or only param is an array
						if (parameterCount >= pCount - 1)
						{
							// got a match so use it
							result = info;
							break;
						}
					}
					else if (pCount == parameterCount)
					{
						// got a match so use it
						result = info;
						break;
					}
				}
				currentType = currentType.BaseType;
			} while (currentType != null);

			return result;
		}

		#endregion
	}
}
