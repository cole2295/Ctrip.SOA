using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ctrip.SOA.Infratructure.Utility.IPAnalyzer.Interface;
using System.Text.RegularExpressions;

namespace Ctrip.SOA.Infratructure.Utility.IPAnalyzer.Model.IPPatternAnalyzerModel
{
    public class IntervalPatternAnalyzer : IIPPatternAnalyzer
    {
        private const string strReg = @"((?:(?:25[0-5]|2[0-4]\d|[01]?\d?\d)\.){3}(?:25[0-5]|2[0-4]\d|[01]?\d?\d))~((?:(?:25[0-5]|2[0-4]\d|[01]?\d?\d)\.){3}(?:25[0-5]|2[0-4]\d|[01]?\d?\d))";

        public bool isFitPattern(string ipPattern)
        {
            return Regex.IsMatch(ipPattern, strReg);
        }

        public bool Analyze(string clientIp, string ipPattern)
        {
            try
            {
                var matches = Regex.Match(ipPattern, strReg);
                var strStartIp = matches.Groups[1].Value;
                var strEndIp = matches.Groups[2].Value;
                List<int> strIps = strStartIp.Split('.').ToList().ConvertAll(p => int.Parse(p));
                List<int> endIps = strEndIp.Split('.').ToList().ConvertAll(p => int.Parse(p));
                List<int> currentIps = clientIp.Split('.').ToList().ConvertAll(p => int.Parse(p));

                int resultCount = 0;
                for (var i = 0; i < 4; i++)
                {
                    if (currentIps[i] >= strIps[i] && currentIps[i] <= endIps[i])
                    {
                        resultCount++;
                    }
                }

                return resultCount == 4;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
