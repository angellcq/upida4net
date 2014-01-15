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
		private FailureList failures;

		public FailResponse()
		{
			this.main = string.Empty;
			this.failures = new FailureList();
		}

		public FailResponse(string main)
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
			set { this.main = value; }
		}

		/// <summary>
		/// List of failures
		/// </summary>
		public FailureList Failures
		{
			get { return this.failures; }
			set { this.failures = value; }
		}
	}
}