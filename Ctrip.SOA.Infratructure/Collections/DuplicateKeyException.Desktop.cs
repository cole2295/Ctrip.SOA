using System.Runtime.Serialization;

namespace Ctrip.SOA.Infratructure.Collections
{
    /// <summary>
    /// 表示在集合对象添加操作的过程中，遇到重复的对象键值时引发的异常。 
    /// </summary>
    public partial class DuplicateKeyException
    {
        /// <summary>
        /// 用序列化数据初始化 <see cref="DuplicateKeyException"/> 类的新实例。 
        /// </summary>
        /// <param name="info">保存序列化对象数据的对象。</param>
        /// <param name="context">对象，描述序列化数据的源或目标。</param>
        public DuplicateKeyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            key = info.GetString("Key");
        }

        /// <summary>
        /// 设置带有键值和附加异常信息的 <see cref="SerializationInfo"/> 对象。 
        /// </summary>
        /// <param name="info">保存序列化对象数据的对象。</param>
        /// <param name="context">有关源或目标的上下文信息。</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Key", key, typeof(string));
        }
    }
}
