using System.Reflection;

using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Reflection.Dynamic
{   
    /// <summary>
    /// Factory class for dynamic methods.
    /// </summary>
    /// <author>Aleksandar Seovic</author>
    public class DynamicMethod
    {
        /// <summary>
        /// Creates dynamic method instance for the specified <see cref="MethodInfo"/>.
        /// </summary>
        /// <param name="method">Method info to create dynamic method for.</param>
        /// <returns>Dynamic method for the specified <see cref="MethodInfo"/>.</returns>
        public static IDynamicMethod Create(MethodInfo methodInfo)
        {
            Guard.ArgumentNotNull(methodInfo, "methodInfo", "You cannot create a dynamic method for a null value.");

            IDynamicMethod dynamicMethod = new SafeMethod(methodInfo);
            return dynamicMethod;
        }
    }
}
