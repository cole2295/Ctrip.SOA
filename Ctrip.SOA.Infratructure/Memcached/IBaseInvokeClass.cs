using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Memcached
{
    public interface IBaseInvokeClass
    {
        /// <summary>
        /// 回调接口
        /// </summary>
        /// <param name="para"></param>
        void Invoke(object para);

    }
}
