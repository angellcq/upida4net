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
		private DtoAttribute annotation;
		private Type propertyClass;
		private ClassType propertyClassType = ClassType.None;
		private IParser parser;
		private Type nestedType;

		public PropertyMeta(PropertyInfo descriptor)
		{
			this.descriptor = descriptor;
			this.name = descriptor.Name;
			this.name = string.Concat(Char.ToLower(this.name[0]), this.name.Substring(1));
			this.annotation = this.descriptor.GetCustomAttribute<DtoAttribute>();
			this.propertyClass = this.descriptor.PropertyType;
			if (null != this.annotation)
			{
				this.propertyClassType = this.BuildPropertyClassType();
				if (this.propertyClassType == ClassType.Value)
				{
					this.parser = UpidaContext.Current().BuildParser(this);
				}
			}
		}

		public bool isAssigned(Dtobase target)
		{
			return target.IsFieldAssigned(this.name);
		}

		public bool IsValid
		{
			get { return null != this.annotation; }
		}

		public Type PropertyClass
		{
			get { return this.propertyClass; }
		}

		public ClassType PropertyClassType
		{
			get { return this.propertyClassType; }
		}

		public DtoAttribute Annotation
		{
			get { return this.annotation; }
		}

		public IParser Parser
		{
			get { return this.parser; }
		}

		public string Name
		{
			get { return this.name; }
		}

		public Type NestedType
		{
			get
			{
				if (null == this.nestedType)
				{
					this.nestedType = this.PropertyClass.GetGenericArguments()[0];
				}

				return this.nestedType;
			}
		}

		public bool HasLevel(byte level)
		{
			return level >= this.annotation.Value;
		}

		public void Write(Dtobase target, object value)
		{
			this.descriptor.SetValue(target, value);
		}

		public object Read(Dtobase target)
		{
			return this.descriptor.GetValue(target);
		}

		private ClassType BuildPropertyClassType()
		{
			if (ClassType.None == this.propertyClassType)
			{
				this.propertyClassType = ClassType.Value;
				if (typeof(string).IsAssignableFrom(this.propertyClass))
				{
					this.propertyClassType = ClassType.Value;
				}
				else if (typeof(Dtobase).IsAssignableFrom(this.propertyClass))
				{
					this.propertyClassType = this.annotation.IsCustomType ? ClassType.CustomType : ClassType.Class;
				}
				else if (typeof(IEnumerable).IsAssignableFrom(this.propertyClass))
				{
					this.propertyClassType = this.annotation.IsCustomType ? ClassType.CustomTypeCollection : ClassType.Collection;
				}
			}

			return this.propertyClassType;
		}
	}
}