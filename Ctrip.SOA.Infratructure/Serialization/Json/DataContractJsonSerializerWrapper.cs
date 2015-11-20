using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace Ctrip.SOA.Infratructure.Serialization.Json
{
	public class DataContractJsonSerializerWrapper : SerializerBase
	{
		private static DataContractJsonSerializerWrapper s_SerializerWrapper = new DataContractJsonSerializerWrapper();

		public static DataContractJsonSerializerWrapper GetInstance()
		{
			return s_SerializerWrapper;
		}

        public override void SerializeToStream(Stream serializationStream, object obj, Type[] knownTypes, Encoding encoding)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType(), knownTypes);
            XmlWriter writer = JsonReaderWriterFactory.CreateJsonWriter(serializationStream, encoding);
            // writer.Settings.Indent = SerializationSettings.Current.Indent;
            serializer.WriteObject(writer, obj);
            writer.Flush();
        }

        public override object DeserializeFromStream(Type type, Stream serializationStream, Type[] knownTypes)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(type, knownTypes);
            object obj = serializer.ReadObject(serializationStream);
            return obj;
        }
	}
}