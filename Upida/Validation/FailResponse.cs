using System;
using System.Collections.Generic;

namespace Upida.Validation
{
    public class FailResponse
    {
        private String main;
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

        public string Main
        {
            get { return this.main; }
        }

        public IList<Failure> Failures
        {
            get { return this.failures; }
            set { this.failures = value; }
        }
    }
}