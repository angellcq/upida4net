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

		/// <summary>
		/// Creates instance of the Failure class
		/// </summary>
		/// <param name="key">key</param>
		/// <param name="text">text</param>
		/// <param name="severity">severity</param>
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

		/// <summary>
		/// Returns severety of the failure
		/// </summary>
		/// <returns></returns>
		public Severity GetSeverity()
		{
			return this.severity;
		}
	}
}