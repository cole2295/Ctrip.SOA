using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Memcached
{
    interface ISerialize<E, S>
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        S Serialize(E entity);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="serialize"></param>
        /// <returns></returns>
        E Deserialize(S serialize);
    }
}
