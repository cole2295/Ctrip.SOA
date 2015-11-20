using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Caching;
using System.Security.Cryptography;


namespace HHInfratructure.Memcached
{
    public class KeyMinuteManager
    {
        /// <summary>
        /// 缓存默认10分钟
        /// </summary>
        public const int DefaultMinute = 10;

        /// <summary>
        /// 获取Key，一个object生成一个值 key。只要object的每个属性值都相同，生成的key就相同。
        /// 特别注意object中的DateTime和Guid类型的属性，如果是通过DateTime.Now,Guid.NewGuid()赋值，每次key都会不同。
        /// </summary>
        public static string GetValueKey<T>(KeyPrefix keyPrefix, T obj)
        {
            if (obj == null)
            {
                return keyPrefix.ToString();
            }
            if (default(T) == null)
            {
                if (obj.GetType() == typeof(string))
                {
                    return GetStructKey(keyPrefix, obj);
                }
                return GetEntityKey(keyPrefix, obj);
            }
            else
            {
                return GetStructKey(keyPrefix, obj);
            }
        }

        /// <summary>
        /// 获取实体的Key
        /// </summary>
        private static string GetEntityKey<T>(KeyPrefix keyPrefix, T obj)
        {
            IList<object> strList = new List<object>();
            strList.Add(keyPrefix);
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
        /// 获取基元类型Key
        /// </summary>
        private static string GetStructKey(KeyPrefix keyPrefix, object obj)
        {
            IList<object> strList = new List<object>();
            strList.Add(keyPrefix);
            if (obj != null)
            {
                if (!string.IsNullOrEmpty(obj.ToString()))
                {
                    strList.Add(obj.ToString());
                }
            }
            return string.Join(CacheManager.MAIN_KEY_SPLIT_CHAR, strList);
        }

        /// <summary>
        /// 获取Key，一个object生成一个MD5 key。只要object的每个属性值都相同，生成的key就相同。
        /// 特别注意object中的DateTime和Guid类型的属性，如果是通过DateTime.Now,Guid.NewGuid()赋值，每次key都会不同。
        /// </summary>
        public static string GetMD5Key<T>(KeyPrefix keyPrefix, T obj)
        {
            //if (obj != null)
            //{
            //    SerializedType st = SerializedType.Object;
            //    byte[] buffer = Serializer.Serialize(obj, out  st, uint.MaxValue);
            //    MD5 md5 = MD5CryptoServiceProvider.Create();
            //    buffer = md5.ComputeHash(buffer);

            //    StringBuilder sb = new StringBuilder();
            //    sb.Append(keyPrefix);
            //    sb.Append(CacheManager.MAIN_KEY_SPLIT_CHAR);
            //    for (int i = 0; i < buffer.Length; i++)
            //    {
            //        sb.Append(buffer[i].ToString("x2"));
            //    }

            //    return sb.ToString();
            //}
            //return keyPrefix.ToString();
            throw new NotImplementedException();
        }

        private static PropertyInfo[] GetProperties<T>()
        {
            int cacheMinutes = 10;
            Type t = typeof(T);
            string key = string.Format("HHInfr_KeyMinuteManager_GetProperties_{0}", t.FullName);
            MemoryCache cache = MemoryCache.Default;
            if (cache[key] == null)
            {
                PropertyInfo[] properties = t.GetProperties();
                cache.Add(key, properties, new DateTimeOffset(DateTime.Now.AddMinutes(cacheMinutes)));
                return properties;
            }
            return cache[key] as PropertyInfo[];
        }
        /// <summary>
        /// 获取缓存过期时间
        /// </summary>
        /// <param name="keyPrefix">前缀</param>
        /// <returns></returns>
        public static int GetExpiredMinute(KeyPrefix keyPrefix)
        {
            //最少给后端jobws 10分钟
            int minute = GetMinute(keyPrefix.ToString());
            if (minute > 10)
            {
                return minute + DefaultMinute * 3;
            }
            else if (minute > 1)
            {
                return minute + DefaultMinute * 2;
            }
            else
            {
                return DefaultMinute;
            }
        }
        private static int GetMinute(string keyPrefix)
        {
            Dictionary<string, int> dic = CfgService.GetAllUpdateSetConfig();
            if (dic.Keys.Contains(keyPrefix))
            {
                return dic[keyPrefix];
            }
            return DefaultMinute;
        }
    }
}
