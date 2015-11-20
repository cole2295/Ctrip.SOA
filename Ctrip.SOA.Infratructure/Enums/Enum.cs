using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Enums
{
    /// <summary>
    /// 目的地类型，0：空，1：城市，2：国家，3：洲
    /// </summary>
    public enum DestinationType
    {
        Empty = 0,

        City = 1,

        Country = 2,

        Continent = 3
    }
}
