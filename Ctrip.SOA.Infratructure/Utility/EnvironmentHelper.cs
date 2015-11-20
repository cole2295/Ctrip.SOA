using System;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading;

namespace Ctrip.SOA.Infratructure.Utility
{
    public class EnvironmentHelper
    {
        private static string _machineName = string.Empty;
        private static string _machineFullName = string.Empty;

        /// <summary>
        /// 获取此本地计算机的 NetBIOS 名称。 
        /// </summary>
        public static string MachineName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_machineName))
                {
                    try
                    {
                        _machineName = Environment.MachineName;
                    }
                    catch (Exception e)
                    {
                        _machineName = string.Format("不能读取机器名：{0}", e.Message);
                    }
                }

                return _machineName;
            }
        }

        /// <summary>
        /// 获取此本地计算机的 NetBIOS 全名称。
        /// </summary>
        public static string MachineFullName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_machineFullName))
                {
                    try
                    {
                        _machineFullName = Dns.GetHostEntry("LocalHost").HostName;
                    }
                    catch (Exception ex)
                    {
                        _machineFullName = string.Format("不能读取机器全名：{0}", ex.Message);
                    }
                }

                return _machineFullName;
            }
        }

        /// <summary>
        /// 获取些本地计算机所在域的名称。
        /// </summary>
        public static string DomainName
        {
            get
            {
                string domainName = string.Empty;
                string hostName = MachineFullName;
                if (hostName.StartsWith(MachineName, StringComparison.InvariantCultureIgnoreCase))
                {
                    domainName = hostName.Substring(MachineName.Length + 1);
                }

                return domainName;
            }
        }

        /// <summary>
        /// 获取当前计算机上的处理器数。 
        /// </summary>
        public static string ProcessorCount
        {
            get
            {
                string processorCount = string.Empty;
                try
                {
                    processorCount = Environment.ProcessorCount.ToString();
                }
                catch (Exception ex)
                {
                    processorCount = string.Format("获取机器CPU核数出错:{0}。 ", ex.Message);
                }

                return processorCount;
            }
        }

        /// <summary>
        /// 应用程序域名。
        /// </summary>
        public static string AppDomainName
        {
            get
            {
                string appDomainName = string.Empty;
                try
                {
                    appDomainName = AppDomain.CurrentDomain.FriendlyName;
                }
                catch (Exception ex)
                {
                    appDomainName = string.Format("读取应用程序域名错误：{0}。", ex.Message);
                }

                return appDomainName;
            }
        }

        /// <summary>
        /// 获取启动当前线程的人的域用户名。 
        /// </summary>
        public static string DomainUserName
        {
            get
            {
                string domainUserName = string.Empty;
                try
                {
                    if (string.IsNullOrEmpty(domainUserName))
                    {
                        domainUserName = string.Format("{0}/{1}", Environment.UserDomainName, Environment.UserName);
                    }
                }
                catch (Exception ex)
                {
                    domainUserName = string.Format("获取域用户名错误：{0}。", ex.Message);
                }

                return domainUserName;
            }
        }

        #region [ Process Info ]

        /// <summary>
        /// 进程代号。
        /// </summary>
        public static string ProcessId
        {
            get
            {
                string processId = string.Empty;
                try
                {
                    processId = NativeMethods.GetCurrentProcessId().ToString(NumberFormatInfo.InvariantInfo);
                }
                catch (Exception ex)
                {
                    processId = String.Format("获取进程代号错误：{0}。", ex.Message);
                }

                return processId;
            }
        }

        /// <summary>
        /// 进程名称。
        /// </summary>
        public static string ProcessName
        {
            get
            {
                string processName = string.Empty;
                try
                {
                    StringBuilder buffer = new StringBuilder(1024);
                    int length = NativeMethods.GetModuleFileName(NativeMethods.GetModuleHandle(null), buffer, buffer.Capacity);
                    processName = buffer.ToString();
                }
                catch (Exception ex)
                {
                    processName = string.Format("获取进程名出错:{0}。", ex.Message);
                }

                return processName;
            }
        }

        #endregion

        #region [ Thread Info ]

        /// <summary>
        /// 线程代码。
        /// </summary>
        public static string ThreadId
        {
            get 
            {
                string threadId = string.Empty;
                try
                {
                    threadId = NativeMethods.GetCurrentThreadId().ToString(NumberFormatInfo.InvariantInfo);
                }
                catch(Exception ex)
                {
                    threadId = string.Format("获取线程代号出错：{0}。", ex.Message);
                }

                return threadId;
            }
        }

        /// <summary>
        /// 线程名称。
        /// </summary>
        public static string ThreadName
        {
            get { return Thread.CurrentThread.Name; }
        }

        #endregion
    }
}