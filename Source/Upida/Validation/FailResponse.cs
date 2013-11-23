using System;
using System.Collections.Generic;

namespace Upida.Validation
{
	/// <summary>
	/// Represents result of validation
	/// </summary>
	public class FailResponse
	{
		private string main;
		private IList<Failure> failures;

		public FailResponse()
		{
			this.failures = new List<Failure>();
		}

		public FailResponse(String main)
			: this()
		{
			this.main = main;
		}

		/// <summary>
		/// Default message
		/// </summary>
		public string Main
		{
			get { return this.main; }
		}

		/// <summary>
		/// List of failures
		/// </summary>
		public IList<Failure> Failures
		{
			get { return this.failures; }
			set { this.failures = value; }
		}
	}
}