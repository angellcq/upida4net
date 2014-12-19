using System;

namespace Upida
{
    /// <summary>
    /// Defines parsing method (used for parsing field values).
    /// Implement this interface for custom type properties, and mark it in the DtoAttribute.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Must throw Exception if parsing failed
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        object ParseTextValue(Type type, string text);
    }
}