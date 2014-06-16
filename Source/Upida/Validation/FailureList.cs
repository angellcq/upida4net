﻿using System;
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

		/// <summary>
		/// Adds a new failure object to the list
		/// </summary>
		/// <param name="text">text of the failure</param>
		public void Fail(string text)
		{
			this.Fail(null, text, Severity.None);
		}

		/// <summary>
		/// Adds a new failure object to the list
		/// </summary>
		/// <param name="text">text of the failure</param>
		/// <param name="severity">severity of the failure</param>
		public void Fail(string text, Severity severity)
		{
			this.Fail(null, text, severity);
		}

		/// <summary>
		/// Adds a new failure object to the list
		/// </summary>
		/// <param name="key">key of the failure</param>
		/// <param name="text">text of the failure</param>
		public void Fail(string key, string text)
		{
			this.Fail(key, text, Severity.None);
		}

		/// <summary>
		/// Adds a new failure object to the list
		/// </summary>
		/// <param name="key">key of the failure</param>
		/// <param name="text">text of the failure</param>
		/// <param name="severity">severity of the failure</param>
		public void Fail(string key, string text, Severity severity)
		{
			this.Fail(new Failure(key, text, severity));
		}

		/// <summary>
		/// Adds a new failure object to the list if condition is true
		/// </summary>
		/// <param name="condition">condition</param>
		/// <param name="text">text of the failure</param>
		public void FailIf(bool condition, string text)
		{
			this.FailIf(condition, null, text, Severity.None);
		}

		/// <summary>
		/// Adds a new failure object to the list if condition is true
		/// </summary>
		/// <param name="condition">condition</param>
		/// <param name="text">text of the failure</param>
		/// <param name="severity">severity of the failure</param>
		public void FailIf(bool condition, string text, Severity severity)
		{
			this.FailIf(condition, null, text, severity);
		}

		/// <summary>
		/// Adds a new failure object to the list if condition is true
		/// </summary>
		/// <param name="condition">condition</param>
		/// <param name="key">key of the failure</param>
		/// <param name="text">text of the failure</param>
		public void FailIf(bool condition, string key, string text)
		{
			this.FailIf(condition, key, text, Severity.None);
		}

		/// <summary>
		/// Adds a new failure object to the list if condition is true
		/// </summary>
		/// <param name="condition">condition</param>
		/// <param name="key">key of the failure</param>
		/// <param name="text">text of the failure</param>
		/// <param name="severity">severity of the failure</param>
		public void FailIf(bool condition, string key, string text, Severity severity)
		{
			if (condition)
			{
				this.Fail(key, text, severity);
			}
		}
	}
}