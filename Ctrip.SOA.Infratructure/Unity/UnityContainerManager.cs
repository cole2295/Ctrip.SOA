using System.Collections.Generic;
using Ctrip.SOA.Infratructure.ServiceProxy;
using Ctrip.SOA.Infratructure.Unity.ContainerExtensions;
using Ctrip.SOA.Infratructure.Utility;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel.Unity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity.Configuration;

namespace Ctrip.SOA.Infratructure.Unity
{
    public class UnityContainerManager
    {
        #region singleton method

        private static UnityContainerManager inst;

        private static Dictionary<string, IUnityContainer> dic;

        private UnityContainerManager()
        {
            dic = new Dictionary<string, IUnityContainer>();
        }

        public static UnityContainerManager GetInst()
        {
            if (inst == null)
            {
                inst = new UnityContainerManager();
            }
            return inst;
        }

        #endregion singleton method

        public IUnityContainer GetContainer(string containerName)
        {
            try
            {
                return dic[containerName];
            }
            catch (KeyNotFoundException)
            {
                IUnityContainer untityContainer = new UnityContainer();
                UnityConfigurationSection section = (UnityConfigurationSection)System.Configuration.ConfigurationManager.GetSection("unity");
                section.Configure(untityContainer, containerName);
                //untityContainer.AddNewExtension<InstanceFactoryContainerExtension>();

                dic[containerName] = untityContainer;

                return untityContainer;
            }
        }

        public IUnityContainer GetCurrent()
        {
            string name = AppSetting.ContainerName;
            return GetContainer(name);
        }

        public static IUnityContainer Current
        {
            get
            {
                return UnityContainerManager.GetInst().GetCurrent();
            }
        }

    
    }
}