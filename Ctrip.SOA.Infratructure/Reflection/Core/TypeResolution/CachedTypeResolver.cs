using System;
using System.Collections;
using System.Collections.Specialized;

using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.TypeResolution
{
    /// <summary>
    /// Resolves (instantiates) a <see cref="System.Type"/> by it's (possibly
    /// assembly qualified) name, and caches the <see cref="System.Type"/>
    /// instance against the type name.
    /// </summary>
	public class CachedTypeResolver : ITypeResolver
    {
        #region [ Fields ]

        /// <summary>
        /// The cache, mapping type names (<see cref="System.String"/> instances) against
        /// <see cref="System.Type"/> instances.
        /// </summary>
        private IDictionary _typeCache = new HybridDictionary();

        private ITypeResolver _typeResolver = null;

        #endregion

        #region [ Constructor (s) / Destructor ]

        /// <summary>
        /// Creates a new instance of the <see cref="IBatisNet.Common.Utilities.TypesResolver.CachedTypeResolver"/> class.
        /// </summary>
        /// <param name="typeResolver">
        /// The <see cref="IBatisNet.Common.Utilities.TypesResolver.ITypeResolver"/> that this instance will delegate
        /// actual <see cref="System.Type"/> resolution to if a <see cref="System.Type"/>
        /// cannot be found in this instance's <see cref="System.Type"/> cache.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// If the supplied <paramref name="typeResolver"/> is <see langword="null"/>.
        /// </exception>
        public CachedTypeResolver(ITypeResolver typeResolver)
        {
            _typeResolver = typeResolver;
        }

        #endregion

        #region [ ITypeResolver Members ]

        /// <summary>
        /// Resolves the supplied <paramref name="typeName"/> to a
        /// <see cref="System.Type"/>
        /// instance.
        /// </summary>
        /// <param name="typeName">
        /// The (possibly partially assembly qualified) name of a
        /// <see cref="System.Type"/>.
        /// </param>
        /// <returns>
        /// A resolved <see cref="System.Type"/> instance.
        /// </returns>
        /// <exception cref="System.TypeLoadException">
        /// If the supplied <paramref name="typeName"/> could not be resolved
        /// to a <see cref="System.Type"/>.
        /// </exception>
        public Type Resolve(string typeName)
        {
			Guard.ArgumentNotNullOrEmpty("typeName", typeName);

            Type type = null;
            try
            {
                type = _typeCache[typeName] as Type;
                if (type == null)
                {
					lock (_typeCache)
					{
						type = _typeCache[typeName] as Type;
						if (type == null)
						{
							type = _typeResolver.Resolve(typeName);
							_typeCache[typeName] = type;
						}
					}
                }
            }
            catch (Exception ex)
            {
                if (ex is TypeLoadException)
                {
                    throw;
                }

				ThrowHelper.ThrowTypeLoadExcetpion(ex, "¿‡–Õº”‘ÿ ß∞‹", typeName);
            }

            return type;
        }

        #endregion
    }
}