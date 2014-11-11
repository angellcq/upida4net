using System;
using System.Collections.Generic;

namespace Upida.Validation
{
    /// <summary>
    /// Represents result of validation
    /// </summary>
    public class FailResponse
    {
        private IFailureList failures;

        /// <summary>
        /// Creates instance of the FailResponse class
        /// </summary>
        public FailResponse()
        {
            this.failures = null;
        }

        /// <summary>
        /// Creates instance of the FailResponse class and fills it with a list of failures
        /// </summary>
        /// <param name="failures">list of failures</param>
        public FailResponse(IFailureList failures)
        {
            this.failures = failures;
        }

        /// <summary>
        /// Creates an instance of the FailureResponse class with one failure in it
        /// </summary>
        /// <param name="message"></param>
        public FailResponse(string message)
        {
            this.failures = new FailureList();
            this.failures.Fail(null, message);
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