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

        /// <summary>
        /// Initializes new instance of the ValidationException class
        /// </summary>
        /// <param name="errors">list of failures</param>
        public ValidationException(IFailureList errors)
        {
            this.failures = errors;
        }

        /// <summary>
        /// Generates FailResponse object
        /// </summary>
        /// <returns>instance of FailResponse class</returns>
        public FailResponse BuildFailResponse()
        {
            FailResponse response = new FailResponse();
            response.Failures = this.failures;
            return response;
        }

        /// <summary>
        /// Gets list of failures
        /// </summary>
        public IFailureList Failures
        {
            get { return this.failures; }
        }
    }
}