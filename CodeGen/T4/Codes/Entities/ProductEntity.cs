/* ->
 * -> 该代码使用工具生成，请勿手动修改 
 * -> 生成时间： 2015-10-26 17:33:18 
 */

using System;

namespace Ctrip.SOA.Repository.Order.Model
{
    /// <summary>
    /// Product || 
    /// </summary>    
    [Serializable]
    public class ProductEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; }

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
