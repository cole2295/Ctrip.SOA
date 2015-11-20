using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Ctrip.SOA.Infratructure.Aop
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class LogExceptionAttribute : HandlerAttribute
    {
        //TODO:可考虑与LogTraceAttribute合并
        private string _logMessage;
        public LogExceptionAttribute(string logMessage)
        {
            this._logMessage = logMessage;
        }
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new LogExceptionHandler(this._logMessage);
        }
    }
}