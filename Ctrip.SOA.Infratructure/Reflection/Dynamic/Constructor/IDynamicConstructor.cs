namespace Ctrip.SOA.Infratructure.Reflection.Dynamic
{
    /// <summary>
    /// Defines constructors that dynamic constructor class has to implement.
    /// </summary>
    public interface IDynamicConstructor
    {
        /// <summary>
        /// Invokes dynamic constructor.
        /// </summary>
        /// <param name="arguments">
        /// Constructor arguments.
        /// </param>
        /// <returns>
        /// A constructor value.
        /// </returns>
        object Invoke(object[] arguments);
    }
}