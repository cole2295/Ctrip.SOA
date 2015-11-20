using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ctrip.SOA.Infratructure.Serialization.Xml
{
	public class XmlSerializerCache : IDisposable
	{
		// Fields
		private Dictionary<XmlSerializerCacheKey, XmlSerializer> _serializers
			= new Dictionary<XmlSerializerCacheKey, XmlSerializer>();
		private object _syncRoot = new object();

		public XmlSerializer GetSerializer(Type type)
		{
			return GetSerializer(type, null);
		}

		public XmlSerializer GetSerializer(Type type, Type[] knownTypes)
		{
			XmlSerializer serializer = null;
			XmlSerializerCacheKey key = new XmlSerializerCacheKey(type, knownTypes);
			if (!_serializers.ContainsKey(key))
			{
				lock (_syncRoot)
				{
					if (!_serializers.ContainsKey(key))
					{
						serializer = knownTypes == null ? new XmlSerializer(type) : new XmlSerializer(type, knownTypes);
						_serializers.Add(key, serializer);
						return serializer;
					}
				}
			}

			serializer = this._serializers[key];
			return serializer;
		}

		// Methods
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool isDisposing)
		{
		}

		~XmlSerializerCache()
		{
			this.Dispose(false);
		}
	}
}
