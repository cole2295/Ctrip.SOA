using System.Runtime.Serialization;

namespace Ctrip.SOA.Infratructure.Serialization
{
	public class GenericSerializationInfo
	{
		private SerializationInfo m_SerializationInfo;

		public GenericSerializationInfo(SerializationInfo info)
		{
			m_SerializationInfo = info;
		}

		public void AddValue<T>(string name, T value)
		{
			m_SerializationInfo.AddValue(name, value, value == null ? typeof(T) : value.GetType());
		}

		public T GetValue<T>(string name)
		{
			object info = m_SerializationInfo.GetValue(name, typeof(T));
			return (T)info;
		}
	}
}