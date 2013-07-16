using System;

namespace Upida
{
    public interface IParser
    {
        /// <summary>
        /// must throw ArgumentException if parsing failed
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        Object parseTextValue(Type type, String text);
    }
}