using System;
using System.Collections.Generic;

namespace Upida.Validation
{
	public class ValidationException : Exception
	{
		private IFailureList failures;
		private Type typeValidatorType;
		private object group;

		public ValidationException(IFailureList errors)
		{
			this.failures = errors;
		}

		public ValidationException(IFailureList errors, Type typeValidatorType, object group)
		{
			this.failures = errors;
			this.typeValidatorType = typeValidatorType;
			this.group = group;
		}

		public FailResponse BuildFailResponse()
		{
			FailResponse response = new FailResponse();
			response.Failures = this.failures;
			return response;
		}

		public IFailureList Failures
		{
			get { return this.failures; }
		}

		public Type TypeValidatorType
		{
			get { return this.typeValidatorType; }
		}

		public object Group
		{
			get { return this.group; }
		}
	}
}