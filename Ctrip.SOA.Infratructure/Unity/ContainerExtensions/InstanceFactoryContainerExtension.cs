using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;
using Microsoft.Practices.ObjectBuilder2;
using System.Collections.Concurrent;
using System.ServiceModel;

namespace Ctrip.SOA.Infratructure.Unity.ContainerExtensions
{

    /// <summary>
    /// Unity实例工厂扩展。
    /// </summary>
    /// <remarks>
    /// <see cref="InstanceFactoryContainerExtension"/>通过创建Unity扩展模块
    /// 为Unity增加生成策略并拦截Unity的实例创建过程提供外部工厂创建实例的机会。

    public class InstanceFactoryContainerExtension : UnityContainerExtension
    {

        #region Constructor

        public InstanceFactoryContainerExtension(IInstanceFactory instanceFactory)
        {
            if (instanceFactory == null) { throw new ArgumentNullException("instanceFactory"); }
            InstanceFactory = instanceFactory;

        }

        #endregion

        #region Override

        protected override void Initialize()
        {
            Context.Strategies.Add(
                new InstanceFactoryBuilderStrategy(InstanceFactory), UnityBuildStage.PreCreation);
            //Context.Strategies.AddNew<InstanceFactoryBuilderStrategy>(UnityBuildStage.PreCreation);
        }

        #endregion

        #region Properties

        /// <summary>
        /// 实例工厂。
        /// </summary>
        public IInstanceFactory InstanceFactory { get; private set; }

        #endregion
    }
}