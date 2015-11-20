using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Ctrip.SOA.Infratructure.Enums
{
    /// <summary>
    /// 对应Prd_ProductCategory的数据
    /// </summary>
    [DataContract]
    [Serializable]
    public enum ProductCategory
    {
        [EnumMember]
        导游服务 = 1,

        [EnumMember]
        交通接驳 = 2,

        [EnumMember]
        餐饮服务 = 3,

        [EnumMember]
        门票 = 4,

        [EnumMember]
        演出 = 5,

        [EnumMember]
        观光游 = 6,

        [EnumMember]
        购物店 = 7,

        [EnumMember]
        酒店服务 = 8,

        [EnumMember]
        境内N日游 = 9,

        [EnumMember]
        国内旅游 = 10,

        [EnumMember]
        出境旅游 = 11,

        [EnumMember]
        境外一日游 = 12,

        [EnumMember]
        邮轮 = 13,

        [EnumMember]
        保险 = 14,

        [EnumMember]
        签证 = 15,

        [EnumMember]
        实物赠送 = 16,

        [EnumMember]
        附加费 = 17,

        [EnumMember]
        专享特惠 = 18,

        [EnumMember]
        火车票 = 19,

        [EnumMember]
        巴士 = 20,

        [EnumMember]
        船票 = 21,

        [EnumMember]
        套餐 = 22,

        [EnumMember]
        带驾租车 = 23,

        [EnumMember]
        美食 = 24,

        [EnumMember]
        境内一日游 = 25,

        [EnumMember]
        境外N日游 = 26,

        [EnumMember]
        户外运动 = 27,

        [EnumMember]
        休闲活动 = 28,

        [EnumMember]
        PASS = 29,

        [EnumMember]
        单房差 = 30,

        [EnumMember]
        服务 = 31,

        [EnumMember]
        代驾租车 = 32,

        [EnumMember]
        酒店 = 33,

        [EnumMember]
        机票 = 34
    }
}
