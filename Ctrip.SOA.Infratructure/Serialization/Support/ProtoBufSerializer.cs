using System;
using System.IO;
using System.Text;
using ProtoBuf;

namespace Ctrip.SOA.Infratructure.Serialization.Support
{
    internal class ProtoBufSerializer : SerializerBase
    {
        public override void SerializeToStream(Stream serializationStream, object obj, Type[] knownTypes, Encoding encoding)
        {
            ProtoBuf.Serializer.Serialize(serializationStream, obj);
        }

        public override string SerializeToString(object obj, Type[] knownTypes, Encoding encoding)
        {
            byte[] bytes = SerializeToBytes(obj);
            return Convert.ToBase64String(bytes);
        }

        public override object DeserializeFromStream(Type type, Stream serializationStream, Type[] knownTypes)
        {
            return ProtoBuf.Serializer.NonGeneric.Deserialize(type, serializationStream);
        }

        public override T DeserializeFromStream<T>(Stream serializationStream, Type[] knownTypes)
        {
            return ProtoBuf.Serializer.Deserialize<T>(serializationStream);
        }

        public override object DeserializeFromString(Type type, string serializedString, Type[] knownTypes, Encoding encoding)
        {
            if (string.IsNullOrEmpty(serializedString))
            {
                return null;
            }

            byte[] bytes = Convert.FromBase64String(serializedString);
            return DeserializeFromBytes(type, bytes);
        }

        public override T DeserializeFromString<T>(Type type, string serializedString, Type[] knownTypes, Encoding encoding)
        {
            if (string.IsNullOrEmpty(serializedString))
            {
                return default(T);
            }

            byte[] bytes = Convert.FromBase64String(serializedString);
            return DeserializeFromBytes<T>(bytes);
        }
    }
}