using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;
using Ctrip.SOA.Infratructure.Logging;

namespace Ctrip.SOA.Infratructure.Aop
{
    public class LogExceptionHandler : ICallHandler
    {
        private string _logMessage;
        public LogExceptionHandler(string logMessage)
        {
            this._logMessage = logMessage;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn retValue = null;
            try
            {
                retValue = getNext()(input, getNext);//执行方法
            }
            finally
            {
                if(retValue.Exception!=null)
                {
                  
                    var fullMethedName = input.MethodBase.Name;
                    //HHLogHelperV2.LOGWebSite(title, this._logMessage, fullMethedName);
                    LogHelper.WriteError(_logMessage, fullMethedName, retValue.Exception);
                }
               
               
            }
            return retValue;
        }
        public int Order { get; set; }
    }
}
