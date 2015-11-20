using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;
using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.ServiceProxy
{
    internal class ContextSendInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (string.IsNullOrEmpty(ApplicationContext.Current.UserName) && HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated)
                ApplicationContext.Current.UserName = HttpContext.Current.User.Identity.Name;

            //request.Headers.Add(new MessageHeader<string>(AppSetting.ProductPatternIDs).GetUntypedHeader("ProductPatternIDs", ""));

            request.Headers.Add(new MessageHeader<string>(AppSetting.AppID.ToString()).GetUntypedHeader("AppID", ""));

            /*HttpRequestMessageProperty hrmp = new HttpRequestMessageProperty();
            hrmp.Headers.Add(ApplicationContext.ContextHeaderLocalName, ApplicationContext.ContextKey);
            //Set hrmp.Headers, then:
            request.Properties.Add(ApplicationContext.ContextHeaderNamespace, hrmp);*/
            //request.Headers.Add(new MessageHeader<ApplicationContext>().GetUntypedHeader("ApplicationContext", "http://schemas.microsoft.com/ws/2005/05/addressing/none"));
            //request.Headers.Add(MessageHeader.CreateHeader(ApplicationContext.ContextKey, ApplicationContext.ContextHeaderNamespace, ApplicationContext.ContextHeaderLocalName));
            //request.Headers.Add(new MessageHeader<ApplicationContext>(ApplicationContext.Current).GetUntypedHeader("ApplicationContext", "http://schemas.microsoft.com/ws/2005/05/addressing/none"));
            return (object)null;
        }
    }
}
