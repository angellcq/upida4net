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
			: this(byte.MaxValue)
		{
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
		/// Gets or sets value that represents serialization level, applied to current property
		/// </summary>
		public byte Value
		{
			get { return this.value; }
			set { this.value = value; }
		}

		/// <summary>
		/// Gets or sets value that represents serialization level, applied to child properties. Used only when decorated property is DTO class
		/// </summary>
		public byte Nested
		{
			get { return this.nested; }
			set { this.nested = value; }
		}

		/// <summary>
		/// Gets or sets value that tells if decorated property is custom Hibernate type
		/// </summary>
		public bool IsCustomType
		{
			get { return this.isCustomType; }
			set { this.isCustomType = value; }
		}

		/// <summary>
		/// Gets or sets value that represents parser object, used to parse this property from text
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