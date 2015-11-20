using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Serialization
{
    public class SerializationSettings
    {
        private readonly static ISerializationSettings _defaultSettings = new DefaultSerializationSettings();
        private static ISerializationSettings _current = _defaultSettings;

        public static ISerializationSettings Current
        {
            get { return _current; }
        }

        /// <summary>
        /// 缩进标识。
        /// </summary>
        public static bool Indent
        {
            get { return _current.Indent; }
            set { _current.Indent = value; }
        }

        public void SetProvider(ISerializationSettings settings)
        {
            Guard.ArgumentNotNull(settings, "settings");

            _current = settings;
        }
    }
}