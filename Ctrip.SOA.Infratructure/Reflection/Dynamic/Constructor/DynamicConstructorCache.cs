using System;
using System.Collections.Generic;
using System.Reflection;
using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Reflection.Dynamic
{
    public class DynamicConstructorCache
    {
        private static readonly Dictionary<MethodCacheKey, IDynamicConstructor> _dynamicConstructors
            = new Dictionary<MethodCacheKey, IDynamicConstructor>();
        private static readonly Dictionary<DynamicConstructorCacheKey, IDynamicConstructor> _namedArgumentDynamicConstructors
            = new Dictionary<DynamicConstructorCacheKey, IDynamicConstructor>();
        private static object _syncObj = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IDynamicConstructor GetDynamicConstructor<T>(params object[] parameters)
            where T : class
        {
            return GetDynamicConstructor(typeof(T), parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IDynamicConstructor GetDynamicConstructor<T>(params Type[] parameterTypes)
            where T : class
        {
            return GetDynamicConstructor(typeof(T), parameterTypes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IDynamicConstructor GetDynamicConstructor(Type objectType, params object[] parameters)
        {
            Type[] parameterTypes = TypeHelper.GetParameterTypes(parameters);
            return GetDynamicConstructor(objectType, parameterTypes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="injectedParameters"></param>
        /// <returns></returns>
        public static IDynamicConstructor GetDynamicConstructor(Type objectType, InjectedParameterCollection injectedParameters)
        {
            List<Type> parameterTypeList = new List<Type>();
            foreach (InjectedParameter parameter in injectedParameters)
            {
                parameterTypeList.Add(parameter.Type);
            }

            Type[] parameterTypes = parameterTypeList.ToArray();
            return GetDynamicConstructor(objectType, parameterTypes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IDynamicConstructor GetDynamicConstructor(Type objectType, params Type[] parameterTypes)
        {
            if (parameterTypes == null) parameterTypes = Type.EmptyTypes;
            MethodCacheKey key = MethodCacheKey.Create(objectType.FullName, objectType.Name, parameterTypes);
            IDynamicConstructor dynamicConstructor = null;
            if (!_dynamicConstructors.TryGetValue(key, out dynamicConstructor))
            {
                lock (_syncObj)
                {
                    if (!_dynamicConstructors.TryGetValue(key, out dynamicConstructor))
                    {
                        ConstructorInfo ctor = objectType.GetConstructor(parameterTypes);
                        if (ctor != null)
                        {
                            dynamicConstructor = DynamicConstructor.Create(ctor);
                            if (dynamicConstructor != null)
                            {
                                _dynamicConstructors.Add(key, dynamicConstructor);
                            }
                        }
                    }
                }
            }

            return dynamicConstructor;
        }

        public static IDynamicConstructor GetDynamicConstructor(Type objectType, IDictionary<string, object> namedArgValues)
        {
            IDictionary<string, object> namedParamValues = namedArgValues == null ? new Dictionary<string, object>() : namedArgValues;
            DynamicConstructorCacheKey key = new DynamicConstructorCacheKey(objectType, namedParamValues);
            IDynamicConstructor dynamicConstructor = null;
            if (!_namedArgumentDynamicConstructors.TryGetValue(key, out dynamicConstructor))
            {
                lock (_syncObj)
                {
                    if (!_namedArgumentDynamicConstructors.TryGetValue(key, out dynamicConstructor))
                    {
                        ConstructorInfo constructorInfo = ReflectionHelper.GetConstructorByNamedArgumentValues(objectType, namedArgValues);
                        dynamicConstructor = DynamicConstructor.Create(constructorInfo);
                        
                        if (dynamicConstructor != null)
                        {
                            _namedArgumentDynamicConstructors.Add(key, dynamicConstructor);
                        }
                    }
                }
            }

            return dynamicConstructor;
        }
    }
}