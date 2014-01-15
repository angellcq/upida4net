namespace Upida.Validation
{
	/// <summary>
	/// Represents a single validation failure
	/// </summary>
	public class Failure
	{
		private string key;
		private string text;
		private Severity severity;

		public Failure(string key, string text, Severity severity)
		{
			this.key = key;
			this.text = text;
			this.severity = severity;
		}

		/// <summary>
		/// Property path of the failure
		/// </summary>
		public string Key
		{
			get { return this.key; }
		}

		/// <summary>
		/// Text message of the failure
		/// </summary>
		public string Text
		{
			get { return this.text; }
		}

		public Severity GetSeverity()
		{
			return this.severity;
		}
	}
}