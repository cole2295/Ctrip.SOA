/* ->
 * -> 该代码使用工具生成，请勿手动修改 
 * -> 生成时间： 2015-10-26 17:01:46 
 */

using System;

namespace Ctrip.SOA.Repository.Order.Model
{
    /// <summary>
    /// Order || 
    /// </summary>    
    [Serializable]
    public class OrderEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateTime { get; set; }

    }
}
