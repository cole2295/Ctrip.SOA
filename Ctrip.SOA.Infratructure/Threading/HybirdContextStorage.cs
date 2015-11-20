using System.Web;
using System.Runtime.Remoting.Messaging;

namespace Ctrip.SOA.Infratructure.Threading
{
    public class HybirdContextStorage : ThreadStorageBase
    {
        public override object GetData(string name)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx != null)
            {
                return ctx.Items[name];
            }
            else
            {
                return CallContext.LogicalGetData(name);
            }
        }

        public override void SetData(string name, object value)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx != null)
            {
                ctx.Items[name] = value;
            }
            else
            {
                CallContext.LogicalSetData(name, value);
            }
        }

        public override void RemoveData(string name)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx != null)
            {
                ctx.Items.Remove(name);
            }
            else
            {
                CallContext.FreeNamedDataSlot(name);
            }
        }
    }
}