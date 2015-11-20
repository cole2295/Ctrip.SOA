using System;
using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Ctrip.SOA.Infratructure.Serialization;

namespace Ctrip.SOA.Infratructure.Collections
{
    public class SerializableHashtable : Hashtable, IXmlSerializable
    {
        public SerializableHashtable() : base()
        {
        }

        public SerializableHashtable(IDictionary dictionary) : base(dictionary)
        {
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotSupportedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (object key in this.Keys)
            {
                writer.WriteStartElement("Item");
                writer.WriteStartElement("Key");
                Serializer.Xml.SerializeToXmlWriter(writer, key);
                writer.WriteEndElement();
                writer.WriteStartElement("Value");
                Serializer.Xml.SerializeToXmlWriter(writer, this[key]);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }
    }
}
