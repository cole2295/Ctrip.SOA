using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;

namespace Ctrip.SOA.Infratructure.ServiceProxy
{
    internal class ChannelProxy<TChannel> where TChannel : class
    {
        public static readonly ChannelProxy<TChannel> Instance = new ChannelProxy<TChannel>();
        protected string EndPointConfigurationName = typeof(TChannel).Name;
        private ChannelFactory<TChannel> factory;

        protected ChannelFactory<TChannel> Factory
        {
            get
            {
                if (this.factory != null && this.factory.State == CommunicationState.Faulted)
                {
                    this.factory.Abort();
                    this.factory = (ChannelFactory<TChannel>)null;
                }
                if (this.factory != null && (this.factory.State == CommunicationState.Closed || this.factory.State == CommunicationState.Closing))
                    this.factory = (ChannelFactory<TChannel>)null;
                if (this.factory == null)
                {
                    var tempFactory = new ChannelFactory<TChannel>(this.EndPointConfigurationName);
                    tempFactory.Endpoint.Behaviors.Add((IEndpointBehavior) new ContextPropagationBehaviorAttribute());
                    Interlocked.CompareExchange(ref this.factory, tempFactory, null);
                }
                return this.factory;
            }
        }

        static ChannelProxy()
        {
        }

        public TChannel GetChannel()
        {
            return this.Factory.CreateChannel();
        }

        public void PutChannel(TChannel channel)
        {
            ICommunicationObject communicationObject = (object)channel as ICommunicationObject;
            switch (communicationObject.State)
            {
                case CommunicationState.Created:
                case CommunicationState.Opening:
                case CommunicationState.Opened:
                    communicationObject.Close();
                    break;
                case CommunicationState.Faulted:
                    communicationObject.Abort();
                    break;
            }
        }
    }
}
