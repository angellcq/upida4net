using Newtonsoft.Json.Linq;
using System;
using System.Collections;

namespace Upida
{
    public interface IJsonParser
    {
        /// <summary>
        /// Parses JSON data into domain object
        /// </summary>
        /// <typeparam name="T">domain object type</typeparam>
        /// <param name="form">JSON tree</param>
        /// <returns>parsed domain object instance</returns>
        T Parse<T>(JToken node) where T : Dtobase;

        /// <summary>
        /// Parses JSON data into domain object
        /// </summary>
        /// <param name="node">JSON tree</param>
        /// <param name="type">domain object type</param>
        /// <returns>parsed domain object instance</returns>
        object Parse(JToken node, Type type);

        /// <summary>
        /// Parses JSON data into a list of domain objects
        /// </summary>
        /// <param name="node">JSON tree</param>
        /// <param name="type">domain object type</param>
        /// <returns>parsed domain object instance</returns>
        IEnumerable ParseList(JToken node, Type type);
    }
}