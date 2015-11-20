using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Runtime.Caching;
using System.Configuration;
using System.Data;

namespace HHInfratructure.Memcached.Cfg
{
    public class CfgService
    {
        public static Dictionary<string, int> GetAllUpdateSetConfig()
        {
            string key = "HHInfr_CfgService_GetAllUpdateSetConfig";
            try
            {
                MemoryCache localCache = MemoryCache.Default;
                if (localCache[key] == null)
                {
                    Dictionary<string, int> dic = new Dictionary<string, int>();
                    MemcachedUpdateSetConfigDAL configDAL = new MemcachedUpdateSetConfigDAL();
                    var allList = configDAL.GetAll();
                    foreach (var item in allList)
                    {
                        dic.Add(item.CacheKeyPrefix, item.UpdateHourSpan);
                    }
                    localCache.Add(key, dic, new DateTimeOffset(DateTime.Now.AddMinutes(KeyMinuteManager.DefaultMinute)));
                    return dic;
                }
                return localCache[key] as Dictionary<string, int>;
            }
            catch (Exception ex)
            {
                Logging.HHLogHelperV2.ERRORGlobalException(ex);
                return new Dictionary<string, int>();
            }
        }
        public static void UpdateFreCount(string key, int freCount)
        {
            try
            {
                MemcachedGetFrequencyDAL frequencyDAL = new MemcachedGetFrequencyDAL();
                MemcachedGetFrequencyEntity entity = frequencyDAL.Select(string.Format("CacheKey='{0}'", key));
                if (entity != null)
                {
                    entity.FreCount += freCount;
                    entity.LastGetDateTime = DateTime.Now;
                    entity.DataChange_LastTime = DateTime.Now;
                    frequencyDAL.Update(entity);
                }
            }
            catch (Exception ex)
            {
                Logging.HHLogHelperV2.ERRORGlobalException(ex);
            }
        }

        public static void CleanFreCount()
        {
            DataTable dt = new MemcachedGetFrequencyDAL().SelectTableId();
            IList<int> frequencyIds = new List<int>();
            foreach (DataRow row in dt.Rows)
            {
                frequencyIds.Add(int.Parse(row["MemcachedGetFrequencyId"].ToString()));
            }
            MemcachedGetFrequencyDAL dal = new MemcachedGetFrequencyDAL();
            foreach (int frequenceId in frequencyIds)
            {
                try
                {
                    MemcachedGetFrequencyEntity entity = dal.Select(frequenceId);
                    if (entity != null)
                    {
                        entity.FreCount = 0;
                        entity.DataChange_LastTime = DateTime.Now;
                        entity.LastGetDateTime = DateTime.Now;
                        dal.Update(entity);
                    }
                }
                catch (Exception ex)
                {
                    Logging.HHLogHelperV2.ERRORGlobalException(ex);
                }
            }
        }
        public static List<MemcachedUpdateRuleEntity> GetJobUpdateData(int ipLen, int ipIndex)
        {
            try
            {
                MemcachedUpdateRuleDAL ruleDAL = new MemcachedUpdateRuleDAL();
                return ruleDAL.GetJobUpdateData(ipLen, ipIndex);
            }
            catch (Exception ex)
            {
                Logging.HHLogHelperV2.ERRORGlobalException(ex);
                throw ex;
            }
        }
        public static void UpdateTime(int ruleId, string processIP)
        {
            try
            {
                MemcachedUpdateRuleDAL dal = new MemcachedUpdateRuleDAL();
                MemcachedUpdateRuleEntity entity = dal.Select(ruleId);
                if (entity != null)
                {
                    entity.LastUpdateTime = DateTime.Now;
                    entity.DataChange_LastTime = DateTime.Now;
                    entity.ProcessIP = processIP;
                    entity.UpdateLockTime = DateTime.Now;
                    dal.Update(entity);
                }
            }
            catch (Exception ex)
            {
                Logging.HHLogHelperV2.ERRORGlobalException(ex);
            }
        }
        public static void InitCfgData(string key, string jsonCallEnity)
        {
            try
            {
                var index=key.IndexOf(CacheManager.MAIN_KEY_SPLIT_CHAR);
                string cacheKeyPrefix = index >= 0 ? key.Substring(0, index) : key;
                MemcachedUpdateSetConfigDAL configDAL = new MemcachedUpdateSetConfigDAL();
                MemcachedUpdateSetConfigEntity configEngity = configDAL.Select(string.Format("CacheKeyPrefix='{0}'", cacheKeyPrefix));
                if (configEngity == null)
                {
                    configEngity = new MemcachedUpdateSetConfigEntity();
                    configEngity.CacheKeyPrefix = cacheKeyPrefix;
                    configEngity.DataChange_LastTime = DateTime.Now;
                    configEngity.FreMin = 0;//数据库默认
                    configEngity.IsJobActByMin = 0;
                    configEngity.UpdateHourSpan = KeyMinuteManager.DefaultMinute;
                    configDAL.Insert(configEngity);
                }

                MemcachedUpdateRuleDAL ruleDAL = new MemcachedUpdateRuleDAL();
                MemcachedUpdateRuleEntity ruleEntity = ruleDAL.Select(string.Format("CacheKey='{0}'", key));
                if (ruleEntity == null)
                {
                    ruleEntity = new MemcachedUpdateRuleEntity();
                    ruleEntity.CacheKey = key;
                    ruleEntity.CacheKeyPrefix = cacheKeyPrefix;
                    ruleEntity.ConditionEntityJson = jsonCallEnity;
                    ruleEntity.DataChange_LastTime = DateTime.Now;
                    ruleEntity.IsJobActByMin = configEngity.IsJobActByMin;
                    ruleEntity.LastUpdateTime = DateTime.Now;
                    ruleEntity.UpdateHourSpan = configEngity.UpdateHourSpan;
                    ruleDAL.Insert(ruleEntity);
                }

                MemcachedGetFrequencyDAL freqDAL = new MemcachedGetFrequencyDAL();
                MemcachedGetFrequencyEntity freqEntity = freqDAL.Select(string.Format("CacheKey='{0}'", key));
                if (freqEntity == null)
                {
                    freqEntity = new MemcachedGetFrequencyEntity();
                    freqEntity.CacheKey = key;
                    freqEntity.CacheKeyPrefix = cacheKeyPrefix;
                    freqEntity.DataChange_LastTime = DateTime.Now;
                    freqEntity.FreMin = configEngity.FreMin;
                    freqEntity.FreCount = 1;
                    freqEntity.LastGetDateTime = DateTime.Now;
                    freqDAL.Insert(freqEntity);
                }
            }
            catch(Exception ex)
            {
                Logging.HHLogHelperV2.ERRORGlobalException(ex);
            }
        }
    }
}
