using System;

namespace Ctrip.SOA.Infratructure.Entity
{
	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class DataMappingException : Exception
	{
		public DataMappingException()
			: base()
		{
		}

		public DataMappingException(string message) : base(message)
		{
		}

		public DataMappingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
