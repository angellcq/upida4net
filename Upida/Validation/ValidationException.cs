using System;
using System.Collections.Generic;

namespace Upida.Validation
{
    public class ValidationException : Exception
    {
        private IList<Failure> failures;

        public ValidationException(IList<Failure> errors)
        {
            this.failures = errors;
        }

        public FailResponse BuildFailResponse()
        {
            FailResponse response = new FailResponse();
            response.Failures = this.failures;
            return response;
        }
    }
}