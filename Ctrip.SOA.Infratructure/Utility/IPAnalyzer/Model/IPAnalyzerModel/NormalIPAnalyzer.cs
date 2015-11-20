using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ctrip.SOA.Infratructure.Utility.IPAnalyzer.Interface;

namespace Ctrip.SOA.Infratructure.Utility.IPAnalyzer.Model.IPAnalyzerModel
{
    public class NormalIPAnalyzer : IIPAnalyzer
    {
        public bool DoAnalyze(string clientIP, string ipPatterns)
        {
            var list = IPAnalyzerRegistHelper.GetPatternAnalyzer();
            foreach (var pattern in StringHelper.SplitString(ipPatterns, "|"))
            {
                foreach (var check in list)
                {
                    if (check.isFitPattern(pattern))
                    {
                        if (check.Analyze(clientIP, pattern))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
