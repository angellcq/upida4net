using System;

namespace Upida
{
    /// <summary>
    /// Represents marker attribute for DTO class properties
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DtoAttribute : Attribute
    {
        private byte value;
        private byte nested;
        private Type parser;
        private bool isCustomType;
        private bool dynamic;

        /// <summary>
        /// Creates instance of the DtoAttribute class
        /// </summary>
        public DtoAttribute()
        {
            this.value = byte.MaxValue;
            this.nested = byte.MaxValue;
        }

        /// <summary>
        /// Creates instance of the DtoAttribute class
        /// </summary>
        /// <param name="value">serialization level</param>
        public DtoAttribute(byte value)
        {
            this.value = value;
            this.nested = byte.MaxValue;
        }

        /// <summary>
        /// Creates instance of the DtoAttribute class
        /// </summary>
        /// <param name="value">serialization level</param>
        /// <param name="nested">nested class serialization level</param>
        public DtoAttribute(byte value, byte nested)
        {
            this.value = value;
            this.nested = nested;
        }

        /// <summary>
        /// Level of serialization applied to this property
        /// </summary>
        public byte Value
        {
            get { return this.value; }
        }

        /// <summary>
        /// If the current property represents nested domain object, then this field defines
        /// the Level of serialization applied to the child properties of the domain object
        /// </summary>
        public byte Nested
        {
            get { return this.nested; }
        }

        /// <summary>
        /// Must be set to true if current property is Custom Type.
        /// </summary>
        public bool IsCustomType
        {
            get { return this.isCustomType; }
            set { this.isCustomType = value; }
        }

        /// <summary>
        /// You can provide your own parser. Creating custom parser is simple.
        /// You have to implement the IParser interface.
        /// If parsing is impossible due to wrong format custom parser must throw some Exception
        /// </summary>
        public Type Parser
        {
            get { return this.parser; }
            set { this.parser = value; }
        }

        /// <summary>
        /// Gets or sets value that indicates if nested serialization level is resolved dynamically.
        /// Nested level can be dynamic if you are trying to serialize parent object using some level higher than the level indicated in the DtoAttribute.Value.
        /// In this case the nested level is calculated at runtime based on difference between the required level and the DtoAttribute.Value.
        /// This difference is added to the DtoAttribute.Nested serialization level. This behaviour works only if Dynamic property is true.
        /// </summary>
        public bool Dynamic
        {
            get { return this.dynamic; }
            set { this.dynamic = value; }
        }
    }
}