using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using Ctrip.SOA.Infratructure.Logging;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Ctrip.SOA.Infratructure.Aop
{
    /// <summary>
    /// 测试耗时
    /// </summary>
    public class MetricElapsedHandler : ICallHandler
    {
        private string _methodName;
        public MetricElapsedHandler(string methodName)
        {
            this._methodName = methodName;
        }
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn retValue = null;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            retValue = getNext()(input, getNext);//执行方法
            sw.Stop();

            long elapsedMilliseconds = sw.ElapsedMilliseconds > long.MaxValue ? long.MaxValue : (long)sw.ElapsedMilliseconds;
            var appId = System.Configuration.ConfigurationManager.AppSettings["appId"];
            //HHLogHelperV2.MetricElapsed(appId, _methodName, elapsedMilliseconds);
            LogHelper.WriteLog(_methodName, "appid:" + appId.ToString() + ",methodname:" + _methodName + ",time:" + elapsedMilliseconds.ToString());
            return retValue;
        }
        public int Order { get; set; }
    }
}
