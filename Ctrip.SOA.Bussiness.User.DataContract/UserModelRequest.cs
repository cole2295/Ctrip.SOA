/* ->
 * -> 该代码使用工具生成，请勿手动修改 
 * -> 生成时间： 2015-10-16 09:34:28 
 */

using System;
using System.Runtime.Serialization;

namespace Ctrip.SOA.Bussiness.User.DataContract
{
    /// <summary>
    /// User || 
    /// </summary>    
   
    [DataContract]
    public class UserModelRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public long UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string UserName { get; set; }


    }
}
