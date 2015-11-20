using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Utility.IPAnalyzer.Interface
{
    public interface IIPPatternAnalyzer
    {
        bool isFitPattern(string ipPattern);

        bool Analyze(string clientIp, string ipPattern);
    }
}
