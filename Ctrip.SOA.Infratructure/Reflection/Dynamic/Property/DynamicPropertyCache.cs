using System;
using System.Collections.Generic;
using System.Reflection;
using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Reflection.Dynamic
{
    internal class DynamicPropertyCache
    {
        private const BindingFlags propertyFlags = 
            BindingFlags.Public | BindingFlags.NonPublic | 
            BindingFlags.Instance | BindingFlags.FlattenHierarchy;

        private static readonly Dictionary<MethodCacheKey, IDynamicProperty> _dynamicProperties
            = new Dictionary<MethodCacheKey, IDynamicProperty>();
        private static object _syncObj = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IDynamicProperty GetDynamicProperty<TObject>(string propertyName)
            where TObject : class
        {
            return GetDynamicProperty(typeof(TObject), propertyName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IDynamicProperty GetDynamicProperty(Type objectType, string propertyName)
        {
            Guard.ArgumentNotNull(objectType, "objectType");
            Guard.ArgumentNotNullOrEmpty(propertyName, "propertyName");

            MethodCacheKey key = MethodCacheKey.Create(objectType.FullName, propertyName, TypeHelper.GetParameterTypes());
            IDynamicProperty dynamicProperty = null;
            if (!_dynamicProperties.TryGetValue(key, out dynamicProperty))
            {
                lock (_syncObj)
                {
                    if (!_dynamicProperties.TryGetValue(key, out dynamicProperty))
                    {
                        PropertyInfo propertyInfo = objectType.GetProperty(propertyName, propertyFlags);
                        if (propertyInfo == null)
                        {
                            ThrowHelper.ThrowInvalidOperationException(ReflectionSR.PropertyNotFound, objectType.FullName, propertyName);
                        }

                        dynamicProperty = DynamicProperty.Create(propertyInfo);
                        _dynamicProperties.Add(key, dynamicProperty);
                    }
                }
            }

            return dynamicProperty;
        }

        public static IDynamicProperty GetDynamicProperty(Type objType, PropertyInfo propertyInfo)
        {
            return null;
        }
    }
}