using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Ctrip.SOA.Infratructure.Aop
{
    /// <summary>
    /// 耗时AOP
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class MetricElapsedAttribute : HandlerAttribute
    {
        private string _metricName;
        public MetricElapsedAttribute(string metricName)
        {
            this._metricName = metricName;
        }
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new MetricElapsedHandler(this._metricName);
        }
    }
}
