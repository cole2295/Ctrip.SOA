using System;
using System.Reflection;

using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Reflection.Dynamic
{
    /// <summary>
    /// Safe wrapper for the dynamic method.
    /// </summary>
    /// <remarks>
    /// <see cref="SafeMethod"/> will attempt to use dynamic
    /// method if possible, but it will fall back to standard
    /// reflection if necessary.
    /// </remarks>
    public class SafeMethod : IDynamicMethod
    {
        private readonly MethodInfo methodInfo;
        private bool hasFinalArrayParam;
        private int methodParamsLength;
        private Type finalArrayElementType;

        /// <summary>
        /// Gets the class, that declares this method
        /// </summary>
        public Type DeclaringType
        {
            get { return methodInfo.DeclaringType; }
        }

        public bool HasFinalArrayParam { get { return hasFinalArrayParam; } }
        public int MethodParamsLength { get { return methodParamsLength; } }
        public Type FinalArrayElementType { get { return finalArrayElementType; } }

        private readonly MethodDelegate method;

        /// <summary>
        /// Creates a new instance of the safe method wrapper.
        /// </summary>
        /// <param name="methodInfo">Method to wrap.</param>
        public SafeMethod(MethodInfo methodInfo)
        {
            Guard.ArgumentNotNull(methodInfo, "methodInfo", "You cannot create a dynamic method for a null value.");

            this.methodInfo = methodInfo;
            this.Name = methodInfo.Name;
            ParameterInfo[] infoParams = methodInfo.GetParameters();
            int pCount = infoParams.Length;
            if (pCount > 0 &&
               ((pCount == 1 && infoParams[0].ParameterType.IsArray) ||
               (infoParams[pCount - 1].GetCustomAttributes(typeof(ParamArrayAttribute), true).Length > 0)))
            {
                this.hasFinalArrayParam = true;
                this.methodParamsLength = pCount;
                this.finalArrayElementType = infoParams[pCount - 1].ParameterType;
            }

            this.method = DynamicReflectionManager.CreateMethod(methodInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Invokes dynamic method.
        /// </summary>
        /// <param name="target">
        /// Target object to invoke method on.
        /// </param>
        /// <param name="arguments">
        /// Method arguments.
        /// </param>
        /// <returns>
        /// A method return value.
        /// </returns>
        public object Invoke(object target, object[] arguments)
        {
            object[] inParams = null;
            if (arguments == null)
                inParams = new object[] { null };
            else
                inParams = arguments;

            if (this.HasFinalArrayParam)
            {
                int pCount = this.MethodParamsLength;
                var inCount = inParams.Length;
                if (inCount == pCount - 1)
                {
                    // no paramter was supplied for the param array
                    // copy items into new array with last entry null
                    object[] paramList = new object[pCount];
                    for (var pos = 0; pos <= pCount - 2; pos++)
                        paramList[pos] = arguments[pos];
                    paramList[paramList.Length - 1] = inParams.Length == 0 ? inParams : null;

                    // use new array
                    inParams = paramList;
                }
                else if ((inCount == pCount && inParams[inCount - 1] != null && !inParams[inCount - 1].GetType().IsArray) || inCount > pCount)
                {
                    // last param is a param array or only param is an array
                    int extras = inParams.Length - (pCount - 1);

                    // 1 or more params go in the param array
                    // copy extras into an array
                    object[] extraArray = GetExtrasArray(extras, this.FinalArrayElementType);
                    for (int i = 0; i < extras; i++)
                    {
                        extraArray[i] = inParams[i + inParams.Length - extras];
                    }

                    // copy items into new array
                    object[] paramList = new object[pCount];
                    for (int pos = 0; pos <= pCount - 2; pos++)
                        paramList[pos] = arguments[pos];
                    paramList[paramList.Length - 1] = extraArray;

                    // use new array
                    inParams = paramList;
                } 
            }

            return this.method(target, inParams);
        }

        private static object[] GetExtrasArray(int count, Type arrayType)
        {
            return (object[])(System.Array.CreateInstance(arrayType.GetElementType(), count));
        }
    }
}
