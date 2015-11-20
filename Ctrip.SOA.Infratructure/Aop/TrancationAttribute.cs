using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Ctrip.SOA.Infratructure.Aop
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class TrancationAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new TrancationHandler();
        }
    }
}
