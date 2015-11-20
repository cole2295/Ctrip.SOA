using System;
using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Reflection
{
    internal class MethodCacheKey
    {
        public string TypeName { get; private set; }
        public string MethodName { get; private set; }
        public Type[] ParamTypes { get; private set; }
        internal int HashKey { get; set; }

        private MethodCacheKey()
        {
        }

        public static MethodCacheKey Create(Type type, string methodName, object[] parameters)
        {
            return Create(type.FullName, methodName, TypeHelper.GetParameterTypes(parameters));
        }

        public static MethodCacheKey Create(string typeName, string methodName, Type[] paramTypes)
        {
            MethodCacheKey key = new MethodCacheKey();
            key.TypeName = typeName;
            key.MethodName = methodName;
            key.ParamTypes = paramTypes;

            key.HashKey = key.TypeName.GetHashCode();
            key.HashKey = key.HashKey ^ key.MethodName.GetHashCode();
            foreach (Type item in key.ParamTypes)
                key.HashKey = key.HashKey ^ item.Name.GetHashCode();

            return key;
        }

        public override bool Equals(object obj)
        {
            MethodCacheKey key = obj as MethodCacheKey;
            if (key != null &&
                key.TypeName == this.TypeName &&
                key.MethodName == this.MethodName &&
                ArrayEquals(key.ParamTypes, this.ParamTypes))
                return true;

            return false;
        }

        private bool ArrayEquals(Type[] a1, Type[] a2)
        {
            if (a1.Length != a2.Length)
                return false;

            for (int pos = 0; pos < a1.Length; pos++)
                if (a1[pos] != a2[pos])
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            return this.HashKey;
        }
    }
}
