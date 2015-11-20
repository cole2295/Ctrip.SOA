using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Memcached
{
    class BaseInvokeParameter
    {
            /// <summary>
            /// 构造函数
            /// </summary>
            public BaseInvokeParameter()
            {
            }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="cacheKey">缓存Key</param>
            /// <param name="isActByMin">是否按照分钟触发Job服务 0：false,1:true</param>
            /// <param name="conditionEntityJson">条件Json</param>
            public BaseInvokeParameter(string cacheKey, BaseInvokeType invokeType,
                int isActByMin, string conditionEntityJson)
            {
                this.cacheKey = cacheKey;
                this.invokeType = invokeType;
                this.isJobActByMin = isActByMin;
                this.conditionEntityJson = conditionEntityJson;
            }

            private string cacheKey;

            /// <summary>
            /// 缓存Key
            /// </summary>
            public string CacheKey
            {
                get { return cacheKey; }
                set { cacheKey = value; }
            }

            private BaseInvokeType invokeType;

            /// <summary>
            /// 标记是存入缓存还是取出缓存
            /// </summary>
            public BaseInvokeType InvokeType
            {
                get { return this.invokeType; }
                set { this.invokeType = value; }
            }

            private int isJobActByMin;

            /// <summary>
            /// 是不是分钟级缓存
            /// </summary>
            public int IsJobActByMin
            {
                get { return isJobActByMin; }
                set { isJobActByMin = value; }
            }

            private string conditionEntityJson;

            /// <summary>
            /// 条件Json序列化字符串
            /// </summary>
            public string ConditionEntityJson
            {
                get { return conditionEntityJson; }
                set { conditionEntityJson = value; }
            }

    }

        /// <summary>
        /// 标记是存入缓存还是取出缓存
        /// </summary>
        public enum BaseInvokeType
        {
            /// <summary>
            /// 取出缓存
            /// </summary>
            Get,

            /// <summary>
            /// 存入缓存
            /// </summary>
            Set
        }


}
