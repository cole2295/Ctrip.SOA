using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity.InterceptionExtension;
using Ctrip.SOA.Infratructure.Logging;

namespace Ctrip.SOA.Infratructure.Aop
{
    public class LogTraceHandler : ICallHandler
    {
        private string _logMessage;
        public LogTraceHandler()
        {
            this._logMessage = string.Empty;
        }
        public LogTraceHandler(string logMessage)
        {
            this._logMessage = logMessage;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn retValue = null;

            //这之前插入方法执行前的处理
            retValue = getNext()(input, getNext);//执行方法
            //这之后插入方法执行后的处理

            var title = string.Format("{0}方法已执行", input.MethodBase.Name);
            var fullMethedName = input.MethodBase.Name;
            //HHLogHelperV2.LOGWebSite(title, this._logMessage, fullMethedName);
            LogHelper.WriteLog(fullMethedName, this._logMessage);
            return retValue;
        }
        /// <summary>
        /// 这是ICallHandler的成员，表示执行顺序
        /// </summary>
        public int Order { get; set; }
    }
}