using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrip.SOA.Infratructure.Logging
{
    public class LogHelper
    {
        /// <summary>
        /// 应用层的日志,用于输出逻辑上的信息或错误信息
        /// </summary>
        private static log4net.ILog logger = log4net.LogManager.GetLogger("Logger");

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="classInfo">方法信息</param>
        /// <param name="message">信息</param>
        public static void WriteLog(string classInfo, string message)
        {
            try
            {
                if (logger.IsInfoEnabled)
                {
                    logger.Info(classInfo + "\t" + message);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="classInfo">方法信息</param>
        /// <param name="message">信息</param>
        public static void WriteWarnLog(string classInfo, string message)
        {
            try
            {
                if (logger.IsWarnEnabled)
                {
                    logger.Warn(classInfo + "\t" + message);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 写错误日志日志
        /// </summary>
        /// <param name="classInfo">方法信息</param>
        /// <param name="ex">错误信息</param>
        public static void WriteError(string classInfo, string message, Exception ex)
        {
            try
            {
                if (logger.IsErrorEnabled)
                {
                    logger.Error(classInfo + "\n" + message + "\n" + ex.ToString());
                }
            }
            catch
            {

            }
        }

      
    }
}
