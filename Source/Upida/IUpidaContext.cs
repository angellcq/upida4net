using System;
using Upida.Impl;
using Upida.Validation;

namespace Upida
{
    /// <summary>
    /// Defines Upida context object methods
    /// </summary>
    public interface IUpidaContext
    {
        /// <summary>
        /// Gets singleton instance of the PathHelpr class
        /// </summary>
        IPathHelper PathHelper { get; }

        /// <summary>
        /// Gets singleton instance of the Checker class
        /// </summary>
        IChecker Checker { get; }

        /// <summary>
        /// Gets singleton instance of the Math class
        /// </summary>
        IMath Math { get; }

        /// <summary>
        /// Generates an array of ProperyMeta class from a domain class
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        PropertyMeta[] GetPropertyDefs(Type type);

        /// <summary>
        /// Creates a property parser instance based on property class
        /// </summary>
        /// <param name="name">property name</param>
        /// <param name="propertyClass">property class</param>
        /// <param name="propertyClassType">property class type</param>
        /// <param name="annotation">DtoAttribute instance</param>
        /// <returns>parser instance</returns>
        IParser BuildParser(string name, Type propertyClass, PropertyMeta.ClassType propertyClassType, DtoAttribute annotation);

        /// <summary>
        /// Creates a new instance of the generic ListAndSet<T> class
        /// </summary>
        /// <param name="type">inner type</param>
        /// <returns>new instance of ListAndSet<T></returns>
        object BuildList(Type type);
    }
}