using System;
using System.Reflection;

using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.TypeResolution
{
	/// <summary>
	/// Resolves a <see cref="System.Type"/> by name.
	/// </summary>
	/// <remarks>
	/// <p>
	/// The rationale behind the creation of this class is to centralise the
	/// resolution of type names to <see cref="System.Type"/> instances beyond that
	/// offered by the plain vanilla System.Type.GetType method call.
	/// </p>
	/// </remarks>
	/// <version>$Id: TypeResolver.cs,v 1.5 2004/09/28 07:51:47 springboy Exp $</version>
    public class TypeResolver : ITypeResolver
	{
		#region [ Constants ]

		private const string NULLABLE_TYPE = "System.Nullable";

		#endregion

		#region ITypeResolver Members

		/// <summary>
        /// Resolves the supplied <paramref name="typeName"/> to a
        /// <see cref="System.Type"/> instance.
        /// </summary>
        /// <param name="typeName">
        /// The unresolved name of a <see cref="System.Type"/>.
        /// </param>
        /// <returns>
        /// A resolved <see cref="System.Type"/> instance.
        /// </returns>
        /// <exception cref="System.TypeLoadException">
        /// If the supplied <paramref name="typeName"/> could not be resolved
        /// to a <see cref="System.Type"/>.
        /// </exception>
        public virtual Type Resolve(string typeName)
        {
            Type type = ResolveGenericType(typeName.Replace(" ", string.Empty));
            if (type == null)
            {
                type = ResolveType(typeName.Replace(" ", string.Empty));
            }
            return type;
        }

        #endregion

        /// <summary>
        /// Resolves the supplied generic <paramref name="typeName"/>,
        /// substituting recursively all its type parameters., 
        /// to a <see cref="System.Type"/> instance.
        /// </summary>
        /// <param name="typeName">
        /// The (possibly generic) name of a <see cref="System.Type"/>.
        /// </param>
        /// <returns>
        /// A resolved <see cref="System.Type"/> instance.
        /// </returns>
        /// <exception cref="System.TypeLoadException">
        /// If the supplied <paramref name="typeName"/> could not be resolved
        /// to a <see cref="System.Type"/>.
        /// </exception>
        private Type ResolveGenericType(string typeName)
        {
			Guard.ArgumentNotNullOrEmpty("typeName", typeName);

            if (typeName.StartsWith(NULLABLE_TYPE))
            {
                return null;
            }
            else
            {
                GenericArgumentsInfo genericInfo = new GenericArgumentsInfo(typeName);
                Type type = null;
                try
                {
                    if (genericInfo.ContainsGenericArguments)
                    {
						type = TypeHelper.ResolveType(genericInfo.GenericTypeName);
                        if (!genericInfo.IsGenericDefinition)
                        {
                            string[] unresolvedGenericArgs = genericInfo.GetGenericArguments();
                            Type[] genericArgs = new Type[unresolvedGenericArgs.Length];
                            for (int i = 0; i < unresolvedGenericArgs.Length; i++)
                            {
								genericArgs[i] = TypeHelper.ResolveType(unresolvedGenericArgs[i]);
                            }
                            type = type.MakeGenericType(genericArgs);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex is TypeLoadException)
                    {
                        throw;
                    }

					ThrowHelper.ThrowTypeLoadExcetpion(ex, "类型加载失败", typeName);
                }

                return type;
            }
        }

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
        private Type ResolveType(string typeName)
        {
			Guard.ArgumentNotNullOrEmpty("typeName", typeName);

            TypeAssemblyInfo typeInfo = new TypeAssemblyInfo(typeName);
            Type type = null;
            try
            {
                type = (typeInfo.IsAssemblyQualified) ?
                     LoadTypeDirectlyFromAssembly(typeInfo) :
                     LoadTypeByIteratingOverAllLoadedAssemblies(typeInfo);
            }
            catch (Exception ex)
            {
				ThrowHelper.ThrowTypeLoadExcetpion(ex, "类型加载失败", typeName);
            }

            if (type == null)
            {
				ThrowHelper.ThrowTypeLoadExcetpion("类型加载失败", typeName);
            }

            return type;
        }

        /// <summary>
        /// Uses <see cref="System.Reflection.Assembly.LoadWithPartialName(string)"/>
        /// to load an <see cref="System.Reflection.Assembly"/> and then the attendant
        /// <see cref="System.Type"/> referred to by the <paramref name="typeInfo"/>
        /// parameter.
        /// </summary>
        /// <remarks>
        /// <p>
        /// <see cref="System.Reflection.Assembly.LoadWithPartialName(string)"/> is
        /// deprecated in .NET 2.0, but is still used here (even when this class is
        /// compiled for .NET 2.0);
        /// <see cref="System.Reflection.Assembly.LoadWithPartialName(string)"/> will
        /// still resolve (non-.NET Framework) local assemblies when given only the
        /// display name of an assembly (the behaviour for .NET Framework assemblies
        /// and strongly named assemblies is documented in the docs for the
        /// <see cref="System.Reflection.Assembly.LoadWithPartialName(string)"/> method).
        /// </p>
        /// </remarks>
        /// <param name="typeInfo">
        /// The assembly and type to be loaded.
        /// </param>
        /// <returns>
        /// A <see cref="System.Type"/>, or <see lang="null"/>.
        /// </returns>
        /// <exception cref="System.Exception">
        /// <see cref="System.Reflection.Assembly.LoadWithPartialName(string)"/>
        /// </exception>
        private static Type LoadTypeDirectlyFromAssembly(TypeAssemblyInfo typeInfo)
        {
            Type type = null;
            // assembly qualified... load the assembly, then the Type
            Assembly assembly = Assembly.Load(typeInfo.AssemblyName);

            if (assembly != null)
            {
                type = assembly.GetType(typeInfo.TypeName, true, true);
            }
            return type;
        }

        /// <summary>
        /// Check all assembly
        /// to load the attendant <see cref="System.Type"/> referred to by 
        /// the <paramref name="typeInfo"/> parameter.
        /// </summary>
        /// <param name="typeInfo">
        /// The type to be loaded.
        /// </param>
        /// <returns>
        /// A <see cref="System.Type"/>, or <see lang="null"/>.
        /// </returns>
        private static Type LoadTypeByIteratingOverAllLoadedAssemblies(TypeAssemblyInfo typeInfo)
        {
            Type type = null;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                type = assembly.GetType(typeInfo.TypeName, false, false);
                if (type != null)
                {
                    break;
                }
            }
            return type;
        }
    }
}