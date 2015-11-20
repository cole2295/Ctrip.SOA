using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel;

namespace Ctrip.SOA.Infratructure.ServiceProxy
{
    public class ServiceRealProxy<T> : RealProxy
    {
        private string _endpointName;

        public ServiceRealProxy(string endpointName)
            : base(typeof(T))
        {
            if (string.IsNullOrEmpty(endpointName))
                throw new ArgumentNullException("endpointName");
            this._endpointName = endpointName;
        }

        public override IMessage Invoke(IMessage msg)
        {
            T channel = ChannelFactoryCreator.Create<T>(this._endpointName).CreateChannel();
            IMethodCallMessage mcm = (IMethodCallMessage)msg;
            object[] objArray = Array.CreateInstance(typeof(object), mcm.Args.Length) as object[];
            mcm.Args.CopyTo((Array)objArray, 0);
            IMethodReturnMessage methodReturnMessage;
            try
            {
                methodReturnMessage = (IMethodReturnMessage)new ReturnMessage(mcm.MethodBase.Invoke((object)channel, objArray), objArray, objArray.Length, mcm.LogicalCallContext, mcm);
                ((object)channel as ICommunicationObject).Close();
            }
            catch (CommunicationException ex)
            {
                ((object)channel as ICommunicationObject).Abort();
                methodReturnMessage = (IMethodReturnMessage)new ReturnMessage((Exception)ex, mcm);
            }
            catch (TimeoutException ex)
            {
                ((object)channel as ICommunicationObject).Abort();
                methodReturnMessage = (IMethodReturnMessage)new ReturnMessage((Exception)ex, mcm);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is CommunicationException || ex.InnerException is TimeoutException)
                    ((object)channel as ICommunicationObject).Abort();
                methodReturnMessage = ex.InnerException == null ? (IMethodReturnMessage)new ReturnMessage(ex, mcm) : (IMethodReturnMessage)new ReturnMessage(ex.InnerException, mcm);
            }
            return (IMessage)methodReturnMessage;
        }
    }
}
