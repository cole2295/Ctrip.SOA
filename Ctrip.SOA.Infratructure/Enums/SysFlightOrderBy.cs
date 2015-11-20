using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Enums
{
    /// <summary>
    /// 系统机票排序项（Price=1,价格,DTime=2,起飞时间 Atime=4,到达时间，TravelTime=3，飞行时长（飞机在天空的飞行时长），TotalTravelTime=5，飞行时长（包括地面飞行时长）空=1）
    /// </summary>
    public enum SysFlightOrderBy
    {

        /// <summary>
        /// 价格
        /// </summary>
        Price = 1,

        /// <summary>
        /// 起飞时间
        /// </summary>
        DTime = 2,

        /// <summary>
        /// 飞行时长（飞机在天空的飞行时长）
        /// </summary>
        TravelTime=3,

        /// <summary>
        /// 到达时间
        /// </summary>
        Atime = 4,

        /// <summary>
        /// 飞行时长（包括地面飞行时长）
        /// </summary>
        TotalTravelTime=5
    }
}
