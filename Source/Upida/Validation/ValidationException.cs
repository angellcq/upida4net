using System;
using System.Collections.Generic;

namespace Upida.Validation
{
    /// <summary>
    /// Represents Validation exception
    /// </summary>
    public class ValidationException : Exception
    {
        private IFailureList failures;

        public ValidationException(IFailureList errors)
        {
            this.failures = errors;
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
    }
}