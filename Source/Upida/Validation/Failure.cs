namespace Upida.Validation
{
	/// <summary>
	/// Represents a single validation failure
	/// </summary>
	public class Failure
	{
		private string key;
		private string text;

		public Failure(string key, string text)
		{
			this.key = key;
			this.text = text;
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
	}
}