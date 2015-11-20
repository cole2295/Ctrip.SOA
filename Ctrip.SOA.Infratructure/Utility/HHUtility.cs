using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Utility
{
    public class HHUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="iparray"></param>
        /// <returns></returns>
        public static bool InIPArray(string ip, string[] iparray)
        {
            string[] strArray1 = StringHelper.SplitString(ip, ".");
            for (int index1 = 0; index1 < iparray.Length; ++index1)
            {
                string[] strArray2 = StringHelper.SplitString(iparray[index1], ".");
                int num = 0;
                for (int index2 = 0; index2 < strArray2.Length; ++index2)
                {
                    if (strArray2[index2] == "*")
                        return true;
                    if (strArray1.Length > index2 && strArray2[index2] == strArray1[index2])
                        ++num;
                    else
                        break;
                }
                if (num == 4)
                    return true;
            }
            return false;
        }
    }
}
