using Ctrip.SOA.Infratructure.ServiceProxy;
using Ctrip.SOA.Infratructure.Unity;
using Ctrip.SOA.Infratructure.Unity.ContainerExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Unity.Wcf;
using Microsoft.Practices.Unity;

namespace Ctrip.SOA.Infratructure.Wcf
{
    public class WcfServiceHostFactory: ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new UnityServiceHost(UnityContainerManager.Current, serviceType, baseAddresses);
        }

        public static T GetService<T>()
        {
            return UnityContainerManager.Current.Resolve<T>();
        }
    }
}
