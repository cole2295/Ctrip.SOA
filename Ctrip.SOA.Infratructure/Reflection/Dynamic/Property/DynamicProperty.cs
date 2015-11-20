using System.Reflection;
using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Reflection.Dynamic
{    
    /// <summary>
    /// Factory class for dynamic properties.
    /// </summary>
    public class DynamicProperty
    {
        /// <summary>
        /// Creates dynamic property instance for the specified <see cref="PropertyInfo"/>.
        /// </summary>
        /// <param name="property">Property info to create dynamic property for.</param>
        /// <returns>Dynamic property for the specified <see cref="PropertyInfo"/>.</returns>
        public static IDynamicProperty Create(PropertyInfo property)
        {
            Guard.ArgumentNotNull(property, "property", "You cannot create a dynamic property for a null value.");

            return new SafeProperty(property);
        }
    }
}