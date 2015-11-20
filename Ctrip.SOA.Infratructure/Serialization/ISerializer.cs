using System;
using System.IO;
using System.Text;

namespace Ctrip.SOA.Infratructure.Serialization
{
    public interface ISerializer : IProtobufSerializer
	{
		// SerializeToStream
        void SerializeToStream<T>(Stream serializationStream, T obj);
        
        void SerializeToStream(Stream serializationStream, object obj);
		void SerializeToStream(Stream serializationStream, object obj, Encoding encoding);
		void SerializeToStream(Stream serializationStream, object obj, Type[] knownTypes);
		void SerializeToStream(Stream serializationStream, object obj, Type[] knownTypes, Encoding encoding);

		// SerializeToString
        string SerializeToString<T>(T obj);
        
        string SerializeToString(object obj);
		string SerializeToString(object obj, Encoding encoding);
		string SerializeToString(object obj, Type[] knownTypes);
		string SerializeToString(object obj, Type[] knownTypes, Encoding encoding);

		// SerializeToBytes
        byte[] SerializeToBytes<T>(T obj);

        byte[] SerializeToBytes(object obj);
		byte[] SerializeToBytes(object obj, Encoding encoding);
		byte[] SerializeToBytes(object obj, Type[] knownTypes);
		byte[] SerializeToBytes(object obj, Type[] knownTypes, Encoding encoding);

		// SerializeToFile
		void SerializeToFile(object obj, string fileName);
		void SerializeToFile(object obj, string fileName, Encoding encoding);
		void SerializeToFile(object obj, Type[] knownTypes, string fileName);
		void SerializeToFile(object obj, Type[] knownTypes, string fileName, Encoding encoding);

		// DeserializeFromStream
		T DeserializeFromStream<T>(Stream serializationStream);
		T DeserializeFromStream<T>(Stream serializationStream, Type[] knownTypes);

        object DeserializeFromStream(Type type, Stream serializationStream);
        object DeserializeFromStream(Type type, Stream serializationStream, Type[] knownTypes);

		// DeserializeFromBytes
		T DeserializeFromBytes<T>(byte[] bytes);
		T DeserializeFromBytes<T>(byte[] bytes, Type[] knownTypes);

        object DeserializeFromBytes(Type type, byte[] bytes);
        object DeserializeFromBytes(Type type, byte[] bytes, Type[] knownTypes);

		// DeserializeFromString
		T DeserializeFromString<T>(string serializedString);
		T DeserializeFromString<T>(string serializedString, Type[] knownTypes);
		T DeserializeFromString<T>(string serializedString, Encoding encoding);
		T DeserializeFromString<T>(string serializedString, Type[] knownTypes, Encoding encoding);

        T DeserializeFromString<T>(Type type, string serializedString);
        T DeserializeFromString<T>(Type type, string serializedString, Type[] knownTypes);
        T DeserializeFromString<T>(Type type, string serializedString, Encoding encoding);
        T DeserializeFromString<T>(Type type, string serializedString, Type[] knownTypes, Encoding encoding);

        object DeserializeFromString(Type type, string serializedString);
        object DeserializeFromString(Type type, string serializedString, Type[] knownTypes);
        object DeserializeFromString(Type type, string serializedString, Encoding encoding);
        object DeserializeFromString(Type type, string serializedString, Type[] knownTypes, Encoding encoding);

		// DeserializeFromFile
		T DeserializeFromFile<T>(string fileName);
		T DeserializeFromFile<T>(string fileName, Type[] knownTypes);
        T DeserializeFromFile<T>(string fileName, Encoding encoding);
        T DeserializeFromFile<T>(string fileName, Type[] knownTypes, Encoding encoding);

        object DeserializeFromFile(Type type, string fileName);
        object DeserializeFromFile(Type type, string fileName, Type[] knownTypes);
        object DeserializeFromFile(Type type, string fileName, Encoding encoding);
        object DeserializeFromFile(Type type, string fileName, Type[] knownTypes, Encoding encoding);
	}
}