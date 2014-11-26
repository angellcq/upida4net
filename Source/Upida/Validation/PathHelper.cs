using System.Collections.Generic;
using System.Text;

namespace Upida.Validation
{
    public class PathHelper
    {
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
    }
}