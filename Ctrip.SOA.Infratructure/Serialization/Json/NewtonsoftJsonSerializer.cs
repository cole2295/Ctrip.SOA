using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters;

namespace Ctrip.SOA.Infratructure.Serialization.Json
{
    public class NewtonsoftJsonSerializer : SerializerBase
    {
        private static NewtonsoftJsonSerializer s_SerializerWrapper = new NewtonsoftJsonSerializer();

        public static NewtonsoftJsonSerializer GetInstance()
        {
            return s_SerializerWrapper;
        }

        #region Serialize

        public override string SerializeToString(object obj, Type[] knownTypes, Encoding encoding)
        {
            if (SerializationSettings.Indent == true)
            {
                return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.None,
                    TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
                });
            }

            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.None,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            });
        }

        public override byte[] SerializeToBytes(object obj, Type[] knownTypes, Encoding encoding)
        {
            string serializedString = SerializeToString(obj, knownTypes, encoding);
            return encoding.GetBytes(serializedString);
        }

        public override void SerializeToFile(object obj, Type[] knownTypes, string fileName, Encoding encoding)
        {
            byte[] serializedBytes = SerializeToBytes(obj, knownTypes, encoding);
            using (FileStream stream = File.Create(fileName))
            {
                stream.Write(serializedBytes, 0, serializedBytes.Length);
            }
        }

        #endregion

        #region Deserialize

        public override object DeserializeFromStream(Type type, Stream serializationStream, Type[] knownTypes)
        {
            string serializedString = null;
            using (StreamReader sr = new StreamReader(serializationStream))
            {
                serializedString = sr.ReadToEnd();
            }

            return DeserializeFromString(type, serializedString, knownTypes);
        }

        public override object DeserializeFromString(Type type, string serializedString, Type[] knownTypes, Encoding encoding)
        {
            return JsonConvert.DeserializeObject(serializedString, type, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            });
        }

        public override object DeserializeFromBytes(Type type, byte[] bytes, Type[] knownTypes)
        {
            string serializedString = null;
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    serializedString = sr.ReadToEnd();
                }
            }

            return DeserializeFromString(type, serializedString, knownTypes);
        }

        public override object DeserializeFromFile(Type type, string fileName, Type[] knownTypes, Encoding encoding)
        {
            string serializedString = null;
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    serializedString = sr.ReadToEnd();
                }
            }

            return DeserializeFromString(type, serializedString, knownTypes);
        }

        #endregion
    }
}