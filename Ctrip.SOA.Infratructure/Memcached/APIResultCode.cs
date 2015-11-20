using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Memcached
{
    public enum APIResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success,
        /// <summary>
        /// 失败
        /// </summary>
        Fail,
        /// <summary>
        /// 未知问题
        /// </summary>
        UnKnown,
        /// <summary>
        /// 禁止访问
        /// </summary>
        Forbidden,
        /// <summary>
        /// 错误
        /// </summary>
        Error,
    }
}
