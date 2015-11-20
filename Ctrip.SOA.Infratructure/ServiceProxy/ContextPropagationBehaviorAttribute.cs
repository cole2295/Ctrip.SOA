using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Ctrip.SOA.Infratructure.ServiceProxy
{
    public class ContextPropagationBehaviorAttribute : Attribute, IServiceBehavior, IEndpointBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in (SynchronizedCollection<ChannelDispatcherBase>)serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
                {
                    foreach (DispatchOperation dispatchOperation in (SynchronizedCollection<DispatchOperation>)endpointDispatcher.DispatchRuntime.Operations)
                        dispatchOperation.CallContextInitializers.Add((ICallContextInitializer)new ContextReceivalCallContextInitializer());
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add((IClientMessageInspector)new ContextSendInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            foreach (DispatchOperation dispatchOperation in (SynchronizedCollection<DispatchOperation>)endpointDispatcher.DispatchRuntime.Operations)
                dispatchOperation.CallContextInitializers.Add((ICallContextInitializer)new ContextReceivalCallContextInitializer());
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
