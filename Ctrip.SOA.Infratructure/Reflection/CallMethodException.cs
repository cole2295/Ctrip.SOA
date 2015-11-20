using System;

namespace Ctrip.SOA.Infratructure.Reflection
{
	/// <summary>
	/// This exception is returned from the CallMethod method.
	/// </summary>
	[Serializable()]
	public partial class CallMethodException : Exception
	{
		private string _innerStackTrace;

		/// <summary>
		/// Get the stack trace from the original exception.
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public override string StackTrace
		{
			get
			{
				return string.Format("{0}{1}{2}",
				  _innerStackTrace, Environment.NewLine, base.StackTrace);
			}
		}

		/// <summary>
		/// Creates an instance of the object.
		/// </summary>
		/// <param name="message">Message text describing the exception.</param>
		/// <param name="ex">Inner exception object.</param>
		public CallMethodException(string message, Exception ex)
			: base(message, ex)
		{
			_innerStackTrace = ex.StackTrace;
		}
	}
}