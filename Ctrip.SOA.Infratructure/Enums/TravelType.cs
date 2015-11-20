using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Ctrip.SOA.Infratructure.Enums
{
    /// <summary>
    /// 旅游型态
    /// </summary>
    [DataContract]
    public enum TravelType
    {
        /// <summary>
        /// 默认值
        /// </summary>
        [EnumMember]
        TravelType0 = 0,
        /// <summary>
        /// 私家团(全程领队服务)
        /// </summary>
        [Description("私家团")]
        [EnumMember]
        TravelType1 = 1,

        /// <summary>
        /// 自由行
        /// </summary>
        [Description("自由行")]
        [EnumMember]
        TravelType2 = 2,

        /// <summary>
        /// HH33
        /// </summary>
        [Description("HH33(线路)")]
        [EnumMember]
        TravelType3 = 3,
        /// <summary>
        /// 私家团(当地专属车导)
        /// </summary>
        [Description("自家游")]
        [EnumMember]
        TravelType4 = 4,

        /// <summary>
        /// 私家团(当地专属车导)
        /// </summary>
        [Description("HH33(酒店)")]
        [EnumMember]
        TravelType5 = 5
    }
}