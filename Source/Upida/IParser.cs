using System;

namespace Upida
{
    /// <summary>
    /// Defines JsonParser class methods
    /// </summary>
	public interface IParser
	{
		/// <summary>
		/// Must throw ArgumentException if parsing failed
		/// </summary>
		/// <param name="?"></param>
		/// <returns></returns>
		object ParseTextValue(Type type, string text);
	}
}