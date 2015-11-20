using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ctrip.SOA.Infratructure.Enums
{
    [DataContract]
    public enum EmailType
    {
        /// <summary>
        /// 转寄好友
        /// </summary>
        [EnumMember]
        Share,

        /// <summary>
        /// 订单
        /// </summary>
        [EnumMember]
        Order,

        /// <summary>
        /// 电子报订阅通知
        /// </summary>
        [EnumMember]
        NewsSub,

        /// <summary>
        /// 产品过期检查
        /// </summary>
        [EnumMember]
        ProductCheck,

        /// <summary>
        /// MBT订单
        /// </summary>
        [EnumMember]
        MBTOrder,
    }
}
