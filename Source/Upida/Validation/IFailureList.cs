using System.Collections.Generic;

namespace Upida.Validation
{
    /// <summary>
    /// Represents list of validation failures
    /// </summary>
    public interface IFailureList : IList<Failure>
    {
        /// <summary>
        /// True if list is empty
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Adds a new failure object to the list
        /// </summary>
        /// <param name="item">failure object</param>
        void Fail(Failure item);

        /// <summary>
        /// Adds a new failure object to the list
        /// </summary>
        /// <param name="key">key of the failure</param>
        /// <param name="text">text of the failure</param>
        void Fail(string key, string text);

        /// <summary>
        /// Adds a new failure object to the list if condition is true
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="key">key of the failure</param>
        /// <param name="text">text of the failure</param>
        void FailIf(bool condition, string key, string text);
    }
}