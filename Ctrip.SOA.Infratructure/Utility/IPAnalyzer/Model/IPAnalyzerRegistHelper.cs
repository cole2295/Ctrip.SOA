using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ctrip.SOA.Infratructure.IOCFactory;
using Ctrip.SOA.Infratructure.Utility.IPAnalyzer.Interface;
using Ctrip.SOA.Infratructure.Utility.IPAnalyzer.Model.IPAnalyzerModel;
using Ctrip.SOA.Infratructure.Utility.IPAnalyzer.Model.IPPatternAnalyzerModel;

namespace Ctrip.SOA.Infratructure.Utility.IPAnalyzer.Model
{
    public enum IPPatternType
    {
        interval = 1,
        normal = 10000
    }

    public static class IPAnalyzerRegistHelper
    {
        private static bool isRegist = false;

        private static object lockObj = new object();

        public static void RegistInst()
        {
            if (isRegist == false)
            {
                lock (lockObj)
                {
                    isRegist = true;
                    Regist();
                }
            }
        }

        private static void Regist()
        {
            var factory = Factory.GetInst();
            factory.Regist<IIPAnalyzer, NormalIPAnalyzer>(IOCFactoryModel.InstType.Singleton);

            factory.Regist<IIPPatternAnalyzer, NormalPatternAnalyzer>(IPPatternType.normal.ToString(), IOCFactoryModel.InstType.Singleton);
            factory.Regist<IIPPatternAnalyzer, IntervalPatternAnalyzer>(IPPatternType.interval.ToString(), IOCFactoryModel.InstType.Singleton);
        }

        internal static List<IIPPatternAnalyzer> GetPatternAnalyzer()
        {
            var factory = Factory.GetInst();

            var returnValue = new List<IIPPatternAnalyzer>();
            foreach (var objE in Enum.GetValues(typeof(IPPatternType)))
            {
                returnValue.Add(factory.Get<IIPPatternAnalyzer>(objE.ToString()));
            }
            return returnValue;
        }
    }
}
