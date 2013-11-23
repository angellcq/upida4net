using System;
using System.Collections.Generic;

namespace Upida.Validation
{
	public class ValidationException : Exception
	{
		private IList<Failure> failures;
		private Type typeValidatorType;
		private object group;

		public ValidationException(IList<Failure> errors, Type typeValidatorType, object group)
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

		public IList<Failure> GetFailures()
		{
			return this.failures;
		}

		public Type GetTypeValidatorType()
		{
			return this.typeValidatorType;
		}

		public object GetGroup()
		{
			return this.group;
		}
	}
}