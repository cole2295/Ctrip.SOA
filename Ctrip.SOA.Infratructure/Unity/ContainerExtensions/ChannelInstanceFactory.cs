using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
using Microsoft.Practices.Unity;
using System.ServiceModel;

namespace Ctrip.SOA.Infratructure.Unity.ContainerExtensions
{

    /// <summary>
    /// Wcf自动客户端通道实例工厂。
    /// </summary>
    public class ChannelInstanceFactory : InstanceFactoryBase
    {
        #region Fields

        private Func<Type, object> _creator;
        private ConcurrentDictionary<Type, bool> _contractTypeCache = new ConcurrentDictionary<Type, bool>();
        private IUnityContainer _container;

        #endregion

        #region Constructor

        public ChannelInstanceFactory(Func<Type, object> creator, IUnityContainer container)
        {
            if (creator == null) { throw new ArgumentNullException("creator"); }
            if (container == null) { throw new ArgumentNullException("container"); }

            _creator = creator;
            _container = container;
        }

        #endregion

        #region Override

        protected override InstanceResult CreateInstance(Type type)
        {
            return new InstanceResult(_creator(type),
                new ContainerControlledLifetimeManager());
        }

        protected override bool CanCreate(Type type)
        {
            // type必须是接口类型的Wcf服务契约，并且不能在Unity容器中显式注册过。
            // 缓存用于优化Unity注册表线性扫描和Attribute查找。
            return type.IsInterface && _contractTypeCache.GetOrAdd(type, (t) =>
            {
                return (!_container.IsRegistered(t) // 线性扫描Unity注册表
                    && t.IsDefined(typeof(ServiceContractAttribute), false));
              
                //var flag = !_container.IsRegistered(t);
                //var flag2 = t.IsDefined(typeof(ServiceContractAttribute), false);
                //return flag && flag2;
            });
        }

        #endregion
    }
}