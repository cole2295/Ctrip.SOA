using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Utility.IPAnalyzer.Interface
{
    public interface IIPAnalyzer
    {
        bool DoAnalyze(string clientIP, string ipPatterns);
    }
}
