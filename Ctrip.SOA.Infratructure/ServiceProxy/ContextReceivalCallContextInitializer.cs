using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Ctrip.SOA.Infratructure.ServiceProxy
{
    internal class ContextReceivalCallContextInitializer : ICallContextInitializer
    {
        public void AfterInvoke(object correlationState)
        {
            ApplicationContext.Current.Clear();
        }

        public object BeforeInvoke(InstanceContext instanceContext, IClientChannel channel, Message message)
        {
            //ApplicationContext.Current = message.Headers.GetHeader<ApplicationContext>("ApplicationContext", "http://schemas.microsoft.com/ws/2005/05/addressing/none");
            return (object)null;
        }
    }
}
