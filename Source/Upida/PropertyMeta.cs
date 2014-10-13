using System;
using System.Collections;
using System.Reflection;

namespace Upida
{
	public class PropertyMeta
	{
		public enum ClassType { None, Class, CustomType, Collection, CustomTypeCollection, Value }

		private PropertyInfo descriptor;
		private string name;
		private Type propertyClass;

		public PropertyMeta(PropertyInfo descriptor)
		{
			this.descriptor = descriptor;
			this.name = string.Concat(Char.ToLower(descriptor.Name[0]), descriptor.Name.Substring(1));
			this.propertyClass = descriptor.PropertyType;
		}

		public string Name
		{
			get { return this.name; }
		}

		public Type PropertyClass
		{
			get { return this.propertyClass; }
		}

		public ClassType PropertyClassType { get; set; }
		public IParser Parser { get; set; }
		public Type NestedGenericClass { get; set; }
		public byte DtoLevel { get; set; }
		public byte DtoNestedLevel { get; set; }
		public bool DtoCustomType { get; set; }
		public bool DtoDynamic { get; set; }
		public bool Valid { get; set; }

		public bool HasLevel(byte level)
		{
			return this.DtoLevel <= level;
		}

		public bool IsAssigned(Dtobase target)
		{
			return target.IsFieldAssigned(this.name);
		}

		public void Write(Dtobase target, object value)
		{
			this.descriptor.SetValue(target, value);
		}

		public object Read(Dtobase target)
		{
			return this.descriptor.GetValue(target);
		}
	}
}