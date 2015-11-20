using System.Collections;

namespace Ctrip.SOA.Infratructure.Collections
{
    public interface IKeyValueCollection : IList
    {
        ICollection Keys { get; }
        ICollection Values { get; }

        object GetValueByKey(object key);
    }
}
