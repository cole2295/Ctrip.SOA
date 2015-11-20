using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Ctrip.SOA.Infratructure.Memcached;
using Ctrip.SOA.Infratructure.Serialization.Json;
using Newtonsoft.Json;

namespace Ctrip.SOA.Infratructure.Memcached
{
    public static class CacheManager
    {
        #region 公共变量

        /// <summary>
        /// 主Key和条件字符分割符"$"
        /// </summary>
        public const string MAIN_KEY_SPLIT_CHAR = "$";
        /// <summary>
        /// 分页分割符"#"
        /// </summary>
        public const string PAGE_SPLIT_CHAR = "#";

        /// <summary>
        /// 缓存默认10分钟
        /// </summary>
        public const int MinuteDefault = 10;
        /// <summary>
        /// 缓存20分钟
        /// </summary>
        public const int Minute20 = 20;
        /// <summary>
        /// 缓存30分钟
        /// </summary>
        public const int Minute30 = 30;
        /// <summary>
        /// 缓存60分钟
        /// </summary>
        public const int Minute60 = 60;
        /// <summary>
        /// 缓存120分钟
        /// </summary>
        public const int Minute120 = 120;
        /// <summary>
        /// 缓存1440分钟（1天）
        /// </summary>
        public const int Minute24Hour = 60 * 24;
        /// <summary>
        /// 缓存默认1小时
        /// </summary>
        public const int HourDefault = 1;
        /// <summary>
        /// 缓存2小时
        /// </summary>
        public const int Hour2 = 2;
        /// <summary>
        /// 缓存12小时
        /// </summary>
        public const int Hour12 = 12;
        /// <summary>
        /// 缓存24小时
        /// </summary>
        public const int Hour24 = 24;

        #endregion 公共变量

        #region set object

        /// <summary>
        /// 缓存设置 added by Pluto Mei 2014.2.24
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">缓存值</param>
        public static object GetObject(string key)
        {
            return HHMemCachedDllImport.Get(key);
        }

        /// <summary>
        /// 缓存设置 added by Pluto Mei 2014.2.24
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">缓存值</param>
        public static void SetObject<T>(string key, T value)
        {
            HHMemCachedDllImport.Set(key, value, int.MaxValue, "");
        }

        /// <summary>
        /// 缓存设置 added by lgq 2014.4.18
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存KEY</param>
        /// <param name="value">缓存值</param>
        /// <param name="time">缓存时长</param>
        public static void SetObject<T>(string key, T value, int time)
        {
            HHMemCachedDllImport.Set(key, value, time, "");
            
        }

        public static void SetObject<T>(string key, T value, int min, bool intodb)
        {
            HHMemCachedDllImport.Set(key, value, TimeSpan.FromMinutes(min), "", intodb);

        }

        #endregion set object

        #region 返回Object

        /// <summary>
        /// 缓存获取
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="callGetLogic">调用逻辑</param>
        /// <returns>返回调用结果集合</returns>
        public static TResult GetObject<T, TResult>(string key, Func<T, TResult> callGetLogic, T callEntity)
        {
            //Added by Pluto Mei
            var tResult = HHMemCachedDllImport.Get(key);
            TResult result = default(TResult);
            if (tResult == null)
            {
                tResult = callGetLogic(callEntity);
                if (result != null)
                {
                    string jsonCallEnity = ConvertEnityToJson(callEntity);
                    HHMemCachedDllImport.Set(key, tResult, int.MaxValue, jsonCallEnity);
                }
            }
            else
            {
                result = (TResult)tResult;
            }

            return result;
            //如果返回类型为值类型，在第一次初始化的时候下面方法将会引发报错
            //note by Pluto Mei 2014-2-24
            //TResult result = (TResult)HHMemCachedDllImport.Get(key);
            //if (result == null)
            //{
            //    result = callGetLogic(callEntity);

            //    if (result != null)
            //    {
            //        HHMemCachedDllImport.Set(key, result);
            //    }
            //}
            //return result;
        }


        public static TResult GetObject<TResult>(string key, Func<TResult> callGetLogic)
        {
            //Added by Pluto Mei
            var tResult = HHMemCachedDllImport.Get(key);
            TResult result = default(TResult);
            if (tResult == null)
            {
                tResult = callGetLogic();
                
                if (tResult != null)
                {
                    //string jsonCallEnity = ConvertEnityToJson();
                    result = (TResult)tResult; 
                    HHMemCachedDllImport.Set(key, tResult);
                }
            }
            else
            {
                result = (TResult)tResult;
            }

            return result;
            //如果返回类型为值类型，在第一次初始化的时候下面方法将会引发报错
            //note by Pluto Mei 2014-2-24
            //TResult result = (TResult)HHMemCachedDllImport.Get(key);
            //if (result == null)
            //{
            //    result = callGetLogic(callEntity);

            //    if (result != null)
            //    {
            //        HHMemCachedDllImport.Set(key, result);
            //    }
            //}
            //return result;
        }
        /// <summary>
        /// 缓存获取
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="callGetLogic">调用逻辑</param>
        /// <param name="time">缓存时间 单位小时</param>
        /// <returns>返回调用结果集合</returns>
        public static TResult GetObject<T, TResult>(string key, Func<T, TResult> callGetLogic, T callEntity, int time)
        {
            object result = HHMemCachedDllImport.Get(key, time);
            if (result == null)
            {
                result = callGetLogic(callEntity);

                if (result != null)
                {
                    string jsonCallEnity = ConvertEnityToJson(callEntity);
                    HHMemCachedDllImport.Set(key, result, time, jsonCallEnity);
                }
            }
            return (TResult)result;
        }

        /// <summary>
        /// 缓存获取
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="time">缓存时间 单位小时</param>
        /// <returns>返回调用结果集合</returns>
        public static TResult GetObject<TResult>(string key)
        {
            object result = HHMemCachedDllImport.Get(key);

            return (TResult)result;
        }

        /// <summary>
        /// 缓存获取
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="callGetLogic">调用逻辑</param>
        /// <param name="time">缓存时间 单位分钟</param>
        /// <returns>返回调用结果集合</returns>
        public static TResult GetObjectByMin<T, TResult>(string key, Func<T, TResult> callGetLogic, T callEntity, int minute)
        {
            //关闭请开启以下语句
            //return callGetLogic(callEntity);

            object result = null;
            int requestTimes = 0;
            while (requestTimes < 2)//取2次，防止取缓存时在Set，取不到数据
            {
                result = HHMemCachedDllImport.Get(key, 0, minute, true);
                if (result == null) Thread.Sleep(10);
                else break;
                requestTimes++;
            }
            if (result == null)
            {
                result = callGetLogic(callEntity);
                if (result != null)
                {
                    string jsonCallEnity = ConvertEnityToJson(callEntity);
                    HHMemCachedDllImport.Set(key, result, 0, minute, jsonCallEnity, true);
                    
                }
            }
            return (TResult)result;
        }

        /// <summary>
        /// 缓存获取
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="callGetLogic">调用逻辑</param>
        /// <param name="time">缓存时间 单位分钟</param>
        /// <returns>返回调用结果集合</returns>
        public static TResult GetObjectByTimeSpan<T, TResult>(string key, Func<T, TResult> callGetLogic, T callEntity, TimeSpan timeSpan)
        {
            //关闭请开启以下语句
            //return callGetLogic(callEntity);

            object result = null;
            int requestTimes = 0;
            while (requestTimes < 2)//取2次，防止取缓存时在Set，取不到数据
            {
                result = HHMemCachedDllImport.Get(key);
                if (result == null) Thread.Sleep(10);
                else break;
                requestTimes++;
            }
            if (result == null)
            {
                result = callGetLogic(callEntity);
                if (result != null)
                {
                    string jsonCallEnity = ConvertEnityToJson(callEntity);
                    HHMemCachedDllImport.Set(key, result, timeSpan, jsonCallEnity, true);
                }
            }
            return (TResult)result;
        }

        /// <summary>
        /// 缓存获取
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="callGetLogic">调用逻辑</param>
        /// <param name="time">缓存时间 单位分钟</param>
        /// <returns>返回调用结果集合</returns>
        public static TResult GetObjectByMin<T, TResult>(string key, Func<T, TResult> callGetLogic, T callEntity, int minute, bool insertToDB)
        {
            object result = null;
            int requestTimes = 0;
            while (requestTimes < 3)//取三次，防止取缓存时在Set，取不到数据
            {
                result = HHMemCachedDllImport.Get(key, 0, minute, insertToDB);
                if (result == null) Thread.Sleep(20);
                else break;
                requestTimes++;
            }
            if (result == null)
            {
                result = callGetLogic(callEntity);
                if (result != null)
                {
                    string jsonCallEnity = ConvertEnityToJson(callEntity);
                    HHMemCachedDllImport.Set(key, result, 0, minute, jsonCallEnity, insertToDB);
                }
            }
            return (TResult)result;
        }

        /// <summary>
        /// 缓存获取-可实现整取切片缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="callGetLogic">调用逻辑</param>
        /// <param name="time">缓存时间 单位小时</param>
        /// <returns>返回调用结果集合</returns>
        public static object GetMergeObject<T, TResult>(string key, Func<T, TResult> callGetLogic, T callEntity, int time)
        {
            object keyCount = HHMemCachedDllImport.Get(key, time);
            object[] result = null;
            if (keyCount == null || !(keyCount is int) || Convert.ToInt32(keyCount) == 0)//不存在缓存
            {
                result = callGetLogic(callEntity) as object[];
                if (result != null)
                {
                    string jsonCallEnity = ConvertEnityToJson(callEntity);
                    for (int i = 0; i < result.Length; i++)
                    {
                        HHMemCachedDllImport.Set(key + PAGE_SPLIT_CHAR + i.ToString(), result[i], time, string.Empty);
                    }
                    HHMemCachedDllImport.Set(key, result.Length, time, jsonCallEnity);//只保存到条件Key的字段中
                }
            }
            else
            {
                List<object> cacheObjectList = new List<object>();
                int count = Convert.ToInt32(keyCount);
                for (int i = 0; i < count; i++)
                {
                    string itemKey = key + PAGE_SPLIT_CHAR + i.ToString();
                    if (!CacheManager.IsExit(itemKey))//缺失一个Key，全部重新Get
                    {
                        if (result == null)//如果不为NULL，表示之前有发生缺失，已经取过接口，将不再重新调用SOA
                            result = callGetLogic(callEntity) as object[];
                        if (result != null && i < result.Length)//不为空，且页码在result的结果集中
                        {
                            string jsonCallEnity = ConvertEnityToJson(callEntity);
                            object soaItem = result[i];
                            HHMemCachedDllImport.Set(itemKey, soaItem, time, string.Empty);
                            cacheObjectList.Add(soaItem);//添加到退回列表中
                            HHMemCachedDllImport.Set(key, result.Length, time, jsonCallEnity);//主Key更新到最新时间
                        }
                    }
                    else
                    {
                        object item = HHMemCachedDllImport.Get(itemKey, time);
                        cacheObjectList.Add(item);
                    }
                }
                result = cacheObjectList.ToArray();
            }
            return result;
        }

        /// <summary>
        /// 缓存获取
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="callGetLogic">调用逻辑</param>
        /// <param name="time">缓存时间 单位分钟</param>
        /// <returns>返回调用结果集合</returns>
        public static object GetMergeObjectByMin<T, TResult>(string key, Func<T, TResult> callGetLogic, T callEntity, int minute)
        {
            object keyCount = null;
            int requestTimes = 0;
            while (requestTimes < 3)//取三次，防止取缓存时在Set，取不到数据
            {
                keyCount = HHMemCachedDllImport.Get(key, 0, minute, true);
                if (keyCount == null) Thread.Sleep(20);
                else break;
                requestTimes++;
            }
            object[] result = null;
            if (keyCount == null || !(keyCount is int) || Convert.ToInt32(keyCount) == 0)//不存在缓存
            {
                result = callGetLogic(callEntity) as object[];
                if (result != null)
                {
                    string jsonCallEnity = ConvertEnityToJson(callEntity);
                    for (int i = 0; i < result.Length; i++)
                    {
                        HHMemCachedDllImport.Set(key + PAGE_SPLIT_CHAR + i.ToString(), result[i], 0, minute, string.Empty, true);
                    }
                    HHMemCachedDllImport.Set(key, result.Length, 0, minute, jsonCallEnity, true);
                }
            }
            else
            {
                List<object> cacheObjectList = new List<object>();
                int count = Convert.ToInt32(keyCount);
                for (int i = 0; i < count; i++)
                {
                    string itemKey = key + PAGE_SPLIT_CHAR + i.ToString();
                    object item = null;
                    requestTimes = 0;
                    while (requestTimes < 3)//取三次，防止取缓存时在Set，取不到数据
                    {
                        item = HHMemCachedDllImport.Get(itemKey, 0, minute, true);
                        if (item == null) Thread.Sleep(20);
                        else break;
                        requestTimes++;
                    }
                    if (item == null)//缺失一个Key，全部重新Get
                    {
                        if (result == null)//如果不为NULL，表示之前有发生缺失，已经取过接口，将不再重新调用SOA
                            result = callGetLogic(callEntity) as object[];
                        if (result != null && i < result.Length)//不为空，且页码在result的结果集中
                        {
                            string jsonCallEnity = ConvertEnityToJson(callEntity);
                            object soaItem = result[i];
                            HHMemCachedDllImport.Set(itemKey, soaItem, 0, minute, string.Empty, true);
                            cacheObjectList.Add(soaItem);//添加到退回列表中
                            HHMemCachedDllImport.Set(key, result.Length, 0, minute, jsonCallEnity, true);//主Key更新到最新时间
                        }
                    }
                    else
                    {
                        cacheObjectList.Add(item);
                    }
                }
                result = cacheObjectList.ToArray();
            }
            return result;
        }

        #endregion 返回Object

        #region 返回ByteSliceEntity,以字节为切片对象

        /// <summary>
        /// 缓存获取-ByteSliceEntity
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="callGetLogic">调用逻辑</param>
        /// <returns>返回调用结果集合</returns>
        public static ByteSliceEntity GetByteSliceEntity<T, TResult>(string key, Func<T, TResult> callGetLogic, T callEntity, bool isForceCall = false)
        {
            ByteSliceEntity result = HHMemCachedDllImport.Get(key) as ByteSliceEntity;
            if (result == null || isForceCall)
            {
                result = callGetLogic(callEntity) as ByteSliceEntity;

                if (result != null && result.CallSuccess)
                {
                    HHMemCachedDllImport.Set(key, result);
                }
            }
            else//来自缓存的标记
            {
                result.IsGetFromCache = true;
            }
            return result;
        }

        /// <summary>
        /// 缓存获取
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="callGetLogic">调用逻辑</param>
        /// <param name="time">缓存时间 单位小时</param>
        /// <returns>返回调用结果集合</returns>
        public static ByteSliceEntity GetByteSliceEntity<T, TResult>(string key, Func<T, TResult> callGetLogic, T callEntity, int time, bool isForceCall = false)
        {
            ByteSliceEntity result = null;
            if (isForceCall)//强制调用时（Job调用），不计数
            {
                result = callGetLogic(callEntity) as ByteSliceEntity;
                if (result != null && result.CallSuccess)
                {
                    string jsonCallEnity = ConvertEnityToJson(callEntity);
                    HHMemCachedDllImport.Set(key, result, time, jsonCallEnity);
                }
            }
            else
            {
                result = HHMemCachedDllImport.Get(key, time) as ByteSliceEntity;
                if (result == null)
                {
                    result = callGetLogic(callEntity) as ByteSliceEntity;

                    if (result != null && result.CallSuccess)
                    {
                        string jsonCallEnity = ConvertEnityToJson(callEntity);
                        HHMemCachedDllImport.Set(key, result, time, jsonCallEnity);
                    }
                }
                else//来自缓存的标记
                {
                    result.IsGetFromCache = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 缓存获取
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="callGetLogic">调用逻辑</param>
        /// <param name="time">缓存时间 单位分钟</param>
        /// <returns>返回调用结果集合</returns>
        public static ByteSliceEntity GetByteSliceEntityByMin<T, TResult>(string key, Func<T, TResult> callGetLogic, T callEntity, int minute, bool isForceCall = false)
        {
            ByteSliceEntity result = null;
            if (isForceCall)//强制调用时（Job调用），不计数
            {
                result = callGetLogic(callEntity) as ByteSliceEntity;
                if (result != null && result.CallSuccess)
                {
                    string jsonCallEnity = ConvertEnityToJson(callEntity);
                    HHMemCachedDllImport.Set(key, result, 0, minute, jsonCallEnity, true);
                }
            }
            else
            {
                int requestTimes = 0;
                while (requestTimes < 3)//取三次，防止取缓存时在Set，取不到数据
                {
                    result = HHMemCachedDllImport.Get(key, 0, minute, true) as ByteSliceEntity;
                    if (result == null) Thread.Sleep(20);
                    else break;
                    requestTimes++;
                }
                if (result == null)
                {
                    result = callGetLogic(callEntity) as ByteSliceEntity;
                    if (result != null && result.CallSuccess)
                    {
                        string jsonCallEnity = ConvertEnityToJson(callEntity);
                        HHMemCachedDllImport.Set(key, result, 0, minute, jsonCallEnity, true);
                    }
                }
                else//来自缓存的标记
                {
                    result.IsGetFromCache = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 缓存获取
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="callGetLogic">调用逻辑</param>
        /// <param name="time">缓存时间 单位分钟</param>
        /// <returns>返回调用结果集合</returns>
        public static ByteSliceEntity[] GetMergeByteSliceEntityByMin<T, TResult>(string key, Func<T, TResult> callGetLogic, T callEntity, int minute, bool isForceCall = false)
        {
            ByteSliceEntity[] result = null;
            if (isForceCall)
            {
                result = callGetLogic(callEntity) as ByteSliceEntity[];
                if (result != null && result.Length > 0 && result[0].CallSuccess)
                {
                    string jsonCallEnity = ConvertEnityToJson(callEntity);
                    for (int i = 0; i < result.Length; i++)
                    {
                        HHMemCachedDllImport.Set(key + PAGE_SPLIT_CHAR + i.ToString(), result[i], 0, minute, string.Empty, true);
                        Thread.Sleep(1);
                    }
                    HHMemCachedDllImport.Set(key, result.Length, 0, minute, jsonCallEnity, true);
                }
            }
            else
            {
                object keyCount = null;
                int requestTimes = 0;
                while (requestTimes < 3)//取三次，防止取缓存时在Set，取不到数据
                {
                    keyCount = HHMemCachedDllImport.Get(key, 0, minute, true);
                    if (keyCount == null) Thread.Sleep(20);
                    else break;
                    requestTimes++;
                }
                if (keyCount == null || !(keyCount is int) || Convert.ToInt32(keyCount) == 0)//不存在缓存
                {
                    result = callGetLogic(callEntity) as ByteSliceEntity[];
                    if (result != null && result.Length > 0 && result[0].CallSuccess)
                    {
                        string jsonCallEnity = ConvertEnityToJson(callEntity);
                        for (int i = 0; i < result.Length; i++)
                        {
                            HHMemCachedDllImport.Set(key + PAGE_SPLIT_CHAR + i.ToString(), result[i], 0, minute, string.Empty, true);
                            Thread.Sleep(1);
                        }
                        HHMemCachedDllImport.Set(key, result.Length, 0, minute, jsonCallEnity, true);
                    }
                }
                else
                {
                    List<ByteSliceEntity> cacheObjectList = new List<ByteSliceEntity>();
                    int count = Convert.ToInt32(keyCount);
                    for (int i = 0; i < count; i++)
                    {
                        string itemKey = key + PAGE_SPLIT_CHAR + i.ToString();
                        ByteSliceEntity item = null;
                        requestTimes = 0;
                        while (requestTimes < 3)//取三次，防止取缓存时在Set，取不到数据
                        {
                            item = HHMemCachedDllImport.Get(itemKey, 0, minute, true) as ByteSliceEntity;
                            if (item == null) Thread.Sleep(20);
                            else break;
                            requestTimes++;
                        }
                        if (item == null)//缺失一个Key，全部重新Get
                        {
                            cacheObjectList.Clear();
                            result = callGetLogic(callEntity) as ByteSliceEntity[];
                            if (result != null && i < result.Length && result[0].CallSuccess)//不为空，且页码在result的结果集中
                            {
                                string jsonCallEnity = ConvertEnityToJson(callEntity);
                                for (int j = 0; j < result.Length; j++)
                                {
                                    itemKey = key + PAGE_SPLIT_CHAR + j.ToString();
                                    ByteSliceEntity soaItem = result[j];
                                    HHMemCachedDllImport.Set(itemKey, soaItem, 0, minute, string.Empty, true);
                                    cacheObjectList.Add(soaItem);//添加到返回列表中
                                    Thread.Sleep(1);
                                }
                                HHMemCachedDllImport.Set(key, result.Length, 0, minute, jsonCallEnity, true);//主Key更新到最新时间
                                break;
                            }
                        }
                        else
                        {
                            item.IsGetFromCache = true;
                            cacheObjectList.Add(item);
                        }
                    }
                    result = cacheObjectList.ToArray();
                }
            }
            return result;
        }

        /// <summary>
        /// 缓存获取-可实现整取切片缓存
        /// </summary>
        /// <param name="key">缓存KEY</param>
        /// <param name="callGetLogic">调用逻辑</param>
        /// <param name="time">缓存时间 单位小时</param>
        /// <returns>返回调用结果集合</returns>
        public static ByteSliceEntity[] GetMergeByteSliceEntity<T, TResult>(string key, Func<T, TResult> callGetLogic, T callEntity, int time, bool isForceCall = false)
        {
            ByteSliceEntity[] result = null;
            if (isForceCall)
            {
                result = callGetLogic(callEntity) as ByteSliceEntity[];
                if (result != null && result.Length > 0 && result[0].CallSuccess)
                {
                    string jsonCallEnity = ConvertEnityToJson(callEntity);
                    for (int i = 0; i < result.Length; i++)
                    {
                        HHMemCachedDllImport.Set(key + PAGE_SPLIT_CHAR + i.ToString(), result[i], time, string.Empty);
                        Thread.Sleep(0);
                    }
                    HHMemCachedDllImport.Set(key, result.Length, time, jsonCallEnity);
                }
            }
            else
            {
                object keyCount = HHMemCachedDllImport.Get(key, time);
                if (keyCount == null || !(keyCount is int) || Convert.ToInt32(keyCount) == 0)//不存在缓存
                {
                    result = callGetLogic(callEntity) as ByteSliceEntity[];
                    if (result != null && result.Length > 0 && result[0].CallSuccess)
                    {
                        string jsonCallEnity = ConvertEnityToJson(callEntity);
                        for (int i = 0; i < result.Length; i++)
                        {
                            HHMemCachedDllImport.Set(key + PAGE_SPLIT_CHAR + i.ToString(), result[i], time, string.Empty);
                            Thread.Sleep(0);
                        }
                        HHMemCachedDllImport.Set(key, result.Length, time, jsonCallEnity);
                    }
                }
                else
                {
                    List<ByteSliceEntity> cacheObjectList = new List<ByteSliceEntity>();
                    int count = Convert.ToInt32(keyCount);
                    for (int i = 0; i < count; i++)
                    {
                        string itemKey = key + PAGE_SPLIT_CHAR + i.ToString();
                        ByteSliceEntity item = HHMemCachedDllImport.Get(itemKey, time) as ByteSliceEntity;
                        if (item == null)//缺失一个Key，全部重新Get
                        {
                            cacheObjectList.Clear();
                            result = callGetLogic(callEntity) as ByteSliceEntity[];
                            if (result != null && i < result.Length && result[0].CallSuccess)//不为空，且页码在result的结果集中
                            {
                                string jsonCallEnity = ConvertEnityToJson(callEntity);
                                for (int j = 0; j < result.Length; j++)
                                {
                                    itemKey = key + PAGE_SPLIT_CHAR + j.ToString();
                                    ByteSliceEntity soaItem = result[j];
                                    HHMemCachedDllImport.Set(itemKey, soaItem, 0, time, string.Empty, true);
                                    cacheObjectList.Add(soaItem);//添加到返回列表中
                                    Thread.Sleep(0);
                                }
                                HHMemCachedDllImport.Set(key, result.Length, time, jsonCallEnity);//主Key更新到最新时间
                                break;
                            }
                        }
                        else
                        {
                            item.IsGetFromCache = true;
                            cacheObjectList.Add(item);
                        }
                    }
                    result = cacheObjectList.ToArray();
                }
            }
            return result;
        }

        #endregion 返回ByteSliceEntity,以字节为切片对象

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public static void MoveItem(string key)
        {
            HHMemCachedDllImport.MoveItem(key);
        }

        /// <summary>
        /// 判断是否存在缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Boolean IsExit(string key)
        {
            try
            {
                if (HHMemCachedDllImport.Get(key, 0) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 取CallEnity的Json格式
        /// </summary>
        /// <param name="callEnity"></param>
        /// <returns></returns>
        private static string ConvertEnityToJson(object callEnity)
        {
            string jsonCallEntity = string.Empty;
            if (callEnity != null)
            {
                Type callType = callEnity.GetType();
                jsonCallEntity = callType.FullName + "#" + new NewtonsoftJsonSerializer().SerializeToString(callEnity);
            }
            return jsonCallEntity;
        }

        /// <summary>
        /// 获取Key， 一个object生成一个值 key。只要object的每个属性值都相同，生成的key就相同。
        /// 特别注意object中的DateTime和Guid类型的属性，如果是通过DateTime.Now,Guid.NewGuid()赋值，每次key都会不同。
        /// </summary>
        public static string GetValueKey<T>(T obj) where T : class
        {
            IList<object> strList = new List<object>();
            strList.Add(typeof(T).FullName);
            if (obj != null)
            {
                PropertyInfo[] properties = GetProperties<T>();
                foreach (var p in properties)
                {
                    object value = p.GetValue(obj, null);
                    if (value != null)
                    {
                        strList.Add(value.ToString().Replace(CacheManager.MAIN_KEY_SPLIT_CHAR.ToString(), CacheManager.MAIN_KEY_SPLIT_CHAR.ToString() + CacheManager.MAIN_KEY_SPLIT_CHAR.ToString()));
                    }
                    else
                    {
                        strList.Add(string.Empty);
                    }
                }
            }
            return string.Join(CacheManager.MAIN_KEY_SPLIT_CHAR, strList);
        }

        /// <summary>
        /// 获取Key，一个object生成一个MD5 key。只要object的每个属性值都相同，生成的key就相同。
        /// 特别注意object中的DateTime和Guid类型的属性，如果是通过DateTime.Now,Guid.NewGuid()赋值，每次key都会不同。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetMD5Key<T>(T obj)
        {
            //if (obj != null)
            //{
            //    SerializedType st = SerializedType.Object;
            //    byte[] buffer = Serializer.Serialize(obj, out  st, uint.MaxValue);
            //    MD5 md5 = MD5CryptoServiceProvider.Create();
            //    buffer = md5.ComputeHash(buffer);

            //    StringBuilder sb = new StringBuilder();
            //    sb.Append(CacheManager.MAIN_KEY_SPLIT_CHAR);
            //    sb.Append(obj.GetType().FullName);
            //    sb.Append(CacheManager.MAIN_KEY_SPLIT_CHAR);
            //    for (int i = 0; i < buffer.Length; i++)
            //    {
            //        sb.Append(buffer[i].ToString("x2"));
            //    }

            //    return sb.ToString();
            //}
            //return string.Empty;

            throw new NotImplementedException();
        }

        private static PropertyInfo[] GetProperties<T>() where T : class
        {
            int cacheMinutes = MinuteDefault;
            Type t = typeof(T);
            string key = string.Format("HHInfr_CacheManager_GetProperties_{0}", t.FullName);
            MemoryCache cache = MemoryCache.Default;
            if (cache[key] == null)
            {
                PropertyInfo[] properties = t.GetProperties();
                cache.Add(key, properties, new DateTimeOffset(DateTime.Now.AddMinutes(cacheMinutes)));
                return properties;
            }
            return cache[key] as PropertyInfo[];
        }
    }
}