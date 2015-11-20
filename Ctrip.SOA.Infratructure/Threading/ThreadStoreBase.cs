using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Threading
{
	/// <summary>
	/// 表示线程上下文存储基类。
	/// </summary>
	public abstract class ThreadStorageBase : IThreadStorage
    {
        public T GetData<T>(string name, T defaultValue = default(T))
        {
            object data = GetData(name);
            if (data == null)
            {
                return defaultValue;
            }

            return (T)data;
        }

		public abstract object GetData(string name);
		public abstract void SetData(string name, object value);
		public abstract void RemoveData(string name);
	}
}
