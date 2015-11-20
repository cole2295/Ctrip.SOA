using System;

namespace Ctrip.SOA.Infratructure.Reflection.Dynamic
{
    /// <summary>
    /// Defines methods that dynamic method class has to implement.
    /// </summary>
    public interface IDynamicMethod
    {
        /// <summary>
        /// ·½·¨Ãû
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Invokes dynamic method on the specified target object.
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
        object Invoke(object target, params object[] arguments);
    }
}