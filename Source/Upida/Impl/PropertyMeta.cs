using System;
using System.Collections;
using System.Reflection;

namespace Upida.Impl
{
    /// <summary>
    /// Represents Domain object Property metadata
    /// </summary>
    public class PropertyMeta
    {
        public enum ClassType { None, Class, CustomType, Collection, CustomTypeCollection, Value }

        private PropertyInfo descriptor;
        private string name;
        private Type propertyClass;

        /// <summary>
        /// Initializesa new instance of the PropertyMeta class
        /// </summary>
        /// <param name="descriptor"></param>
        public PropertyMeta(PropertyInfo descriptor)
        {
            this.descriptor = descriptor;
            this.name = string.Concat(Char.ToLower(descriptor.Name[0]), descriptor.Name.Substring(1));
            this.propertyClass = descriptor.PropertyType;
        }

        /// <summary>
        /// Gets property name
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Gets property class
        /// </summary>
        public Type PropertyClass
        {
            get { return this.propertyClass; }
        }

        /// <summary>
        /// Gets property class type
        /// </summary>
        public ClassType PropertyClassType { get; set; }

        /// <summary>
        /// Gets property parser object
        /// </summary>
        public IParser Parser { get; set; }

        /// <summary>
        /// Gets property inner generic class (used for IList<T>, ISet<T>, etc)
        /// </summary>
        public Type InnerGenericClass { get; set; }

        /// <summary>
        /// Gets serialization level assigned to the property
        /// </summary>
        public byte DtoLevel { get; set; }

        /// <summary>
        /// Gets serialization level assigned to property children
        /// </summary>
        public byte DtoNestedLevel { get; set; }

        /// <summary>
        /// Returns true if the property is custom type (used for Hibernate custom types)
        /// </summary>
        public bool DtoCustomType { get; set; }

        /// <summary>
        /// Returns true if serialization level assigned to property children is resolved dynamicaly
        /// </summary>
        public bool DtoDynamic { get; set; }

        /// <summary>
        /// Returns true is property is valid DTO
        /// </summary>
        public bool Valid { get; set; }

        /// <summary>
        /// Determines if the property falls into the requested serialization level
        /// </summary>
        /// <param name="level">the requested serialization level</param>
        /// <returns>true is yes</returns>
        public bool HasLevel(byte level)
        {
            return this.DtoLevel <= level;
        }

        /// <summary>
        /// Determines if the property instance is assigned by JSON deserializer
        /// </summary>
        /// <param name="target">object instance</param>
        /// <returns>true if assigned</returns>
        public bool IsAssigned(Dtobase target)
        {
            return target.IsFieldAssigned(this.name);
        }

        /// <summary>
        /// Assignes value to a property instance
        /// </summary>
        /// <param name="target">object instance</param>
        /// <param name="value">value</param>
        public void Write(Dtobase target, object value)
        {
            this.descriptor.SetValue(target, value);
        }

        /// <summary>
        /// Reads value of the property instance
        /// </summary>
        /// <param name="target">object instance</param>
        /// <returns>value</returns>
        public object Read(Dtobase target)
        {
            return this.descriptor.GetValue(target);
        }
    }
}