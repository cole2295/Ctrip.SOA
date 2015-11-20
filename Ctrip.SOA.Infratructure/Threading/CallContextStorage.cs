using System.Runtime.Remoting.Messaging;

namespace Ctrip.SOA.Infratructure.Threading
{
    /// <summary>
    /// ”√<see cref="CallContext"/> µœ÷<see cref="IThreadStorage"/>°£
    /// </summary>
    public class CallContextStorage : ThreadStorageBase
    {
        public override object GetData(string name)
        {
            return CallContext.LogicalGetData(name);
        }

        public override void SetData(string name, object value)
        {
            CallContext.LogicalSetData(name, value);
        }

        public override void RemoveData(string name)
        {
            CallContext.FreeNamedDataSlot(name);
        }
    }
}