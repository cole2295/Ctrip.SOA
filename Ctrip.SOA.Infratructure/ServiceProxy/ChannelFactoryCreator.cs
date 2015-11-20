using System;
using System.Collections;
using System.ServiceModel;

namespace Ctrip.SOA.Infratructure.ServiceProxy
{
    internal static class ChannelFactoryCreator
    {
        private static Hashtable channelFactories = new Hashtable();

        static ChannelFactoryCreator()
        {
        }

        public static ChannelFactory<T> Create<T>(string endpointName)
        {
            if (string.IsNullOrEmpty(endpointName))
                throw new ArgumentNullException("endpointName");
            ChannelFactory<T> channelFactory = (ChannelFactory<T>)null;
            if (ChannelFactoryCreator.channelFactories.ContainsKey((object)endpointName))
                channelFactory = ChannelFactoryCreator.channelFactories[(object)endpointName] as ChannelFactory<T>;
            if (channelFactory == null)
            {
                channelFactory = new ChannelFactory<T>(endpointName);
                lock (ChannelFactoryCreator.channelFactories.SyncRoot)
                    ChannelFactoryCreator.channelFactories[(object)endpointName] = (object)channelFactory;
            }
            return channelFactory;
        }
    }
}

