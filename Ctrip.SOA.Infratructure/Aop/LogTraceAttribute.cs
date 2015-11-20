using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Ctrip.SOA.Infratructure.Aop
{
    /// <summary>
    /// LogTrace日志记录
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class LogTraceAttribute : HandlerAttribute
    {
        private string _logMessage;
        public LogTraceAttribute()
        {
            _logMessage = string.Empty;
        }
        public LogTraceAttribute(string logMessage)
        {
            this._logMessage = logMessage;
        }
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new LogTraceHandler(this._logMessage);
        }
    }
}