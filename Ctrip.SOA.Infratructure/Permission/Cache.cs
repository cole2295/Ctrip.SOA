using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using HHInfratructure.Data;
using HHInfratructure.Logging;

namespace HHInfratructure.Permission
{
    public class Cache
    {
        internal string DBName = "";
        internal string CacheName = "";
        internal int CacheUpdateInterval = 100;
        internal TimeInterval CacheUpdatePolicy = TimeInterval.Minute;
        internal string Sqls = "";
        internal string Tbls = "";
        internal DataSet dsCache = new DataSet();
        internal DateTime dtUpdate = DateTime.Now.AddDays(-1);

        internal Cache()
        {
        }

        public DataSet GetDataSet()
        {
            return dsCache;
        }

        /// <summary>
        /// 更新缓存
        /// </summary>
        internal void UpdateCache()
        {
            dtUpdate = DateTime.Now;
            DataSet ds = new DataSet();
            try
            {
                //ds = Data.SqlHelper.GetDataSet(DBName, Sqls);
                ds = new CacheSelectDB().GetDataSet(Sqls);
                string[] tbl = Tbls.Split(';');
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = tbl[i];
                }

                //LocalCache.SaveCacheToLocalFile(ds, "Cache-" + this.CacheName);
            }
            catch (Exception e)
            {
                //ds = LocalCache.ReadCacheFromLocalFile("Cache-" + this.CacheName);
                SysLog.WriteException("HHTravel.Base.Common.Framework.Cache.UpdateCache", e);
                //如果从数据获取缓存失败，并且从本地文件恢复也失败，此时向上抛出异常
                if (ds == null) throw e;
            }
            lock (dsCache)
            {
                dsCache = ds;
            }
        }
    }

    public enum TimeInterval
    {
        Hour = 0,
        Minute = 1
    }
}