using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Ctrip.SOA.Infratructure.Reflection
{
	/// <summary>
	/// This exception is returned from the CallMethod method
	/// </summary>
	public partial class CallMethodException
	{
		private string _innerStackTraceSerializationName = "_innerStackTrace";

		/// <summary>
		/// Creates an instance of the object for deserialization.
		/// </summary>
		/// <param name="info">Serialization info.</param>
		/// <param name="context">Serialiation context.</param>
		protected CallMethodException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			info.GetString(_innerStackTraceSerializationName);
		}

		/// <summary>
		/// Serializes the object.
		/// </summary>
		/// <param name="info">Serialization info.</param>
		/// <param name="context">Serialization context.</param>
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue(_innerStackTraceSerializationName, _innerStackTrace);
		}
	}
}