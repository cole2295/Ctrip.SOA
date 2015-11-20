using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Ctrip.SOA.Infratructure.Memcached
{
    /// <summary>
    /// 缓存公共函数类
    /// </summary>
    public class CommonCacheFunc
    {
        /// <summary>
        /// 把返回实体以指定长度（字节)进行分割
        /// </summary>
        /// <param name="returnEntity"></param>
        /// <param name="sliceLength"></param>
        public static List<ByteSliceEntity> SplitReturnEntity(BaseReturnEntity returnEntity, int splitLength)
        {
            List<ByteSliceEntity> reutnEntityList = new List<ByteSliceEntity>();
            if (returnEntity != null)
            {
                //去除ResponseXML测试字段
                returnEntity.ResponseXML = string.Empty;
                DataEntityBinarySerialize dataEntityBinarySerialize = new DataEntityBinarySerialize(returnEntity.GetType());
                byte[] entityBytes = dataEntityBinarySerialize.Serialize(returnEntity);

                int sliceLength = splitLength;//切片长度
                int entityLenght = entityBytes.Length;//实体总长度

                int pageCount = entityLenght / sliceLength + ((entityLenght % sliceLength) > 0 ? 1 : 0);//切片总个数
                for (int i = 0; i < pageCount; i++)//拆分字节数据
                {
                    ByteSliceEntity siliceItem = new ByteSliceEntity();
                    if (i == 0)//调用是否成功和总长度放到第一个ByteSliceEntity的
                    {
                        siliceItem.CallSuccess = returnEntity.CallSuccess;
                        siliceItem.EntityLength = entityBytes.Length;
                    }
                    int copyStartIndex = i * sliceLength;
                    int copyLength = sliceLength;
                    copyLength = (copyStartIndex + copyLength) > entityLenght ? entityLenght - copyStartIndex : copyLength;
                    byte[] content = new byte[copyLength];

                    Array.Copy(entityBytes, copyStartIndex, content, 0, copyLength);//拷贝到返回流中
                    siliceItem.Content = content;
                    reutnEntityList.Add(siliceItem);
                    Thread.Sleep(1);
                }
            }
            return reutnEntityList;
        }

        /// <summary>
        /// 合并ByteSliceEntity的Content字段，返回byte[]
        /// </summary>
        /// <param name="byteSliceEntitys"></param>
        /// <returns></returns>
        public static byte[] MergeByteSilceEntity(ByteSliceEntity[] byteSliceEntitys)
        {
            byte[] entityBytes = null;//定义存放原实体变量
            if (byteSliceEntitys != null)
            {
                int currentIndex = 0;
                foreach (ByteSliceEntity itemcacheEntity in byteSliceEntitys)//合并流数据
                {
                    if (itemcacheEntity == null) continue;
                    if (entityBytes == null)
                        entityBytes = new byte[itemcacheEntity.EntityLength];
                    if (itemcacheEntity.Content != null)
                    {
                        Array.Copy(itemcacheEntity.Content, 0, entityBytes, currentIndex, itemcacheEntity.Content.Length);
                        currentIndex += itemcacheEntity.Content.Length;
                    }
                    Thread.Sleep(1);
                }
            }
            return entityBytes;
        }

        /// <summary>
        /// 把返回实体转换成ByteSliceEntity实体
        /// </summary>
        /// <param name="returnEntity"></param>
        /// <returns></returns>
        public static ByteSliceEntity GetByteSliceEntity(BaseReturnEntity returnEntity)
        {
            ByteSliceEntity returnByteSliceEntity = null;
            if (returnEntity != null)
            {
                returnEntity.ResponseXML = string.Empty;
                DataEntityBinarySerialize dataEntityBinarySerialize = new DataEntityBinarySerialize(returnEntity.GetType());
                byte[] entityBytes = dataEntityBinarySerialize.Serialize(returnEntity);
                returnByteSliceEntity = new ByteSliceEntity(returnEntity.CallSuccess, entityBytes.Length, entityBytes);
            }
            return returnByteSliceEntity;
        }

        /// <summary>
        /// 根据字节流，反序列化出实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityBytes"></param>
        /// <returns></returns>
        public static T DeserializeEntity<T>(byte[] entityBytes) where T : BaseReturnEntity
        {
            T returnEntity = default(T);
            if (entityBytes != null)
            {
                DataEntityBinarySerialize dataEntityBinarySerialize = new DataEntityBinarySerialize(typeof(T));
                returnEntity = (T)dataEntityBinarySerialize.Deserialize(entityBytes);
            }
            return returnEntity;
        }
    }
}
