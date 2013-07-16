using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Upida
{
    public class UpidaContext
    {
        private static readonly UpidaContext CURRENT = new UpidaContext();

        public static UpidaContext Current() {
            return CURRENT;
        }

        private readonly IDictionary<Type, PropertyMeta[]> PROPERTY_DEF_MAP = new Dictionary<Type, PropertyMeta[]>();

        private readonly Type STRING_TYPE = typeof(string);
        private readonly Type LONG_TYPE = typeof(long?);
        private readonly Type INTEGER_TYPE = typeof(int?);
        private readonly Type SHORT_TYPE = typeof(short?);
        private readonly Type BYTE_TYPE = typeof(byte?);
        private readonly Type DOUBLE_TYPE = typeof(double?);
        private readonly Type FLOAT_TYPE = typeof(float?);
        private readonly Type BOOLEAN_TYPE = typeof(bool?);
        private readonly Type CHAR_TYPE = typeof(char?);
        private readonly Type DATE_TYPE = typeof(DateTime?);

        public PropertyMeta[] GetPropertyDefs(Type type)
        {
            PropertyMeta[] defs;
            bool found = PROPERTY_DEF_MAP.TryGetValue(type, out defs);
            if (found)
            {
                return defs;
            }

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            defs = new PropertyMeta[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                defs[i] = new PropertyMeta(properties[i]);
            }

            PROPERTY_DEF_MAP.Add(type, defs);
            return defs;
        }

        public IParser BuildParser(PropertyMeta propertyDef)
        {
            try
            {
                if(null != propertyDef.Annotation.Parser)
                {
                    Activator.CreateInstance(propertyDef.Annotation.Parser);
                }

                Type type = propertyDef.PropertyClass;
                IParser parser = null;
                if(this.STRING_TYPE == type)
                {
                    parser = StandardParsers.STRING_PARSER;
                }
                else if(this.LONG_TYPE == type)
                {
                    parser = StandardParsers.LONG_PARSER;
                }
                else if(this.INTEGER_TYPE == type)
                {
                    parser = StandardParsers.INT_PARSER;
                }
                else if(this.DOUBLE_TYPE == type)
                {
                    parser = StandardParsers.DOUBLE_PARSER;
                }
                else if(this.DATE_TYPE == type)
                {
                    parser = StandardParsers.DATETIME_PARSER;
                }
                else if(this.BOOLEAN_TYPE == type)
                {
                    parser = StandardParsers.BOOL_PARSER;
                }
                else if(this.SHORT_TYPE == type)
                {
                    parser = StandardParsers.SHORT_PARSER;
                }
                else if(this.BYTE_TYPE == type)
                {
                    parser = StandardParsers.BYTE_PARSER;
                }
                else if(this.FLOAT_TYPE == type)
                {
                    parser = StandardParsers.FLOAT_PARSER;
                }
                else if(this.CHAR_TYPE == type)
                {
                    parser = StandardParsers.CHAR_PARSER;
                }
                else if(type.IsEnum)
                {
                    parser = StandardParsers.ENUM_PARSER;
                }

                return parser;
            }
            catch
            {
                throw new Exception("Unable to build parser -  property:" + propertyDef.Name + " of type:" + propertyDef.PropertyClass.Name);
            }
        }

        public object BuildList(Type type)
        {
            Type listAndSetType = typeof(ListAndSet<>);
            Type[] typeArgs = { type };
            Type makeme = listAndSetType.MakeGenericType(typeArgs);
            return Activator.CreateInstance(makeme);
        }
    }
}