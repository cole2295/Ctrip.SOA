using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using HHInfratructure.Logging;

namespace HHInfratructure.Permission
{
    /// <summary>
    /// 缓存帮助类 - 提供缓存的获取、主动更新、定时更新等功能
    /// 2012-07-24 Liuyan Created
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// 缓存字典
        /// </summary>
        static Dictionary<string, Cache> caches = new Dictionary<string, Cache>();

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        public static Dictionary<string, Cache> GetCaches()
        {
            return caches;
        }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static CacheHelper()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(UpdateCache));
        }

        /// <summary>
        /// 创建一个新的缓存
        /// </summary>
        /// <param name="cacheName">缓存名称</param>
        /// <param name="dbName">数据库名</param>
        /// <param name="sqls">Sql语句集,以;号区隔</param>
        /// <param name="tbls">对应表名,以;号区隔</param>
        /// <param name="cacheUpdatePolicy">缓存更新策略</param>
        /// <param name="cacheUpdateInterval">缓存更新间隔</param>
        public static void CreateCache(string cacheName, string dbName, string sqls, string tbls, TimeInterval cacheUpdatePolicy, int cacheUpdateInterval)
        {
            Cache c = new Cache();
            c.CacheName = cacheName;
            c.CacheUpdateInterval = cacheUpdateInterval;
            c.CacheUpdatePolicy = cacheUpdatePolicy;
            c.Sqls = sqls;
            c.Tbls = tbls;
            c.DBName = dbName;
            c.UpdateCache();

            lock (caches)
            {
                caches.Remove(cacheName);
                caches.Add(cacheName, c);
            }
        }

        /// <summary>
        /// 判断是否存在指定名称的缓存
        /// </summary>
        /// <param name="cacheName">缓存名称</param>
        /// <returns>是|否</returns>
        public static bool CacheIsExist(string cacheName)
        {
            return caches.ContainsKey(cacheName);
        }

        /// <summary>
        /// 根据指定名称，获取缓存数据
        /// </summary>
        /// <param name="cacheName">缓存名称</param>
        /// <returns></returns>
        public static DataSet GetCache(string cacheName)
        {
            return caches[cacheName].dsCache;
        }

        /// <summary>
        /// 根据指定名称，更新缓存对象
        /// </summary>
        /// <param name="cacheName">缓存名称</param>
        /// <returns></returns>
        public static void UpdateCache(string cacheName)
        {
            caches[cacheName].UpdateCache();
        }

        /// <summary>
        /// 后台线程更新缓存
        /// </summary>
        /// <param name="state"></param>
        private static void UpdateCache(object state)
        {
            while (true)
            {
                Thread.Sleep(1000);

                TimeSpan ts = new TimeSpan();
                try
                {
                    foreach (KeyValuePair<string, Cache> c in caches)
                    {
                        ts = DateTime.Now - c.Value.dtUpdate;
                        //过期就更新缓存
                        if ((c.Value.CacheUpdatePolicy == TimeInterval.Hour &&
                            ts.TotalHours >= c.Value.CacheUpdateInterval) ||
                            (c.Value.CacheUpdatePolicy == TimeInterval.Minute &&
                            ts.TotalMinutes >= c.Value.CacheUpdateInterval))
                            c.Value.UpdateCache();
                    }
                }
                catch (Exception e)
                {
                    SysLog.WriteException("HHTravel.Base.Common.Framework.Cache.UpdateCache", e);
                }
            }
        }
    }
}