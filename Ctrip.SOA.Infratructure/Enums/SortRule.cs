using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Enums
{
    /// <summary>
    /// 排序字段
    /// </summary>
    public enum SortRule
    {
        /// <summary>
        /// 按提交时间
        /// </summary>
        SubmitTime=3,
        /// <summary>
        /// 按最低价格
        /// </summary>
        ProductMinPrice = 2,

        /// <summary>
        /// 按行程天数
        /// </summary>
        ProductTripDays = 1,

        /// <summary>
        /// 默认排序规则:
        /// </summary>
        ProductDefault = 0,
    }
}