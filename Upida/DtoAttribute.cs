using System;

namespace Upida
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DtoAttribute : Attribute
    {
        private byte value;
        private byte nested;
        private Type parser;
        private bool isCustomType;

        public DtoAttribute()
            : this(byte.MaxValue)
        {
        }

        public DtoAttribute(byte value)
        {
            this.value = value;
            this.nested = byte.MaxValue;
        }

        public byte Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public byte Nested
        {
            get { return this.nested; }
            set { this.nested = value; }
        }

        public bool IsCustomType
        {
            get { return this.isCustomType; }
            set { this.isCustomType = value; }
        }

        public Type Parser
        {
            get { return this.parser; }
            set { this.parser = value; }
        }

        public bool HasNested
        {
            get { return byte.MaxValue != this.nested; }
        }
    }
}