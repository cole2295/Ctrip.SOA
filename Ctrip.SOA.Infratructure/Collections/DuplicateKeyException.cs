using System;
using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Collections
{   
    /// <summary>
    /// 当尝试使用正在使用的键将对象添加到集合中时引发。
    /// </summary>
    [Serializable]
    public partial class DuplicateKeyException : InvalidOperationException
    {
        private string key;

        /// <summary>
        /// 初始化 <see cref="DuplicateKeyException"/> 类的新实例。 
        /// </summary>
        public DuplicateKeyException()
            : this("muti keys")
        {
        }

        /// <summary>
        /// 通过引用重复键来初始化 <see cref="DuplicateKeyException"/> 类的一个新实例。
        /// </summary>
        /// <param name="key">键值。</param>
        public DuplicateKeyException(string key)
            : base("muti keys")
        {
            this.key = key;
        }

        /// <summary>
        /// 通过引用重复键并提供错误消息来初始化 <see cref="DuplicateKeyException"/> 类的一个新实例。
        /// </summary>
        /// <param name="key">导致引发异常的重复键。</param>
        /// <param name="message">当引发异常时要显示的消息。</param>
        public DuplicateKeyException(string key, string message)
            : base("muti keys")
        {
            this.Key = key;
        }
        
        /// <summary>
        /// 获取导致引发异常的重复键。
        /// </summary>
        public string Key { get; private set; }
    }
}
