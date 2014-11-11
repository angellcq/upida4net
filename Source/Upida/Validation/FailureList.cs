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
        /// The highest severity in the list of failures
        /// </summary>
        public virtual Severity Severity
        {
            get { return this.severity; }
            internal set { this.severity = value; }
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
            if (item.GetSeverity() > this.severity)
            {
                this.severity = item.GetSeverity();
            }
        }

        public void Fail(string key, string text, Severity severity = Severity.None)
        {
            this.Fail(new Failure(key, text, severity));
        }

        public void FailIf(bool condition, string key, string text, Severity severity = Severity.None)
        {
            if (condition)
            {
                this.Fail(key, text, severity);
            }
        }

        public void FailIfNot(bool condition, string key, string text, Severity severity = Severity.None)
        {
            this.FailIf(!condition, key, text, severity);
        }

        public void FailIfNull(object target, string key, string text, Severity severity = Severity.None)
        {
            this.FailIf(null == target, key, text, severity);
        }

        public void FailIfNotNull(object target, string key, string text, Severity severity = Severity.None)
        {
            this.FailIf(null != target, key, text, severity);
        }

        public void FailIfEqual<T>(T a, T b, string key, string text, Severity severity = Severity.None)
        {
            this.FailIf(object.Equals(a, b), key, text, severity);
        }

        public void FailIfNotEqual<T>(T a, T b, string key, string text, Severity severity = Severity.None)
        {
            this.FailIf(!object.Equals(a, b), key, text, severity);
        }

        public void FailIfEmpty(ICollection collection, string key, string text, Severity severity = Severity.None)
        {
            this.FailIf(0 == collection.Count, key, text, severity);
        }

        public void FailIfNotEmpty(ICollection collection, string key, string text, Severity severity = Severity.None)
        {
            this.FailIf(0 != collection.Count, key, text, severity);
        }
    }
}