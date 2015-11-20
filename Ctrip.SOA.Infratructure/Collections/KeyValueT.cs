using System;
using System.Xml.Serialization;

namespace Ctrip.SOA.Infratructure.Collections
{
    [XmlRoot("KeyValue")]
    [Serializable]
    public class KeyValue<TKey, TValue> : IKeyedObject<TKey>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}