using System;
using System.Collections;

namespace Ctrip.SOA.Infratructure.Threading
{
    /// <summary>
    /// ”√<see cref="ThreadStaticAttribute"/> µœ÷<see cref="IThreadStorage"/>°£
    /// </summary>
    public class ThreadStaticStorage : ThreadStorageBase
    {
        [ThreadStatic]
        private static Hashtable _data = new Hashtable();

        public override object GetData(string name)
        {
            return _data[name];
        }

        public override void SetData(string name, object value)
        {
            _data[name] = value;
        }

        public override void RemoveData(string name)
        {
            _data.Remove(name);
        }
    }
}