using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Ctrip.SOA.Infratructure.Serialization;

namespace Ctrip.SOA.Infratructure.Collections
{
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public SerializableDictionary() : base()
        {
        }

        public SerializableDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary)
        {
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            while (reader.Read())
            {
                reader.MoveToContent();
                if (reader.IsStartElement("Item") && !reader.IsEmptyElement)
                {
                    while (reader.NodeType != XmlNodeType.EndElement)
                    {
                        reader.ReadStartElement("Item");

                        reader.ReadStartElement("Key");
                        TKey key = Serializer.Xml.DeserializeFromXmlReader<TKey>(reader);
                        reader.ReadEndElement();
                        reader.ReadStartElement("Value");
                        TValue value = Serializer.Xml.DeserializeFromXmlReader<TValue>(reader);
                        reader.ReadEndElement();

                        reader.ReadEndElement(); // read </Item>
                        reader.MoveToContent();
                        this.Add(key, value);
                    }
                    reader.ReadEndElement();
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (KeyValuePair<TKey, TValue> keyValue in this)
            {
                writer.WriteStartElement("Item");
                writer.WriteStartElement("Key");
                Serializer.Xml.SerializeToXmlWriter(writer, keyValue.Key);
                writer.WriteEndElement();
                writer.WriteStartElement("Value");
                Serializer.Xml.SerializeToXmlWriter(writer, keyValue.Value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }
    }
}
