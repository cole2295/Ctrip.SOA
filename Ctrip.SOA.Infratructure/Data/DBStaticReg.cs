using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Ctrip.SOA.Infratructure.Data
{
    /// <summary>
    /// 作者：朱肖云
    /// 时间：3/18/2014 3:03:32 PM
    /// 公司:CTrip
    /// 唯一标识：86e25cfe-b55c-4578-9325-d15066377d9e
    /// </summary>
    public static class DBStaticReg
    {
        public static Database SetDatabase(this Database db, string connectionKey)
        {
            db= DatabaseFactory.CreateDatabase(connectionKey);
            return db;
        }
    }
}