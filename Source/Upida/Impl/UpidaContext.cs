using System;
using System.Collections.Generic;
using System.Reflection;
using Upida.Validation;

namespace Upida.Impl
{
    /// <summary>
    /// Represents Upida context object (used internally to store context data)
    /// </summary>
    public class UpidaContext
    {
        private static readonly UpidaContext CURRENT = new UpidaContext();

        /// <summary>
        /// Gets current Upida context
        /// </summary>
        public static UpidaContext Current
        {
            get { return CURRENT; }
        }

        private readonly IDictionary<Type, PropertyMeta[]> PROPERTY_DEF_MAP = new Dictionary<Type, PropertyMeta[]>();

        private readonly Type STRING_TYPE = typeof(string);
        private readonly Type LONG_TYPE = typeof(long?);
        private readonly Type ULONG_TYPE = typeof(ulong?);
        private readonly Type INTEGER_TYPE = typeof(int?);
        private readonly Type UINTEGER_TYPE = typeof(uint?);
        private readonly Type SHORT_TYPE = typeof(short?);
        private readonly Type USHORT_TYPE = typeof(ushort?);
        private readonly Type BYTE_TYPE = typeof(byte?);
        private readonly Type SBYTE_TYPE = typeof(sbyte?);
        private readonly Type DOUBLE_TYPE = typeof(double?);
        private readonly Type FLOAT_TYPE = typeof(float?);
        private readonly Type BOOLEAN_TYPE = typeof(bool?);
        private readonly Type CHAR_TYPE = typeof(char?);
        private readonly Type DATE_TYPE = typeof(DateTime?);
        private readonly Type GUID_TYPE = typeof(Guid?);

        private readonly Type LONG_PRIM = typeof(long);
        private readonly Type ULONG_PRIM = typeof(ulong);
        private readonly Type INTEGER_PRIM = typeof(int);
        private readonly Type UINTEGER_PRIM = typeof(uint);
        private readonly Type SHORT_PRIM = typeof(short);
        private readonly Type USHORT_PRIM = typeof(ushort);
        private readonly Type BYTE_PRIM = typeof(byte);
        private readonly Type SBYTE_PRIM = typeof(sbyte);
        private readonly Type DOUBLE_PRIM = typeof(double);
        private readonly Type FLOAT_PRIM = typeof(float);
        private readonly Type BOOLEAN_PRIM = typeof(bool);
        private readonly Type CHAR_PRIM = typeof(char);
        private readonly Type GUID_PRIM = typeof(Guid);

        private readonly PropertyMetaFactory propertyMetaFactory = new PropertyMetaFactory();
        private readonly IPathHelper pathHelper = new Upida.Validation.Impl.PathHelper();
        private readonly IChecker checker = new Upida.Validation.Impl.Checker();
        private readonly IMath math = new Upida.Validation.Impl.Math();

        /// <summary>
        /// Gets singleton instance of the PathHelpr class
        /// </summary>
        public IPathHelper PathHelper
        {
            get { return this.pathHelper; }
        }

        /// <summary>
        /// Gets singleton instance of the Checker class
        /// </summary>
        public IChecker Checker
        {
            get { return this.checker; }
        }

        /// <summary>
        /// Gets singleton instance of the Math class
        /// </summary>
        public IMath Math
        {
            get { return this.math; }
        }

        /// <summary>
        /// Generates an array of ProperyMeta class from a domain class
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public PropertyMeta[] GetPropertyDefs(Type type)
        {
            PropertyMeta[] defs;
            bool found = PROPERTY_DEF_MAP.TryGetValue(type, out defs);
            if (found)
            {
                return defs;
            }

            lock (this)
            {
                found = PROPERTY_DEF_MAP.TryGetValue(type, out defs);
                if (found)
                {
                    return defs;
                }

                PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                defs = new PropertyMeta[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    defs[i] = this.propertyMetaFactory.Create(properties[i]);
                }

                PROPERTY_DEF_MAP.Add(type, defs);
            }

            return defs;
        }

        /// <summary>
        /// Creates a property parser instance based on property class
        /// </summary>
        /// <param name="name">property name</param>
        /// <param name="propertyClass">property class</param>
        /// <param name="propertyClassType">property class type</param>
        /// <param name="annotation">DtoAttribute instance</param>
        /// <returns>parser instance</returns>
        public IParser BuildParser(string name, Type propertyClass, PropertyMeta.ClassType propertyClassType, DtoAttribute annotation)
        {
            if (PropertyMeta.ClassType.Value != propertyClassType)
            {
                return null;
            }

            IParser parser = null;
            try
            {
                if (null != annotation.Parser)
                {
                    parser = (IParser)Activator.CreateInstance(annotation.Parser);
                    return parser;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to instantiate parser for - property:" + name + " of type:" + propertyClass.Name, ex);
            }

            Type type = propertyClass;
            if (this.STRING_TYPE == type)
            {
                parser = StandardParsers.STRING_PARSER;
            }
            else if (this.LONG_TYPE == type || this.LONG_PRIM == type)
            {
                parser = StandardParsers.LONG_PARSER;
            }
            else if (this.ULONG_TYPE == type || this.ULONG_PRIM == type)
            {
                parser = StandardParsers.ULONG_PARSER;
            }
            else if (this.INTEGER_TYPE == type || this.INTEGER_PRIM == type)
            {
                parser = StandardParsers.INT_PARSER;
            }
            else if (this.UINTEGER_TYPE == type || this.UINTEGER_PRIM == type)
            {
                parser = StandardParsers.UINT_PARSER;
            }
            else if (this.DOUBLE_TYPE == type || this.DOUBLE_PRIM == type)
            {
                parser = StandardParsers.DOUBLE_PARSER;
            }
            else if (this.DATE_TYPE == type)
            {
                parser = StandardParsers.DATETIME_PARSER;
            }
            else if (this.BOOLEAN_TYPE == type || this.BOOLEAN_PRIM == type)
            {
                parser = StandardParsers.BOOL_PARSER;
            }
            else if (this.SHORT_TYPE == type || this.SHORT_PRIM == type)
            {
                parser = StandardParsers.SHORT_PARSER;
            }
            else if (this.BYTE_TYPE == type || this.BYTE_PRIM == type)
            {
                parser = StandardParsers.BYTE_PARSER;
            }
            else if (this.SBYTE_TYPE == type || this.SBYTE_PRIM == type)
            {
                parser = StandardParsers.SBYTE_PARSER;
            }
            else if (this.FLOAT_TYPE == type || this.FLOAT_PRIM == type)
            {
                parser = StandardParsers.FLOAT_PARSER;
            }
            else if (this.CHAR_TYPE == type || this.CHAR_PRIM == type)
            {
                parser = StandardParsers.CHAR_PARSER;
            }
            else if (type.IsEnum)
            {
                parser = StandardParsers.ENUM_PARSER;
            }
            else if (this.GUID_TYPE == type || this.GUID_PRIM == type)
            {
                parser = StandardParsers.GUID_PARSER;
            }
            else if (type.GenericTypeArguments != null && type.GenericTypeArguments.Length == 1 && type.GenericTypeArguments[0].IsEnum)
            {
                parser = StandardParsers.ENUM_NULLABLE_PARSER;
            }
            else
            {
                throw new ApplicationException(
                    "Unable to find parser for property: " + name + ", of type: " + type.FullName +
                    ". Property class either must derive from Dtobase or You must setup custom parser for this property in the Dto attribute.");
            }

            return parser;
        }

        /// <summary>
        /// Creates a new instance of the generic ListAndSet<T> class
        /// </summary>
        /// <param name="type">inner type</param>
        /// <returns>new instance of ListAndSet<T></returns>
        public object BuildList(Type type)
        {
            Type listAndSetType = typeof(ListAndSet<>);
            Type[] typeArgs = { type };
            Type makeme = listAndSetType.MakeGenericType(typeArgs);
            return Activator.CreateInstance(makeme);
        }
    }
}