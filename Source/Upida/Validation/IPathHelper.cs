using System.Collections.Generic;
using Upida.Validation.Impl;

namespace Upida.Validation
{
    /// <summary>
    /// Defines methods for building property path
    /// </summary>
    public interface IPathHelper
    {
        /// <summary>
        /// Builds property path using existing path and a nested property name
        /// </summary>
        /// <param name="path">existing path</param>
        /// <param name="name">nested property name</param>
        /// <returns>property path text</returns>
        string BuildPath(LinkedList<PathNode> path, string name);
    }
}