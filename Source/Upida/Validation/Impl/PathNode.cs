using System;

namespace Upida.Validation.Impl
{
    /// <summary>
    /// Represents property path node
    /// </summary>
    public class PathNode
    {
        /// <summary>
        /// Initializes new instance of the PathNode class with name
        /// </summary>
        /// <param name="name">node name</param>
        public PathNode(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Node index (valid for arrays and lists)
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// Node name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Node Target object
        /// </summary>
        public Dtobase Target { get; set; }
    }
}