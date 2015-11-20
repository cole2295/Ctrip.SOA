using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using HHInfratructure.Data;
using HHInfratructure.Logging;

namespace HHInfratructure.Permission
{
    public class CacheSelectDB : DALContext
    {
        public CacheSelectDB() : base(DBConsts.HHGovDB_SELECT) { }

        public DataSet GetDataSet(string sql)
        {
            DbCommand dbCommand = DB.GetSqlStringCommand(sql);
            return DB.ExecuteDataSet(dbCommand);
        }
    }
}