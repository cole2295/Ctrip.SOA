using System.Reflection;

namespace Ctrip.SOA.Infratructure.Reflection.Dynamic
{
    /// <summary>
    /// Safe wrapper for the dynamic constructor.
    /// </summary>
    /// <remarks>
    /// <see cref="SafeConstructor"/> will attempt to use dynamic
    /// constructor if possible, but it will fall back to standard
    /// reflection if necessary.
    /// </remarks>
    public class SafeConstructor : IDynamicConstructor
    {
        private ConstructorInfo _constructorInfo;
        private ConstructorDelegate _constructor;
        private bool _isOptimized = true;

        /// <summary>
        /// Creates a new instance of the safe constructor wrapper.
        /// </summary>
        /// <param name="constructorInfo">Constructor to wrap.</param>
        public SafeConstructor(ConstructorInfo constructorInfo)
        {
            _constructorInfo = constructorInfo;
            try
            {
                _constructor = DynamicReflectionManager.CreateConstructor(constructorInfo);
            }
            catch
            {
                _isOptimized = false;
            }
        }
        
        /// <summary>
        /// Invokes dynamic constructor.
        /// </summary>
        /// <param name="arguments">
        /// Constructor arguments.
        /// </param>
        /// <returns>
        /// A constructor value.
        /// </returns>
        public object Invoke(object[] arguments)
        {
            if (_isOptimized)
            {
                return _constructor(arguments);
            }
            else
            {
                return _constructorInfo.Invoke(arguments);
            }
        }
    }
}
