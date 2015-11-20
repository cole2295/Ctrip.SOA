using System;
using Ctrip.SOA.Infratructure.Serialization.Xml;
using Ctrip.SOA.Infratructure.Serialization.Json;
using Ctrip.SOA.Infratructure.Serialization.Support;

namespace Ctrip.SOA.Infratructure.Serialization
{
	public class Serializer
	{
        private static readonly ProtoBufSerializer _protobufSerializer = new ProtoBufSerializer();

		public static ISerializer GetSerializer(string serializationMode)
		{
			SerializationMode mode = SerializationMode.Xml;
            mode = (SerializationMode)Enum.Parse(typeof(SerializationMode), serializationMode);
			return GetSerializer(mode);
		}

		public static ISerializer GetSerializer(SerializationMode mode)
		{
			switch(mode)
			{
				case SerializationMode.Xml:
					return Serializer.Xml;
                case SerializationMode.DataContractJson:
                    return Serializer.DataContractJson;
                case SerializationMode.NewtonsoftJson:
                    return Serializer.NewtonsoftJson;
				default:
					throw new NotSupportedException();
			}
		}

		public static IXmlSerializer Xml
		{
			get
			{
				return XmlSerializerWrapper.GetInstance();
			}
		}

        public static ISerializer DataContractJson
        {
            get
            {
                return DataContractJsonSerializerWrapper.GetInstance();
            }
        }

        public static ISerializer NewtonsoftJson
        {
            get
            {
                return NewtonsoftJsonSerializer.GetInstance();
            }
        }

        public static IProtobufSerializer ProtoBuf
        {
            get
            {
                return _protobufSerializer;
            }
        }
	}
}