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
        /// ��ȡ�˱��ؼ������ NetBIOS ���ơ� 
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
                        _machineName = string.Format("���ܶ�ȡ��������{0}", e.Message);
                    }
                }

                return _machineName;
            }
        }

        /// <summary>
        /// ��ȡ�˱��ؼ������ NetBIOS ȫ���ơ�
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
                        _machineFullName = string.Format("���ܶ�ȡ����ȫ����{0}", ex.Message);
                    }
                }

                return _machineFullName;
            }
        }

        /// <summary>
        /// ��ȡЩ���ؼ��������������ơ�
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
        /// ��ȡ��ǰ������ϵĴ��������� 
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
                    processorCount = string.Format("��ȡ����CPU��������:{0}�� ", ex.Message);
                }

                return processorCount;
            }
        }

        /// <summary>
        /// Ӧ�ó���������
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
                    appDomainName = string.Format("��ȡӦ�ó�����������{0}��", ex.Message);
                }

                return appDomainName;
            }
        }

        /// <summary>
        /// ��ȡ������ǰ�̵߳��˵����û����� 
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
                    domainUserName = string.Format("��ȡ���û�������{0}��", ex.Message);
                }

                return domainUserName;
            }
        }

        #region [ Process Info ]

        /// <summary>
        /// ���̴��š�
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
                    processId = String.Format("��ȡ���̴��Ŵ���{0}��", ex.Message);
                }

                return processId;
            }
        }

        /// <summary>
        /// �������ơ�
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
                    processName = string.Format("��ȡ����������:{0}��", ex.Message);
                }

                return processName;
            }
        }

        #endregion

        #region [ Thread Info ]

        /// <summary>
        /// �̴߳��롣
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
                    threadId = string.Format("��ȡ�̴߳��ų���{0}��", ex.Message);
                }

                return threadId;
            }
        }

        /// <summary>
        /// �߳����ơ�
        /// </summary>
        public static string ThreadName
        {
            get { return Thread.CurrentThread.Name; }
        }

        #endregion
    }
}