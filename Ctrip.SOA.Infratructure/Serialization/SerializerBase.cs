using System;
using System.IO;
using System.Text;

namespace Ctrip.SOA.Infratructure.Serialization
{
	/// <summary>
	/// 序列化基类。
	/// </summary>
	public abstract class SerializerBase : ISerializer
	{
		#region [ SerializeToStream ]

		public virtual void SerializeToStream<T>(Stream serializationStream, T obj)
		{
			SerializeToStream(serializationStream, typeof(T));
		}

		public void SerializeToStream(Stream serializationStream, object obj)
		{
			SerializeToStream(serializationStream, obj, Encoding.UTF8);
		}

		public void SerializeToStream(Stream serializationStream, object obj, Encoding encoding)
		{
			SerializeToStream(serializationStream, obj, (Type[])null, encoding);
		}

		public void SerializeToStream(Stream serializationStream, object obj, Type[] knownTypes)
		{
			SerializeToStream(serializationStream, obj, knownTypes, Encoding.UTF8);
		}

		public virtual void SerializeToStream(Stream serializationStream, object obj, Type[] knownTypes, Encoding encoding)
		{
			throw new NotSupportedException();
		}

		#endregion

		#region [ SerializeToString ]

		public virtual string SerializeToString<T>(T obj)
		{
			return SerializeToString(obj, (Type[])null, Encoding.UTF8);
		}

		public string SerializeToString(object obj)
		{
			return SerializeToString(obj, Encoding.UTF8);
		}

		public string SerializeToString(object obj, Encoding encoding)
		{
			return SerializeToString(obj, (Type[])null, encoding);
		}

		public string SerializeToString(object obj, Type[] knownTypes)
		{
			return SerializeToString(obj, knownTypes, Encoding.UTF8);
		}

		public virtual string SerializeToString(object obj, Type[] knownTypes, Encoding encoding)
		{
			byte[] bytes = knownTypes == null ?
				SerializeToBytes(obj, encoding) : SerializeToBytes(obj, knownTypes, encoding);
			return encoding.GetString(bytes);
		}

		#endregion

		#region [ SerializeToBytes ]

		public virtual byte[] SerializeToBytes<T>(T obj)
		{
			return SerializeToBytes(obj, (Type[])null, Encoding.UTF8);
		}

		public byte[] SerializeToBytes(object obj)
		{
			return SerializeToBytes(obj, Encoding.UTF8);
		}

		public byte[] SerializeToBytes(object obj, Encoding encoding)
		{
			return SerializeToBytes(obj, null, encoding);
		}

		public byte[] SerializeToBytes(object obj, Type[] knownTypes)
		{
			return SerializeToBytes(obj, knownTypes, Encoding.UTF8);
		}

		public virtual byte[] SerializeToBytes(object obj, Type[] knownTypes, Encoding encoding)
		{
			if (obj == null)
			{
				return new byte[] { };
			}

			byte[] bytes;
			using (MemoryStream stream = new MemoryStream())
			{
				if (knownTypes == null)
				{
					SerializeToStream(stream, obj, encoding);
				}
				else
				{
					SerializeToStream(stream, obj, knownTypes, encoding);
				}
				stream.Seek(0, 0);
				bytes = stream.ToArray();
				stream.Flush();
			}

			return bytes;
		}
		
		#endregion

		#region [ SerializeToFile ]

		public void SerializeToFile(object obj, string fileName)
		{
			SerializeToFile(obj, fileName, Encoding.UTF8);
		}

		public void SerializeToFile(object obj, string fileName, Encoding encoding)
		{
			SerializeToFile(obj, null, fileName, encoding);
		}

		public void SerializeToFile(object obj, Type[] knownTypes, string fileName)
		{
			SerializeToFile(obj, knownTypes, fileName, Encoding.UTF8);
		}

		public virtual void SerializeToFile(object obj, Type[] knownTypes, string fileName, Encoding encoding)
		{
			using (Stream stream = File.Create(fileName))
			{
				if (knownTypes == null)
				{
					SerializeToStream(stream, obj, encoding);
				}
				else
				{
					SerializeToStream(stream, obj, knownTypes, encoding);
				}
			}
		}

		#endregion

		#region [ DeserializeFromStream ]

		public T DeserializeFromStream<T>(Stream serializationStream)
		{
			object obj = DeserializeFromStream(typeof(T), serializationStream);
			return (T)obj;
		}

		public virtual T DeserializeFromStream<T>(Stream serializationStream, Type[] knownTypes)
		{
			object obj = DeserializeFromStream(typeof(T), serializationStream, knownTypes);
			return (T)obj;
		}

		public object DeserializeFromStream(Type type, Stream serializationStream)
		{
			return DeserializeFromStream(type, serializationStream, (Type[])null);
		}

		public virtual object DeserializeFromStream(Type type, Stream serializationStream, Type[] knownTypes)
		{
			throw new NotSupportedException();
		}

		#endregion

		#region [ DeserializeFromBytes ]

		public T DeserializeFromBytes<T>(byte[] bytes)
		{
			T ret = default(T);
			using (MemoryStream stream = new MemoryStream(bytes))
			{
				ret = DeserializeFromStream<T>(stream);
			}

			return ret;
		}

		public T DeserializeFromBytes<T>(byte[] bytes, Type[] knownTypes)
		{
			T ret = default(T);
			using (MemoryStream stream = new MemoryStream(bytes))
			{
				ret = DeserializeFromStream<T>(stream, knownTypes);
			}

			return ret;
		}

		public object DeserializeFromBytes(Type type, byte[] bytes)
		{
			object ret = null;
			using (MemoryStream stream = new MemoryStream(bytes))
			{
				ret = DeserializeFromStream(type, stream);
			}

			return ret;
		}

		public virtual object DeserializeFromBytes(Type type, byte[] bytes, Type[] knownTypes)
		{
			object ret = null;
			using (MemoryStream stream = new MemoryStream(bytes))
			{
				ret = DeserializeFromStream(type, stream, knownTypes);
			}

			return ret;
		}

		#endregion

		#region [ DeserializeFromString ]

		public T DeserializeFromString<T>(string serializedString)
		{
			return DeserializeFromString<T>(serializedString, (Type[])null);
		}

		public T DeserializeFromString<T>(string serializedString, Type[] knownTypes)
		{
			return DeserializeFromString<T>(serializedString, knownTypes, Encoding.UTF8);
		}

		public T DeserializeFromString<T>(string serializedString, Encoding encoding)
		{
			return DeserializeFromString<T>(serializedString, (Type[])null, encoding);
		}

		public T DeserializeFromString<T>(string serializedString, Type[] knownTypes, Encoding encoding)
		{
			object deserializedObj = DeserializeFromString(typeof(T), serializedString, knownTypes, encoding);
			return (T)deserializedObj;
		}

		public T DeserializeFromString<T>(Type type, string serializedString)
		{
			return DeserializeFromString<T>(type, serializedString, (Type[])null, Encoding.UTF8);
		}

		public T DeserializeFromString<T>(Type type, string serializedString, Type[] knownTypes)
		{
			return DeserializeFromString<T>(type, serializedString, knownTypes, Encoding.UTF8);
		}

		public T DeserializeFromString<T>(Type type, string serializedString, Encoding encoding)
		{
			return DeserializeFromString<T>(type, serializedString, (Type[])null, encoding);
		}

		public virtual T DeserializeFromString<T>(Type type, string serializedString, Type[] knownTypes, Encoding encoding)
		{
			object deserializedObj = DeserializeFromString(type, serializedString, knownTypes, encoding);
			return (T)deserializedObj;
		}

		public object DeserializeFromString(Type type, string serializedString)
		{
			return DeserializeFromString(type, serializedString, (Type[])null, Encoding.UTF8);
		}

		public object DeserializeFromString(Type type, string serializedString, Type[] knownTypes)
		{
			return DeserializeFromString(type, serializedString, (Type[])null, Encoding.UTF8);
		}

		public object DeserializeFromString(Type type, string serializedString, Encoding encoding)
		{
			return DeserializeFromString(type, serializedString, (Type[])null, encoding);
		}

		public virtual object DeserializeFromString(Type type, string serializedString, Type[] knownTypes, Encoding encoding)
		{
			if (string.IsNullOrEmpty(serializedString))
			{
				return null;
			}

			byte[] bytes = encoding.GetBytes(serializedString);
			return (knownTypes == null || knownTypes.Length ==0) ?
				DeserializeFromBytes(type, bytes) : DeserializeFromBytes(type, bytes, knownTypes);
		}

		#endregion

		#region [ DeserializeFromFile ]

		public T DeserializeFromFile<T>(string fileName)
		{
			return DeserializeFromFile<T>(fileName, (Type[])null, Encoding.UTF8);
		}

		public T DeserializeFromFile<T>(string fileName, Type[] knownTypes)
		{
			return DeserializeFromFile<T>(fileName, knownTypes, Encoding.UTF8);
		}

		public T DeserializeFromFile<T>(string fileName, Encoding encoding)
		{
			return DeserializeFromFile<T>(fileName, (Type[])null, encoding);
		}

		public T DeserializeFromFile<T>(string fileName, Type[] knownTypes, Encoding encoding)
		{
			object ret = DeserializeFromFile(typeof(T), fileName, knownTypes, encoding);
			return (T)ret;
		}

		public object DeserializeFromFile(Type type, string fileName)
		{
			return DeserializeFromFile(type, fileName, (Type[])null, Encoding.UTF8);
		}

		public object DeserializeFromFile(Type type, string fileName, Type[] knownTypes)
		{
			return DeserializeFromFile(type, fileName, knownTypes, Encoding.UTF8);
		}

		public object DeserializeFromFile(Type type, string fileName, Encoding encoding)
		{
			return DeserializeFromFile(type, fileName, (Type[])null, encoding);
		}
		
		public virtual object DeserializeFromFile(Type type, string fileName, Type[] knownTypes, Encoding encoding)
		{
			object ret = null;
			using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				ret = DeserializeFromStream(type, fs, knownTypes);
			}

			return ret;
		}

		#endregion
	}
}