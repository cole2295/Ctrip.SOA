using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.ObjectBuilder2;

namespace Ctrip.SOA.Infratructure.Unity.ContainerExtensions
{
    /// <summary>
    /// 实例工厂。 
    /// </summary>
    /// <author>张智</author>
    public interface IInstanceFactory
    {
        /// <summary>
        /// 创建服务类型的实例。 
        /// </summary>
        /// <param name="type">服务类型。</param>
        /// <returns>实例创建结果。</returns>
        InstanceResult Create(Type type);
    }


    /// <summary>
    /// 实例工厂基类。 
    /// </summary>
    public abstract class InstanceFactoryBase : IInstanceFactory
    {
        #region Constructor

        public InstanceResult Create(Type type)
        {
            if (type == null) { throw new ArgumentNullException("type"); }
            if (CanCreate(type)) { return CreateInstance(type); }
            //return CreateInstance(type); 
            return InstanceResult.Empty;
        }

        #endregion

        #region Methods

        protected abstract InstanceResult CreateInstance(Type type);
        protected abstract bool CanCreate(Type type);

        #endregion
    }


    /// <summary>
    /// 实例创建结果。
    /// </summary>
    public struct InstanceResult
    {
        #region Fields

        /// <summary>
        /// 空结果实例。
        /// </summary>
        public static readonly InstanceResult Empty = new InstanceResult();
        readonly object _instance;
        readonly ILifetimePolicy _lifetimePolicy;

        #endregion

        #region Constructor

        public InstanceResult(object instance, ILifetimePolicy lifetimePolicy)
        {
            _instance = instance;
            _lifetimePolicy = lifetimePolicy;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 服务实例。
        /// </summary>
        public object Instance { get { return _instance; } }
        /// <summary>
        /// 生命周期管理策略。
        /// </summary>
        public ILifetimePolicy LifetimePolicy { get { return _lifetimePolicy; } }

        #endregion
    }

}