using System;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Ctrip.SOA.Infratructure.Unity.ContainerExtensions
{
    /// <summary>
    /// 实例工厂生成策略。
    /// </summary>

    public class InstanceFactoryBuilderStrategy : BuilderStrategy
    {
        #region Constructor

        //public static IUnityContainer UnityContainer { get; private set; }
        //public static InjectionMember InterceptionMember { get; private set; }

        static InstanceFactoryBuilderStrategy()
        {
            //IUnityContainer container = UnityContainerManager.Current;
            //UnityContainerConfigurator configurator = new UnityContainerConfigurator(container);
            //EnterpriseLibraryContainer.ConfigureContainer(configurator, ConfigurationSourceFactory.Create());
            //UnityContainer = UnityContainerManager.Current;
            //InterceptionMember = new InstanceInterceptionPolicySettingInjectionMember(new TransparentProxyInterceptor());
        }

        public InstanceFactoryBuilderStrategy(IInstanceFactory instanceFactory)
        {
            if (instanceFactory == null) { throw new ArgumentNullException("instanceFactory"); }
            InstanceFactory = instanceFactory;
        }

        #endregion Constructor

        #region Override

        public override void PreBuildUp(IBuilderContext context)
        {
            var instanceResult = InstanceFactory.Create(context.OriginalBuildKey.Type);
            if (instanceResult.Instance != null)
            {
                SetContext(context, instanceResult);
            }

            //if (null == context.Existing ||
            // context.Existing.GetType().FullName.StartsWith("Microsoft.Practices") ||
            // context.Existing is IInterceptingProxy)
            //{
            //    return;
            //}
            //var type = context.OriginalBuildKey.Type;
            //if (type != null && UnityContainer.IsRegistered(type))
            //{
            //    //context.Existing = UnityContainer.Configure<TransientPolicyBuildUpExtension>()
            //    //    .BuildUp(context.OriginalBuildKey.Type, context.Existing, null, InterceptionMember);
            //    //UnityContainer.AddNewExtension<Interception>();
            //    //UnityContainer.Configure<Interception>().SetInterceptorFor(type, new InterfaceInterceptor());
            //    //var obj = UnityContainer.Resolve(type, null);
            //    //context.Existing = UnityContainer.Configure<TransientPolicyBuildUpExtension>().BuildUp(type, obj, null, InterceptionMember);
            //}

            //context.BuildComplete = true;
                //UnityContainer.Configure<TransientPolicyBuildUpExtension>().BuildUp
                //(context.OriginalBuildKey.Type, context.Existing, null, InterceptionMember);
        }

        #endregion Override

        #region Methods

        private static void SetContext(IBuilderContext context, InstanceResult instanceResult)
        {
            //context.Existing = UnityContainer.Configure<TransientPolicyBuildUpExtension>().BuildUp (context.OriginalBuildKey.Type, context.Existing, null, InterceptionMember);
            context.Existing = instanceResult.Instance;

            if (instanceResult.LifetimePolicy != null)
            {
                context.PersistentPolicies.Set(typeof(ILifetimePolicy), instanceResult.LifetimePolicy, context.BuildKey);
            }
            context.BuildComplete = true;
        }

        #endregion Methods

        #region Properties

        /// <summary>
        /// Unity容器实例。
        /// </summary>
        public IInstanceFactory InstanceFactory { get; private set; }

        #endregion Properties
    }
}