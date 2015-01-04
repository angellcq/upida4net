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
                bool written = false;
                if (null != current.Value.Name)
                {
                    text.Append(current.Value.Name);
                    written = true;
                }

                if (current.Value.Index.HasValue)
                {
                    text.Append('[');
                    text.Append(current.Value.Index.Value);
                    text.Append(']');
                    written = true;
                }

                if (written)
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
            path.AddLast(new PathNode(null));
            return path;
        }

        /// <summary>
        /// Sets top node target
        /// </summary>
        /// <param name="path">existing path</param>
        /// <param name="target">target object</param>
        public void SetTopNodeTarget(LinkedList<PathNode> path, Dtobase target)
        {
            path.Last.Value.Target = target;
        }

        /// <summary>
        /// Gets top node target
        /// </summary>
        /// <param name="path">existing path</param>
        /// <returns></returns>
        public Dtobase GetTopNodeTarget(LinkedList<PathNode> path)
        {
            return path.Last.Value.Target;
        }
    }
}