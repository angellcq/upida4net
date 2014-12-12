using System;
using System.Collections;
using System.Reflection;

namespace Upida.Impl
{
    public class PropertyMetaFactory
    {
        /// <summary>
        /// Creates an instance of PropertyMeta class based on property descriptor
        /// </summary>
        /// <param name="descriptor">property descriptor</param>
        /// <returns>instance of PropertyMeta class</returns>
        public PropertyMeta Create(PropertyInfo descriptor)
        {
            PropertyMeta meta = new PropertyMeta(descriptor);
            DtoAttribute annotation = descriptor.GetCustomAttribute<DtoAttribute>();
            if (null != annotation)
            {
                meta.Valid = true;
                meta.DtoLevel = annotation.Value;
                meta.DtoNestedLevel = annotation.Nested;
                meta.DtoCustomType = annotation.IsCustomType;
                meta.DtoDynamic = annotation.Dynamic;
                meta.PropertyClassType = this.BuildClassType(meta.PropertyClass, annotation);
                meta.NestedGenericClass = this.BuildNestedGenericClass(meta.PropertyClass);
                meta.Parser = UpidaContext.Current.BuildParser(meta.Name, meta.PropertyClass, meta.PropertyClassType, annotation);
            }

            return meta;
        }

        private PropertyMeta.ClassType BuildClassType(Type propertyClass, DtoAttribute annotation)
        {
            PropertyMeta.ClassType propertyClassType = PropertyMeta.ClassType.Value;
            if (typeof(string).IsAssignableFrom(propertyClass))
            {
                propertyClassType = PropertyMeta.ClassType.Value;
            }
            else if (typeof(Dtobase).IsAssignableFrom(propertyClass))
            {
                propertyClassType = annotation.IsCustomType ? PropertyMeta.ClassType.CustomType : PropertyMeta.ClassType.Class;
            }
            else if (typeof(IEnumerable).IsAssignableFrom(propertyClass))
            {
                propertyClassType = annotation.IsCustomType ? PropertyMeta.ClassType.CustomTypeCollection : PropertyMeta.ClassType.Collection;
            }

            return propertyClassType;
        }

        private Type BuildNestedGenericClass(Type propertyClass)
        {
            Type nestedGenericClass = null;
            Type[] generics = propertyClass.GetGenericArguments();
            if (null != generics && generics.Length > 0)
            {
                nestedGenericClass = generics[0];
            }

            return nestedGenericClass;
        }
    }
}