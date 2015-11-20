using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Web;
using System.Xml;
using System;
using System.Runtime.Serialization;

namespace Ctrip.SOA.Infratructure.ServiceProxy
{
    [Serializable]
    public class ApplicationContext : Dictionary<string, object>
    {
        public const string ContextKey = "Ctrip.SOA.Infratructure.ServiceProxy.ApplicationContext";
        public const string ContextHeaderLocalName = "ApplicationContext";
        public const string ContextHeaderNamespace = "http://schemas.microsoft.com/ws/2005/05/addressing/none";

        public ApplicationContext() { }
        public ApplicationContext(SerializationInfo info, StreamingContext ctx)
            : base(info, ctx)
        { }

        public static ApplicationContext Current
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session[ContextKey] == null)
                        HttpContext.Current.Session[ContextKey] = (object)new ApplicationContext();
                    return HttpContext.Current.Session[ContextKey] as ApplicationContext;
                }
                else
                {
                    if (CallContext.GetData(ContextKey) == null)
                        CallContext.SetData(ContextKey, (object)new ApplicationContext());
                    return CallContext.GetData(ContextKey) as ApplicationContext;
                }
            }
            set
            {
                if (HttpContext.Current != null)
                    HttpContext.Current.Session[ContextKey] = (object)value;
                else
                    CallContext.SetData(ContextKey, (object)value);
            }
        }

        public string UserName
        {
            get
            {
                if (!this.ContainsKey("__UserName"))
                {
                    if (HttpContext.Current == null || !HttpContext.Current.User.Identity.IsAuthenticated)
                        return string.Empty;
                    this["__UserName"] = (object)HttpContext.Current.User.Identity.Name;
                }
                return (string)this["__UserName"];
            }
            set
            {
                this["__UserName"] = (object)value;
            }
        }
    }
}
