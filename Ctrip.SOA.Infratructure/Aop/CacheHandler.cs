using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ctrip.SOA.Infratructure.Memcached;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Ctrip.SOA.Infratructure.Aop
{
    public class CacheHandler : ICallHandler
    {
         private string _key;
         public CacheHandler(string key)
        {
            this._key = key;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn retValue = null;

            retValue = CacheManager.GetObject(_key, () =>
            {

                retValue = getNext()(input, getNext);
                return retValue;

            });

            return retValue;
        }
        public int Order { get; set; }
    }
}
