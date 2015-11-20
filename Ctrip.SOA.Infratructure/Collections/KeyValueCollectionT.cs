using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Ctrip.SOA.Infratructure.Serialization;

namespace Ctrip.SOA.Infratructure.Collections
{
    [XmlRoot("KeyValues")]
    [Serializable]
    public class KeyValueCollection<TKey, TValue> : KeyedObjectCollection<TKey, KeyValue<TKey, TValue>>, IXmlSerializable, IKeyValueCollection
    {
        private Dictionary<TKey, KeyValue<TKey, TValue>> _emptyDictionary = null;

        public KeyValueCollection()
            : base()
        {
            _emptyDictionary = new Dictionary<TKey, KeyValue<TKey, TValue>>(this.Comparer);
        }

        public KeyValueCollection(IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
            _emptyDictionary = new Dictionary<TKey, KeyValue<TKey, TValue>>(this.Comparer);
        }

        public void Add(TKey key, TValue value)
        {
            base.Add(new KeyValue<TKey, TValue>() { Key = key, Value = value });
        }

        public void AddRange(KeyValueCollection<TKey, TValue> keyValues)
        {
            foreach (KeyValue<TKey, TValue> keyValue in keyValues)
            {
                base.Add(keyValue);
            }
        }

        public ICollection Keys
        {
            get 
            {
                if (this.Dictionary == null)
                {
                    return _emptyDictionary.Keys;
                }
                return this.Dictionary.Keys as ICollection; 
            }
        }

        public ICollection Values
        {
            get 
            {
                if (this.Dictionary == null)
                {
                    return _emptyDictionary.Keys;
                }
                return this.Dictionary.Values as ICollection; 
            }
        }

        public object GetValueByKey(object key)
        {
            if (key is TKey)
            {
                return base[(TKey)key].Value;
            }

            return null;
        }

        public new TValue this[TKey key]
        {
            get { return base[key].Value; }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            if (reader.IsEmptyElement || !reader.Read())
            {
                return;
            }
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("Item");

                reader.ReadStartElement("Key");
                TKey key = Serializer.Xml.DeserializeFromXmlReader<TKey>(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("Value");
                TValue value = Serializer.Xml.DeserializeFromXmlReader<TValue>(reader);
                reader.ReadEndElement();

                reader.ReadEndElement();
                reader.MoveToContent();
                this.Add(key, value);
            }
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (KeyValue<TKey, TValue> keyValue in this)
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

        public Dictionary<TKey, TValue> ToDictionary()
        {
            Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();

            foreach (KeyValue<TKey, TValue> keyValue in this)
            {
                dict.Add(keyValue.Key, keyValue.Value);
            }

            return dict;
        }

        public List<TValue> ToList()
        {
            List<TValue> values = new List<TValue>();

            foreach (KeyValue<TKey, TValue> keyValue in this)
            {
                values.Add(keyValue.Value);
            }

            return values;
        }
    }
}