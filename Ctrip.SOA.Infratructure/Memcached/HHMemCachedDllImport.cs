using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arch.App.Cache.Client;

namespace Ctrip.SOA.Infratructure.Memcached
{
    public class HHMemCachedDllImport 
    {
        public static IBaseInvokeClass BaseInvokeClassInstance=null; 

        /// <summary>
        /// 缓存SDK
        /// </summary>
        private static ICacheProvider cacheProvider;

        /// <summary>
        /// 并发锁
        /// </summary>
        public static object asyncLock = new object();

        /// <summary>
        /// 相对时间有效期
        /// </summary>
        private static TimeSpan timeSpan;

        
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static HHMemCachedDllImport()
        {
            timeSpan = new TimeSpan(0, 60, 0);
            Init();
        }

        private static void Init()
        {
            lock (asyncLock)
            {
                if (cacheProvider == null)
                {
                    try
                    {
                        cacheProvider = CacheProvider.GetPorvider("HHtravel.Booking");//建议只调用一次 并将GetPorvider的值保存在静态变量中
                    }
                    catch (Exception ex)
                    {
                        
                        //初始化异常，有可能是配置文件错误，网络异常或其他异常。请查看ex获取详细信息，注意ex.Type
                    }
                    if (cacheProvider == null)
                    {
                        //获取不到ICacheProvider，很可能是name传错了，不存在
                    }
                }
            }
        }
 

        /// <summary>
        /// 缓存压入
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static bool Set(string key, object value)
        {
            if (cacheProvider == null)
            {
                Init();
            }
            try
            {
                //不建议存储大对象 例如超过1M Size的对象
                Boolean result = cacheProvider.Set(key, value, timeSpan);
                return result;
            }
            catch//注意可能会有异常
            {
                //处理异常 请查看ex获取详细信息，注意ex.Type
                return false;
            }
        }

        /// <summary>
        /// 缓存压入
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        internal static bool Set(string key, object value, int time, string conditionEntityJson)
        {
            if (cacheProvider == null)
            {
                Init();
            }
            try
            {
                //不建议存储大对象 例如超过1M Size的对象
                TimeSpan timeSpan = new TimeSpan(time, 0, 0);

                Boolean result = cacheProvider.Set(key, value, timeSpan);
                if (result && BaseInvokeClassInstance != null && time >= 24)
                {
                    BaseInvokeParameter para = new BaseInvokeParameter(key, BaseInvokeType.Set, 0, conditionEntityJson);
                    BaseInvokeClassInstance.Invoke(para);
                }
                return result;
            }
            catch//注意可能会有异常
            {
                //处理异常 请查看ex获取详细信息，注意ex.Type
                return false;
            }
        }

        /// <summary>
        /// 缓存压入
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">值</param>
        /// <param name="time">小时</param>
        /// <param name="minute">分钟</param>
        /// <param name="conditionEntityJson">CallEntity条件</param>
        /// <param name="insertToDB">是否插入数据库</param>
        /// <returns></returns>
        internal static bool Set(string key, object value, int time, int minute, string conditionEntityJson, bool insertToDB)
        {
            if (cacheProvider == null)
            {
                Init();
            }
            try
            {
                //不建议存储大对象 例如超过1M Size的对象
                TimeSpan timeSpan = new TimeSpan(time, minute, 0);
                Boolean result = cacheProvider.Set(key, value, timeSpan);
                if (time != 0)//小时级大小等于24小时，插入数据库
                {
                    if (result && BaseInvokeClassInstance != null && timeSpan.Hours >= 24 && insertToDB)
                    {
                        BaseInvokeParameter para = new BaseInvokeParameter(key, BaseInvokeType.Set, 0, conditionEntityJson);
                        BaseInvokeClassInstance.Invoke(para);
                    }
                }
                else if (minute != 0)//分钟级大于等于5分钟，插入数据库
                {
                    if (result && BaseInvokeClassInstance != null && minute > 4 && insertToDB)
                    {
                        BaseInvokeParameter para = new BaseInvokeParameter(key, BaseInvokeType.Set, 1, conditionEntityJson);
                        BaseInvokeClassInstance.Invoke(para);
                    }
                }
                return result;
            }
            catch//注意可能会有异常
            {
                //处理异常 请查看ex获取详细信息，注意ex.Type
                return false;
            }
        }


        /// <summary>
        /// 缓存压入
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">值</param>
        /// <param name="timespan">timespan</param>
        /// <param name="conditionEntityJson">CallEntity条件</param>
        /// <param name="insertToDB">是否插入数据库</param>
        /// <returns></returns>
        internal static bool Set(string key, object value, TimeSpan timeSpan, string conditionEntityJson, bool insertToDB)
        {
            if (cacheProvider == null)
            {
                Init();
            }
            try
            {
                //不建议存储大对象 例如超过1M Size的对象

                Boolean result = cacheProvider.Set(key, value, timeSpan);
                //小时级大小等于24小时，插入数据库
               
                if (result && BaseInvokeClassInstance != null && timeSpan.Hours >= 24 && insertToDB)
                {
                    BaseInvokeParameter para = new BaseInvokeParameter(key, BaseInvokeType.Set, 0, conditionEntityJson);
                    BaseInvokeClassInstance.Invoke(para);
                }
                
             
                return result;
            }
            catch//注意可能会有异常
            {
                //处理异常 请查看ex获取详细信息，注意ex.Type
                return false;
            }
        }

        /// <summary>
        /// 缓存获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static object Get(string key, int time, int minute, bool insertToDB)
        {
            if (cacheProvider == null)
            {
                Init();
            }
            try
            {
                //不建议存储大对象 例如超过1M Size的对象，  
                object result = cacheProvider.Get(key);
                if (time != 0)//小时级大小等于24小时，插入数据库
                {
                    if (result!=null && BaseInvokeClassInstance != null && timeSpan.Hours >= 24 && insertToDB)
                    {
                        BaseInvokeParameter para = new BaseInvokeParameter(key, BaseInvokeType.Get, 0, string.Empty);
                        BaseInvokeClassInstance.Invoke(para);
                    }
                }
                else if (minute != 0)//分钟级大于等于5分钟，插入数据库
                {
                    if (result!=null && BaseInvokeClassInstance != null && minute > 4 && insertToDB)
                    {
                        BaseInvokeParameter para = new BaseInvokeParameter(key, BaseInvokeType.Get, 1, string.Empty);
                        BaseInvokeClassInstance.Invoke(para);
                    }
                }
                return result;
            }
            catch//注意可能会有异常
            {
                return null;
            }
        }

        /// <summary>
        /// 缓存获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static object Get(string key, int time)
        {
            if (cacheProvider == null)
            {
                Init();
            }
            try
            {
                //不建议存储大对象 例如超过1M Size的对象，  
                object result = cacheProvider.Get(key);
                if (result != null && BaseInvokeClassInstance != null && time >= 24)//如果不为空，执行访问Key次数累加
                {
                    BaseInvokeParameter para = new BaseInvokeParameter(key, BaseInvokeType.Get, 0, string.Empty);
                    BaseInvokeClassInstance.Invoke(para);
                }
                return result;
            }
            catch//注意可能会有异常
            {
                return null;
            }
        }  
        
        /// <summary>
        /// 缓存获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static object Get(string key)
        {
            if (cacheProvider == null)
            {
                Init();
            }
            try
            {
                //不建议存储大对象 例如超过1M Size的对象，  
                object result = cacheProvider.Get(key);
                return result;
            }
            catch//注意可能会有异常
            {
                return null;
            }
        }

        /// <summary>
        /// 移除项目
        /// </summary>
        /// <param name="key"></param>
        internal static void MoveItem(string key)
        {
            if (cacheProvider == null)
            {
                Init();
            }
            try
            {
                object result = cacheProvider.Delete(key);
            }
            catch
            {
            }
        }

    }
}
