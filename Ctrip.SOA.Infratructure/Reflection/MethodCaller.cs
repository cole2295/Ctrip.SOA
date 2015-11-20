using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ctrip.SOA.Infratructure.Reflection.Dynamic;
using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Reflection
{
    /// <summary>
    /// Provides methods to dynamically find and call methods.
    /// </summary>
    public static class MethodCaller
    {
        #region Create Instance

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateInstance<T>()
            where T : class
        {
            object[] parameters = null;
            return CreateInstance<T>(parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T CreateInstance<T>(params object[] parameters)
            where T : class
        {
            return CreateInstance(typeof(T), parameters) as T;
        }

        /// <summary>
        /// Uses reflection to create an object using its 
        /// default constructor.
        /// </summary>
        /// <param name="objectType">Type of object to create.</param>
        public static object CreateInstance(Type objectType)
        {
            IDynamicConstructor ctor = DynamicConstructorCache.GetDynamicConstructor(objectType, Type.EmptyTypes);
            if (ctor == null)
                ThrowHelper.ThrowNotImplementedException(ReflectionSR.DefaultConstructorMethodNotImplemented, objectType.FullName);

            return ctor.Invoke(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object CreateInstance(Type objectType, params object[] parameters)
        {
            IDynamicConstructor ctor = DynamicConstructorCache.GetDynamicConstructor(objectType, parameters);
            if (ctor == null)
                ThrowHelper.ThrowNotImplementedException(ReflectionSR.ConstructorNotImplemented, objectType.FullName, TypeHelper.GetTypesString(parameters));

            return ctor.Invoke(parameters);
        }

        public static T CreateInstance<T>(IDictionary<string, object> namedArgValues) where T : class
        {
            return CreateInstance(typeof(T), namedArgValues) as T;
        }

        public static object CreateInstance(Type objectType, IDictionary<string, object> namedArgValues)
        {
            object target = null;
            IDynamicConstructor ctor = DynamicConstructorCache.GetDynamicConstructor(objectType, namedArgValues);
            if (ctor != null)
            {
                target = ctor.Invoke(namedArgValues.Values.ToArray());
            }

            return target;
        }

        #endregion

        #region Dynamic Property

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IDynamicProperty GetProperty(object objectType, string propertyName)
        {
            return DynamicPropertyCache.GetDynamicProperty(objectType.GetType(), propertyName);
        }

        /// <summary>
        /// Invokes a property getter using dynamic
        /// method invocation.
        /// </summary>
        /// <param name="obj">Target object.</param>
        /// <param name="property">Property to invoke.</param>
        /// <returns></returns>
        public static object CallPropertyGetter(object objectType, string property)
        {
            IDynamicProperty dynamicProperty = DynamicPropertyCache.GetDynamicProperty(objectType.GetType(), property);
            return dynamicProperty.GetValue(objectType);
        }

        /// <summary>
        /// Invokes a property setter using dynamic
        /// method invocation.
        /// </summary>
        /// <param name="obj">Target object.</param>
        /// <param name="property">Property to invoke.</param>
        /// <param name="value">New value for property.</param>
        public static void CallPropertySetter(object objectType, string property, object value)
        {
            IDynamicProperty dynamicProperty = DynamicPropertyCache.GetDynamicProperty(objectType.GetType(), property);
            dynamicProperty.SetValue(objectType, value);
        }

        #endregion

        #region Call Method

        /// <summary>
        /// Uses reflection to dynamically invoke a method
        /// if that method is implemented on the target object.
        /// </summary>
        /// <param name="obj">
        /// Object containing method.
        /// </param>
        /// <param name="method">
        /// Name of the method.
        /// </param>
        /// <param name="parameters">
        /// Parameters to pass to method.
        /// </param>
        public static object CallMethodIfImplemented(object obj, string method, params object[] parameters)
        {
            Guard.ArgumentNotNull(obj, "obj");
            Guard.ArgumentNotNullOrEmpty(method, "method");

            IDynamicMethod dynamicMethod = DynamicMethodCache.GetDynamicMethod(obj.GetType(), method, parameters);
            if (dynamicMethod == null)
                return null;

            object result = null;
            try
            {
                result = dynamicMethod.Invoke(obj, parameters);
            }
            catch (Exception ex)
            {
                throw new CallMethodException(
                    string.Format(ReflectionSR.Culture, ReflectionSR.MethodCallFailed, obj.GetType().FullName, method), ex);
            }

            return result;
        }

        /// <summary>
        /// Uses reflection to dynamically invoke a method,
        /// throwing an exception if it is not
        /// implemented on the target object.
        /// </summary>
        /// <param name="obj">
        /// Object containing method.
        /// </param>
        /// <param name="method">
        /// Name of the method.
        /// </param>
        /// <param name="parameters">
        /// Parameters to pass to method.
        /// </param>
        public static object CallMethod(object obj, string method, params object[] parameters)
        {
            Guard.ArgumentNotNull(obj, "obj");
            Guard.ArgumentNotNullOrEmpty(method, "method");

            IDynamicMethod dynamicMethod = DynamicMethodCache.GetDynamicMethod(obj.GetType(), method, parameters);
            if (dynamicMethod == null)
            {
                ThrowHelper.ThrowNotImplementedException(
                    ReflectionSR.MethodNotImplemented, obj.GetType().FullName, method, TypeHelper.GetTypesString(parameters));
            }

            object result = null;
            try
            {
                result = dynamicMethod.Invoke(obj, parameters);
            }
            catch (Exception ex)
            {
                throw new CallMethodException(
                    string.Format(ReflectionSR.Culture, ReflectionSR.MethodCallFailed, obj.GetType().FullName, method), ex);
            }

            return result;
        }

        #endregion
    }
}