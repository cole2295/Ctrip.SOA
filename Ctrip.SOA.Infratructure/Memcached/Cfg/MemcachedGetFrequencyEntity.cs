//业务实体
//******************************************************************
//功能:
//
//历史: 2014/1/10 
//说明: 这是由一个工具自动生成的代码
//******************************************************************/
using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
namespace HHInfratructure.Memcached.Cfg
{
    /// <summary>
    /// MemcachedGetFrequency实体类
    /// </summary>
    [Serializable]
    public class MemcachedGetFrequencyEntity
    {
		/// <summary>
		/// 缓存键
		/// </summary>
		public string CacheKey { get; set; }
		
		/// <summary>
		/// 频次统计计数
		/// </summary>
		public int FreCount { get; set; }
		
		/// <summary>
		/// 最后一次获取时间
		/// </summary>
		public DateTime LastGetDateTime { get; set; }
		
		/// <summary>
		/// 自动增长列
		/// </summary>
		public int MemcachedGetFrequencyId { get; set; }		
		
		/// <summary>
		/// 访问频率清除最小阀值
		/// </summary>
		public int FreMin { get; set; }
		
		/// <summary>
		/// 缓存KEY前缀
		/// </summary>
		public string CacheKeyPrefix { get; set; }
		
		/// <summary>
		/// 数据改变最后时间
		/// </summary>
		public DateTime DataChange_LastTime { get; set; }
		
        /// <summary>
        /// 根据一条数据记录创建实体对象
        /// </summary>
        public static MemcachedGetFrequencyEntity CreateEntity(DataRow dr)
        {
            MemcachedGetFrequencyEntity ent = new MemcachedGetFrequencyEntity();
            if(dr["CacheKey"] != DBNull.Value)ent.CacheKey = (string)dr["CacheKey"];            
            if(dr["FreCount"] != DBNull.Value)ent.FreCount = int.Parse(dr["FreCount"].ToString());
            if(dr["LastGetDateTime"] != DBNull.Value)ent.LastGetDateTime = DateTime.Parse(dr["LastGetDateTime"].ToString());            
            if(dr["MemcachedGetFrequencyId"] != DBNull.Value)ent.MemcachedGetFrequencyId = int.Parse(dr["MemcachedGetFrequencyId"].ToString());
            if(dr["FreMin"] != DBNull.Value)ent.FreMin = int.Parse(dr["FreMin"].ToString());            
            if(dr["CacheKeyPrefix"] != DBNull.Value)ent.CacheKeyPrefix = (string)dr["CacheKeyPrefix"];            
            if(dr["DataChange_LastTime"] != DBNull.Value)ent.DataChange_LastTime = DateTime.Parse(dr["DataChange_LastTime"].ToString());
            return ent;
        }
       
        /// <summary>
        /// 根据数据集合创建实体对象集合
        /// </summary>
        public static List<MemcachedGetFrequencyEntity> CreateEntity(DataRow[] drs)
        {
            List<MemcachedGetFrequencyEntity> ents = new List<MemcachedGetFrequencyEntity>();
            foreach (DataRow dr in drs)
            {
                ents.Add(CreateEntity(dr));
            }
            return ents;
        }
       
        /// <summary>
        /// 将当前实体转化成日志记录
        /// </summary>
        public string ConvertEntityToLogString()
        {
            StringBuilder LogDetail = new StringBuilder();
            if (this.CacheKey != null)LogDetail.AppendLine("CacheKey=" + this.CacheKey.ToString());            
            LogDetail.AppendLine("FreCount=" + this.FreCount.ToString());
            LogDetail.AppendLine("LastGetDateTime=" + this.LastGetDateTime.ToString());            
            LogDetail.AppendLine("MemcachedGetFrequencyId=" + this.MemcachedGetFrequencyId.ToString());
            LogDetail.AppendLine("FreMin=" + this.FreMin.ToString());            
            if (this.CacheKeyPrefix != null)LogDetail.AppendLine("CacheKeyPrefix=" + this.CacheKeyPrefix.ToString());
            LogDetail.AppendLine("DataChange_LastTime=" + this.DataChange_LastTime.ToString());            
            return LogDetail.ToString();
        }
       
       
	}
}
