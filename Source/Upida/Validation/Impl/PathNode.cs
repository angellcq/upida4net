using System;

namespace Upida.Validation.Impl
{
    /// <summary>
    /// Represents property path node
    /// </summary>
    public class PathNode
    {
        /// <summary>
        /// Node index (valid for arrays and lists)
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// Node name
        /// </summary>
        public string Name { get; set; }
    }
}