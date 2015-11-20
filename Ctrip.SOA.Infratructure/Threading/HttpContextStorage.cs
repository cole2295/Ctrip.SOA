using System.Web;

namespace Ctrip.SOA.Infratructure.Threading
{
	/// <summary>
    ///  用<see cref="HttpContext"/>实现<see cref="IThreadStorage"/>。
	/// </summary>
	public class HttpContextStorage : ThreadStorageBase
	{
		public override object GetData(string name)
		{
			HttpContext ctx = HttpContext.Current;
			if (ctx != null)
			{
				return ctx.Items[name];
			}

			return null;
		}

		public override void SetData(string name, object value)
		{
			HttpContext ctx = HttpContext.Current;
			if (ctx != null)
			{
				ctx.Items[name] = value;
			}
		}

		public override void RemoveData(string name)
		{
			HttpContext ctx = HttpContext.Current;
			if (ctx != null)
			{
				ctx.Items.Remove(name);
			}
		}
	}
}
