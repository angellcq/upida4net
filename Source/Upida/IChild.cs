using System;

namespace Upida
{
    /// <summary>
    /// Defines a method for cascaded child domain objects
    /// </summary>
    public interface IChild
    {
        /// <summary>
        /// Connects parent domain object to a child domain object
        /// </summary>
        /// <param name="parent"></param>
        void ConnectToParent(object parent);
    }
}