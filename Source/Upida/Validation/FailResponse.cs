﻿using System;
using System.Collections.Generic;

namespace Upida.Validation
{
	/// <summary>
	/// Represents result of validation
	/// </summary>
	public class FailResponse
	{
		private IFailureList failures;

		public FailResponse()
		{
			this.failures = null;
		}

		public FailResponse(IFailureList failures)
		{
			this.failures = failures;
		}

		/// <summary>
		/// Creates an instance of FailureResponse class with one failure in it
		/// </summary>
		/// <param name="message"></param>
		public FailResponse(string message)
		{
			this.failures = new FailureList();
			this.failures.Fail(message);
		}

		/// <summary>
		/// List of failures
		/// </summary>
		public IFailureList Failures
		{
			get { return this.failures; }
			set { this.failures = value; }
		}
	}
}