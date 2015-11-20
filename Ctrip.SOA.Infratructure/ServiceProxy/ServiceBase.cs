using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Ctrip.SOA.Infratructure.Logging;

namespace Ctrip.SOA.Infratructure.ServiceProxy
{
    public class ServiceBase
    {
        protected string GetPatternIDs()
        {
            OperationContext context = OperationContext.Current;

            try
            {
                return context.IncomingMessageHeaders.GetHeader<string>("ProductPatternIDs", "");
            }
            catch (MessageHeaderException ex)
            {

                //HHLogHelperV2.ERRORExecption(
                //    string.Format("AppId:{0}, Title:尝试获得PatternIDs失败", GetAppID()), ex);
                LogHelper.WriteError("GetPatternIDs", string.Format("AppId:{0}, Title:尝试获得PatternIDs失败", GetAppID()), ex);
                return "1,2,3,4,5";
            }
        }

        protected string GetAppID()
        {
            OperationContext context = OperationContext.Current;
            try
            {
                return context.IncomingMessageHeaders.GetHeader<string>("AppID", "");
            }
            catch (MessageHeaderException)
            {
            }

            return String.Empty;
        }
    }
}
