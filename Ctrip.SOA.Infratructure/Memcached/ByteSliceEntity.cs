using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Memcached
{
    /// <summary>
    /// 字节分片实体
    /// </summary>
    [Serializable]
    public class ByteSliceEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ByteSliceEntity()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="callSuccess"></param>
        /// <param name="entityLength"></param>
        public ByteSliceEntity(bool callSuccess, int entityLength)
        {
            this.callSuccess = callSuccess;
            this.entityLength = entityLength;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="callSuccess"></param>
        /// <param name="entityLength"></param>
        /// <param name="content"></param>
        public ByteSliceEntity(bool callSuccess, int entityLength, byte[] content)
            : this(callSuccess, entityLength)
        {
            this.content = content;
        }

        private bool callSuccess;

        /// <summary>
        /// SOA请求成功标志
        /// </summary>
        public bool CallSuccess
        {
            get { return callSuccess; }
            set { callSuccess = value; }
        }

        private bool isGetFromCache;

        /// <summary>
        /// 是否来自缓存
        /// </summary>
        public bool IsGetFromCache
        {
            get { return isGetFromCache; }
            set { isGetFromCache = value; }
        }


        private int entityLength;

        /// <summary>
        /// 实体字节总长度
        /// </summary>
        public int EntityLength
        {
            get { return entityLength; }
            set { entityLength = value; }
        }

        private byte[] content;

        /// <summary>
        /// 分片内容
        /// </summary>
        public byte[] Content
        {
            get { return content; }
            set { content = value; }
        }
    }
}
