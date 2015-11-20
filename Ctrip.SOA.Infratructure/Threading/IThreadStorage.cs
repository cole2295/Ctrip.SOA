namespace Ctrip.SOA.Infratructure.Threading
{	
	/// <summary>
	/// 表示线程数据存储器。
	/// </summary>
	public interface IThreadStorage
	{        
        T GetData<T>(string name, T defaultValue=default(T));

		object GetData(string name);		
		void SetData(string name, object value);
		void RemoveData(string name);
	}
}
