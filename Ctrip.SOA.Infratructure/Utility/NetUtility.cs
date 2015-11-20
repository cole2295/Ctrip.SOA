using System;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace Ctrip.SOA.Infratructure.Utility
{
    public class NetUtility
    {
        public static string GetMaskIP(string maskIPPattern, out int maskLen)
        {
            maskLen = 0;
            string[] parts = maskIPPattern.Split(new char[]{ '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
            {
                return parts[0];
            }
            else if (parts.Length == 2)
            {
                int.TryParse(parts[1], out maskLen);
                return GetMaskIP(parts[0], maskLen);
            }
            else
            {
                return maskIPPattern;
            }
        }

        /// <summary>
        /// 获取掩码IP地址
        /// </summary>
        /// <param name="IP">IP地址</param>
        /// <param name="Masklen">子网掩码长度</param>
        /// <returns></returns>
        public static string GetMaskIP(string ip, int masklen)
        {
            string maskIP = string.Empty;
            string[] iptmp = ip.Split('.');
            if (iptmp != null && iptmp.Length == 4)
            {
                uint tIP = 0;
                uint iIP = 0;
                uint iMask = 0xFFFFFFFF;     // 把子网掩码设置为 255.255.255.255
                masklen = 32 - masklen;         // MaskLen表标,bit位值等于1的个数,被32减了之后就变成了,bit位 0 的个数了,
                iMask <<= masklen;              // 把子网掩码向左移 bit位值为0 的位数,这样就可以把以数字表示的子网掩码变成32位数的掩码了
                for (int i = 0; i < 4; i++)  // 把IP从字符串的表示形式改成整型的形式
                {
                    tIP = byte.Parse(iptmp[i]);
                    iIP += (tIP << (3 - i) * 8);
                }

                tIP = iIP & iMask;

                maskIP = ((byte)(tIP >> 24)).ToString();   // 网络ID, IP段
                for (int i = 1; i < 4; i++)
                {
                    maskIP += "." + ((byte)(tIP >> (3 - i) * 8)).ToString();
                }
            }
            else
            {
                maskIP = ip;
            }

            return maskIP;
        }

        public static string GetClientIP()
        {
            string ip = string.Empty;
            try
            {
                if (HttpContext.Current != null)
                {
                    HttpRequest requet = HttpContext.Current.Request;
                    if (requet != null)
                    {
                        if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                        { // 服务器， using proxy
                            ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        }
                        else
                        { // 如果没有使用代理服务器或者得不到客户端的ip not using proxy or can't get the Client IP
                            ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                        }
                    }
                }
            }
            catch
            {
                ip = string.Empty;
            }
            
            // 用于WinForm应用程序获取本机的IP地址作为客户端IP
            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = GetHostIP();
            }

            return ip;
        }

        public static string GetHostIP()
        {
            string ip = string.Empty;
            try
            {
                string strHostName = Dns.GetHostName(); //得到本机的主机名
                IPHostEntry ipEntry = Dns.GetHostEntry(strHostName); //取得本机IP
                ip = GetIpFromHostEntry(ipEntry);
            }
            catch
            {
                ip = string.Empty;
            }

            return ip;
        }

        private static string GetIpFromHostEntry(IPHostEntry ipHostEntry)
        {
            foreach (System.Net.IPAddress ipAddress in ipHostEntry.AddressList)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                    return ipAddress.ToString();
            }

            return (ipHostEntry.AddressList != null && ipHostEntry.AddressList.Length > 0) ?
                ipHostEntry.AddressList[0].ToString() :
                string.Empty;
        }
    }
}