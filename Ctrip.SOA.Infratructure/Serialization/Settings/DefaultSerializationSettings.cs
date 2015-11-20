using System.Threading;
using Ctrip.SOA.Infratructure.Threading;

namespace Ctrip.SOA.Infratructure.Serialization
{
    public class DefaultSerializationSettings : ISerializationSettings
    {
        private static ThreadLocal<bool> _indent = new ThreadLocal<bool>(() => false);
        private static ThreadStaticStorage _store = new ThreadStaticStorage();
        private const string IndentKey = "SerializationSettings_Indent";

        public bool Indent
        {
            get
            {
                return _indent.Value;
            }
            set
            {
                _indent.Value = value;
            }
        }
    }
}