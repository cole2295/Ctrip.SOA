using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Ctrip.SOA.Infratructure.Memcached
{
    public class DataEntityBinarySerialize : ISerialize<object, byte[]>
    {
        //内部类型
        private Type _type = null;

        public Type type
        {
            get
            {
                return _type;
            }
            set
            {
                //校验类型是否可序列化
                if (!value.IsSerializable)
                {
                    throw new Exception("The type must be Serializable!");
                }
                _type = value;
            }
        }

        /// <summary>
        /// 构造函数(不可用)
        /// </summary>
        private DataEntityBinarySerialize()
        { }

        /// <summary>
        /// 带类型参数 构造函数
        /// </summary>
        /// <param name="type">类型必须可序列化</param>
        public DataEntityBinarySerialize(Type type)
        {
            _type = type;
        }

        #region ISerialize<object,byte[]> 成员
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="entity">实例,必须是可序列化的对象</param>
        /// <returns>XML文档</returns>
        public byte[] Serialize(object entity)
        {
            //校验序列化类型是否赋值
            if (_type == null) return null;
            BinaryFormatter ser = new BinaryFormatter();
            using (MemoryStream mem = new MemoryStream())
            {
                //StreamReader readertemp;

                ser.Serialize(mem, entity);
                mem.Position = 0;
                using (StreamReader readertemp = new StreamReader(mem))
                {
                    //valueStr = Convert.ToBase64String(mem.ToArray());
                    return mem.ToArray();
                }
            }
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="entity">实例,必须是可序列化的对象</param>
        /// <returns>XML文档</returns>
        public string SerializeToBase64String(object entity)
        {
            BinaryFormatter ser = new BinaryFormatter();
            using (MemoryStream mem = new MemoryStream())
            {
                //StreamReader readertemp;

                ser.Serialize(mem, entity);
                mem.Position = 0;
                using (StreamReader readertemp = new StreamReader(mem))
                {
                    return Convert.ToBase64String(mem.ToArray());
                }
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="serialize">XML文档</param>
        /// <returns>object实例</returns>
        public object Deserialize(byte[] serialize)
        {
            using (MemoryStream mem = new MemoryStream(serialize))
            {
                BinaryFormatter ser = new BinaryFormatter();
                //反序列
                return ser.Deserialize(mem);
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="serialize">XML文档</param>
        /// <returns>object实例</returns>
        public object Deserialize(string Base64strserialize)
        {

            byte[] data = System.Convert.FromBase64String(Base64strserialize);
            using (MemoryStream mem = new MemoryStream(data))
            {
                BinaryFormatter ser = new BinaryFormatter();
                //反序列
                return ser.Deserialize(mem);
            }
        }
        #endregion
    }
}
