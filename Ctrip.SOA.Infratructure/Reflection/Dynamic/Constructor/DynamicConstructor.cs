using System.Reflection;
using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Reflection.Dynamic
{
    /// <summary>
    /// Factory class for dynamic constructors.
    /// </summary>
    public class DynamicConstructor
    {
        /// <summary>
        /// Creates dynamic constructor instance for the specified <see cref="ConstructorInfo"/>.
        /// </summary>
        /// <param name="constructorInfo">Constructor info to create dynamic constructor for.</param>
        /// <returns>Dynamic constructor for the specified <see cref="ConstructorInfo"/>.</returns>
        public static IDynamicConstructor Create(ConstructorInfo constructorInfo)
        {
            Guard.ArgumentNotNull(constructorInfo, "You cannot create a dynamic constructor for a null value.");

            return new SafeConstructor(constructorInfo);
        }
    }
}
