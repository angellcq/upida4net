using System;
using System.Collections;
using System.Collections.Generic;

namespace Upida.Validation.Impl
{
    /// <summary>
    /// Represents list of failures
    /// </summary>
    public class FailureList : List<Failure>, IFailureList
    {
        private Severity severity = Severity.None;

        /// <summary>
        /// Creates new Failure instance
        /// </summary>
        public FailureList()
        {
        }

        /// <summary>
        /// Overriden from List<T>
        /// </summary>
        /// <param name="capacity"></param>
        public FailureList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Overriden from List<T>
        /// </summary>
        /// <param name="collection"></param>
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

        /// <summary>
        ///  Adds a new failure object to the list with key and text message
        /// </summary>
        /// <param name="key">failure key (property path)</param>
        /// <param name="text">text message</param>
        public void Fail(string key, string text)
        {
            this.Fail(new Failure(key, text));
        }

        /// <summary>
        /// Adds a new failure object to the list if condition is true
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="key">failure key (property path)</param>
        /// <param name="text">text message</param>
        public void FailIf(bool condition, string key, string text)
        {
            if (condition)
            {
                this.Fail(key, text);
            }
        }
    }
}