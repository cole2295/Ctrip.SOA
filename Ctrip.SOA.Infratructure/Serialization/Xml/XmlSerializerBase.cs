using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using Ctrip.SOA.Infratructure.IO;

namespace Ctrip.SOA.Infratructure.Serialization.Xml
{
	public abstract class XmlSerializerBase : SerializerBase, IXmlSerializer
	{
		protected static XmlSerializerCache s_XmlSerializerCache = new XmlSerializerCache();

        #region SeralizeToXmlTextWriter

        public void SerializeToXmlWriter(XmlWriter writer, object obj)
        {
            if (obj != null)
            {
                XmlSerializer serializer = s_XmlSerializerCache.GetSerializer(obj.GetType());
                XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
                xmlns.Add(string.Empty, string.Empty);
                serializer.Serialize(writer, obj, xmlns);
            }
        }

        #endregion

        #region [ SerializeToString ]

		protected string SerializeToString(XmlSerializer serializer, object obj, Encoding encoding)
		{
			StringBuilder sb = new StringBuilder();
			using (TextWriter writer = new EncodedStringWriter(sb, encoding))
			{
                XmlTextWriter xtw = new XmlTextWriter(writer);
                if (SerializationSettings.Current.Indent)
                {
                    xtw.Formatting = Formatting.Indented;
                    xtw.Indentation = 2;
                }
                XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
                xmlns.Add(string.Empty, string.Empty);
                serializer.Serialize(xtw, obj, xmlns);
			}

			return sb.ToString();
		}

		#endregion

        #region DeserializeFromXmlReader

        public T DeserializeFromXmlReader<T>(XmlReader xmlReader)
        {
            XmlSerializer serializer = s_XmlSerializerCache.GetSerializer(typeof(T));
            return (T)serializer.Deserialize(xmlReader);
        }

        public object DeserializeFromXmlReader(Type type, XmlReader xmlReader)
        {
            XmlSerializer serializer = s_XmlSerializerCache.GetSerializer(type);
            return serializer.Deserialize(xmlReader);
        }

        #endregion

        #region [ DeserializeFromString ]

		protected T DeserializeFromString<T>(XmlSerializer serializer, string serializedString)
		{
			T obj;
			using (StringReader reader = new StringReader(serializedString))
			{
				obj = (T)serializer.Deserialize(reader);
			}

			return obj;
		}

		#endregion
	}
}