using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Ctrip.SOA.Infratructure.Serialization.Xml
{
    public partial class XmlSerializerWrapper : XmlSerializerBase
    {
        private static XmlSerializerWrapper s_XmlSerializerWrapper = new XmlSerializerWrapper();

        public static XmlSerializerWrapper GetInstance()
        {
            return s_XmlSerializerWrapper;
        }

        #region [ SerializeToStream ]

        public override void SerializeToStream(Stream serializationStream, object obj, Type[] extraTypes, Encoding encoding)
        {
            XmlSerializer serializer = s_XmlSerializerCache.GetSerializer(obj.GetType(), extraTypes);
            serializer.Serialize(serializationStream, obj);
            serializationStream.Seek(0, SeekOrigin.Begin);
        }

        #endregion

        #region [ SerilaizeToString ]

        public override string SerializeToString(object obj, System.Type[] extraTypes, Encoding encoding)
        {
            string serializedString = string.Empty;
            if (obj != null)
            {
                XmlSerializer serializer = s_XmlSerializerCache.GetSerializer(obj.GetType(), extraTypes);
                serializedString = SerializeToString(serializer, obj, encoding);
            }
            
            return serializedString;
        }

        #endregion

        #region [ DeserializeFromStream ]

        public override object DeserializeFromStream(Type type, Stream serializationStream, Type[] extraTypes)
        {
            XmlSerializer serializer = s_XmlSerializerCache.GetSerializer(type, extraTypes);
            return serializer.Deserialize(serializationStream);
        }

        #endregion

        #region [ DeserializeFromString ]

        public override object DeserializeFromString(Type type, string serializedString, Type[] knownTypes, Encoding encoding)
        {
            XmlSerializer serializer = s_XmlSerializerCache.GetSerializer(type, knownTypes);
            object obj;
            using (StringReader reader = new StringReader(serializedString))
            {
                obj = serializer.Deserialize(reader);
            }

            return obj;
        }

        #endregion
    }
}