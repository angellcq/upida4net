using System;
using System.Collections;
using System.Collections.Generic;

namespace Upida.Validation
{
    public class FailureList : List<Failure>, IFailureList
    {
        private Severity severity = Severity.None;

        public FailureList()
        {
        }

        public FailureList(int capacity)
            : base(capacity)
        {
        }

        public FailureList(IEnumerable<Failure> collection)
            : base(collection)
        {
        }

        /// <summary>
        /// True if list is empty
        /// </summary>
        public virtual bool IsEmpty
        {
            get { return 0 == this.Count; }
        }

        /// <summary>
        /// Adds a new failure object to the list
        /// </summary>
        /// <param name="item">failure object</param>
        public void Fail(Failure item)
        {
            this.Add(item);
        }

        public void Fail(string key, string text)
        {
            this.Fail(new Failure(key, text));
        }

        public void FailIf(bool condition, string key, string text)
        {
            if (condition)
            {
                this.Fail(key, text);
            }
        }
    }
}