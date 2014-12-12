using System.Collections.Generic;
using Upida.Validation.Impl;

namespace Upida.Validation
{
    public interface IPathHelper
    {
        string BuildPath(LinkedList<PathNode> path, string name);
    }
}