using System.Collections.Generic;
using System.Text;

namespace Upida.Validation.Impl
{
    /// <summary>
    /// Represents property path builder class
    /// </summary>
    public class PathHelper : IPathHelper
    {
        /// <summary>
        /// Builds property path using existing path and a nested property name
        /// </summary>
        /// <param name="path">existing path</param>
        /// <param name="name">nested property name</param>
        /// <returns>property path text</returns>
        public string BuildPath(LinkedList<PathNode> path, string name)
        {
            StringBuilder text = new StringBuilder();
            LinkedListNode<PathNode> current = path.First;
            while (null != current)
            {
                text.Append(current.Value.Name);
                if (current.Value.Index.HasValue)
                {
                    text.Append('[');
                    text.Append(current.Value.Index.Value);
                    text.Append(']');
                }

                if (!string.IsNullOrEmpty(current.Value.Name) ||
                    current.Value.Index.HasValue)
                {
                    text.Append('.');
                }

                current = current.Next;
            }

            text.Append(name);
            return text.ToString();
        }

        /// <summary>
        /// Creates a new instance of the path list
        /// </summary>
        /// <returns></returns>
        public LinkedList<PathNode> CreateNew()
        {
            LinkedList<PathNode> path = new LinkedList<PathNode>();
            path.AddLast(new PathNode(string.Empty));
            return path;
        }
    }
}