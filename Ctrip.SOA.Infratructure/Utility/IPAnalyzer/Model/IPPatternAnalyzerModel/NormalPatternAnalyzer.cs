using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ctrip.SOA.Infratructure.Utility.IPAnalyzer.Interface;
using System.Text.RegularExpressions;

namespace Ctrip.SOA.Infratructure.Utility.IPAnalyzer.Model.IPPatternAnalyzerModel
{
    public class NormalPatternAnalyzer : IIPPatternAnalyzer
    {
        public bool isFitPattern(string ipPattern)
        {
            return true;
        }

        public bool Analyze(string clientIp, string ipPattern)
        {
            return HHUtility.InIPArray(clientIp, new string[] { ipPattern });
        }
    }
}
