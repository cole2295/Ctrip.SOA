using System;
using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Threading
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ThreadContext
    {
        private static IThreadStorage threadStorage = new HybirdContextStorage();

        /// <summary>
        /// Set the new <see cref="IThreadStorage"/> strategy.
        /// </summary>
        public static void SetStorage(IThreadStorage storage)
        {
            Guard.ArgumentNotNull(storage, "storage");
            threadStorage = storage;
        }

        private ThreadContext()
        {
            throw new NotSupportedException("must not be instantiated");
        }

        public static T GetData<T>(string name)
        {
            return threadStorage.GetData<T>(name);
        }

        public static object GetData(string name)
        {
            return threadStorage.GetData(name);
        }

        public static void SetData(string name, object value)
        {
            threadStorage.SetData(name, value);
        }

        public static void RemoveData(string name)
        {
            threadStorage.RemoveData(name);
        }
    }
}