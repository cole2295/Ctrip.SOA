using System;
using System.Xml.Serialization;

namespace Ctrip.SOA.Infratructure.Serialization.Xml
{
	/// <summary>
	/// 表示 XmlSerializer 缓存键。
	/// </summary>
	public class XmlSerializerCacheKey
	{
		private int m_HashKey;

		/// <summary>
		/// 构造函数。
		/// </summary>
		/// <param name="type"></param>
		public XmlSerializerCacheKey(Type type)
			: this(type, null)
		{
		}

		/// <summary>
		/// 构造函数。
		/// </summary>
		/// <param name="type"></param>
		/// <param name="knownTypes"></param>
		public XmlSerializerCacheKey(Type type, Type[] knownTypes)
		{
            this.Type = type;
			this.KnownTypes = knownTypes;

			m_HashKey = type.FullName.GetHashCode() ^ GetArrayHashCode(this.KnownTypes);
		}

		public Type Type { get; private set; }
		public Type[] KnownTypes { get; private set; }
 
		/// <summary>
		/// 重写相等操作。
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
		    XmlSerializerCacheKey key = obj as XmlSerializerCacheKey;
			if (key != null
				&& key.Type == this.Type
				&& ArrayEquals(key.KnownTypes, this.KnownTypes))
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// 获取Hash键值。
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return m_HashKey;
		}

        /// <summary>
        /// 判断两个Type的数组是否相等。
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        private bool ArrayEquals(Type[] a1, Type[] a2)
        {
            if (a1 == null && a2 == null)
            {
                return true;
            }

            if ((a1 == null && a2 != null) || (a1 != null && a2 == null))
            {
                return false;
            }

            if (a1.Length != a2.Length)
            {
                return false;
            }

            for (int pos = 0; pos < a1.Length; pos++)
            {
                if (a1[pos] != a2[pos])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 获取类型数组Hash键值。
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        private int GetArrayHashCode(Type[] types)
        {
            int hashCode = 0;
            if (types != null && types.Length > 0)
            {
                foreach (Type item in types)
                {
                    hashCode = hashCode ^ item.FullName.GetHashCode();
                }
            }

            return hashCode;
        }
	}
}