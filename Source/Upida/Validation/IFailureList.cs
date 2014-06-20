using System;
using System.Collections;
using System.Collections.Generic;

namespace Upida.Validation
{
	public interface IFailureList : IList<Failure>
	{
		/// <summary>
		/// The highest severity in the list of failures
		/// </summary>
		Severity Severity { get; }

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
		/// <param name="severity">severity of the failure</param>
		void Fail(string key, string text, Severity severity = Severity.None);

		/// <summary>
		/// Adds a new failure object to the list if condition is true
		/// </summary>
		/// <param name="condition">condition</param>
		/// <param name="key">key of the failure</param>
		/// <param name="text">text of the failure</param>
		/// <param name="severity">severity of the failure</param>
		void FailIf(bool condition, string key, string text, Severity severity = Severity.None);

		void FailIfNot(bool condition, string key, string text, Severity severity = Severity.None);

		void FailIfNull(object target, string key, string text, Severity severity = Severity.None);

		void FailIfNotNull(object target, string key, string text, Severity severity = Severity.None);

		void FailIfEqual<T>(T a, T b, string key, string text, Severity severity = Severity.None);

		void FailIfNotEqual<T>(T a, T b, string key, string text, Severity severity = Severity.None);

		void FailIfEmpty(ICollection collection, string key, string text, Severity severity = Severity.None);

		void FailIfNotEmpty(ICollection collection, string key, string text, Severity severity = Severity.None);
	}
}