using System;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace Ctrip.SOA.Infratructure.Serialization.Xml
{
	public interface IXmlSerializer : ISerializer
	{
        // SeralizeToXmlTextWriter
        void SerializeToXmlWriter(XmlWriter writer, object obj);

        // DeserializeFromXmlReader
        T DeserializeFromXmlReader<T>(XmlReader xmlReader);
        object DeserializeFromXmlReader(Type type, XmlReader xmlReader);
	}
}