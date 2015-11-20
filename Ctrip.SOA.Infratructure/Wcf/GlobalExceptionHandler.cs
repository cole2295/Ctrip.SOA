using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Ctrip.SOA.Infratructure.Wcf
{
    public class GlobalExceptionHandler : IErrorHandler
    {
        /// <summary>
        /// 测试log4net
        /// </summary>
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(GlobalExceptionHandler));

        #region IErrorHandler Members
        /// <summary>
        /// HandleError
        /// </summary>
        /// <param name="ex">ex</param>
        /// <returns>true</returns>
        public bool HandleError(Exception ex)
        {
            return true;
        }

        /// <summary>
        /// ProvideFault
        /// </summary>
        /// <param name="ex">ex</param>
        /// <param name="version">version</param>
        /// <param name="msg">msg</param>
        public void ProvideFault(Exception ex, MessageVersion version, ref Message msg)
        {
            //// 写入log4net
            //log.Error("WCF异常", ex);
            Logging.LogHelper.WriteError(string.Empty, "WCF异常", ex);
            var newEx = new FaultException(string.Format("WCF接口出错 {0}", ex.TargetSite.Name));
            MessageFault msgFault = newEx.CreateMessageFault();
            msg = Message.CreateMessage(version, msgFault, newEx.Action);
        }
        #endregion

    }
}
