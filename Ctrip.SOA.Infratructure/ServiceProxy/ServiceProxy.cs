using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;

namespace Ctrip.SOA.Infratructure.ServiceProxy
{
    internal class ServiceProxy<TChannel> : RealProxy where TChannel : class
    {
        public ServiceProxy()
            : base(typeof(TChannel))
        {
        }

        public override IMessage Invoke(IMessage msg)
        {
            IMethodCallMessage mcm = msg as IMethodCallMessage;
            object[] objArray = new object[mcm.ArgCount];
            mcm.Args.CopyTo((Array)objArray, 0);
            IMethodReturnMessage methodReturnMessage = (IMethodReturnMessage)null;
            TChannel channel = ChannelProxy<TChannel>.Instance.GetChannel();
            try
            {
                methodReturnMessage = (IMethodReturnMessage)new ReturnMessage(mcm.MethodBase.Invoke((object)channel, objArray), objArray, mcm.ArgCount, mcm.LogicalCallContext, mcm);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is CommunicationException || ex.InnerException is TimeoutException)
                    ((object)channel as ICommunicationObject).Abort();
                methodReturnMessage = ex.InnerException == null ? (IMethodReturnMessage)new ReturnMessage(ex, mcm) : (IMethodReturnMessage)new ReturnMessage(ex.InnerException, mcm);
            }
            finally
            {
                ChannelProxy<TChannel>.Instance.PutChannel(channel);
            }
            return (IMessage)methodReturnMessage;
        }
    }
}
