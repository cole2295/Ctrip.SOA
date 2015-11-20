using System.IO;

namespace Ctrip.SOA.Infratructure.Serialization
{
    public interface IProtobufSerializer
    {
        void SerializeToStream<T>(Stream serializationStream, T obj);        
        byte[] SerializeToBytes<T>(T obj);
        string SerializeToString<T>(T obj);

        T DeserializeFromStream<T>(Stream serializationStream);
        T DeserializeFromBytes<T>(byte[] bytes);
        T DeserializeFromString<T>(string serializedString);
    }
}