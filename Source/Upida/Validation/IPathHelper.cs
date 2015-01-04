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

        /// <summary>
        /// Creates a new instance of the path list
        /// </summary>
        /// <returns></returns>
        LinkedList<PathNode> CreateNew();

        /// <summary>
        /// Sets top node target
        /// </summary>
        /// <param name="path">existing path</param>
        /// <param name="target">target object</param>
        void SetTopNodeTarget(LinkedList<PathNode> path, Dtobase target);

        /// <summary>
        /// Gets top node target
        /// </summary>
        /// <param name="path">existing path</param>
        /// <returns></returns>
        Dtobase GetTopNodeTarget(LinkedList<PathNode> path);
    }
}