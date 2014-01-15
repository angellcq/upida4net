using System;
using System.Collections.Generic;

namespace Upida.Validation
{
	public class FailureList : List<Failure>
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

		public Severity Severity
		{
			get { return this.severity; }
			set { this.severity = value; }
		}
	}
}