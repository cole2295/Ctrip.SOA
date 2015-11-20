using System;
using System.Web;
using Ctrip.SOA.Bussiness.User.Service;
using Ctrip.SOA.Infratructure.Logging;
using Ctrip.SOA.Infratructure.ServiceProxy;
using Ctrip.SOA.Infratructure.Unity;
using Ctrip.SOA.Infratructure.Unity.ContainerExtensions;
using Ctrip.SOA.Infratructure.Utility;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Ctrip.SOA.Service.User
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~\\log4net.xml")));
            InitUnity();
        }

        public void InitUnity()
        {
            // _unityContainer = UnityContainerManager.Current;
         
            string name = AppSetting.ContainerName;
            var container = UnityContainerManager.GetInst().GetContainer(name);
            UnityContainerManager.Current.AddExtension(
           new InstanceFactoryContainerExtension(
               new ChannelInstanceFactory(ServiceProxyFactory.CreateChannel, UnityContainerManager.Current)));
            container.AddNewExtension<Interception>();
            container.Configure<Interception>().SetInterceptorFor(typeof(IUserService), new InterfaceInterceptor());
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            var httpCode = (ex is HttpException) ? (ex as HttpException).GetHttpCode() : 500;
            if (httpCode != 404)
            {
                //Ctrip.SOA.Infratructure.Logging.HHLogHelperV2.ERRORGlobalException(ex);
                LogHelper.WriteError("Application_Error", ex.Message, ex);
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}