﻿using Iesi.Collections;
using Iesi.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;

namespace Upida
{
    public class JsonParser
    {
        public T Parse<T>(JToken node)
        {
            return (T)this.Parse(node, typeof(T));
        }

        public object Parse(JToken node, Type type)
        {
            try
            {
                if(!node.HasValues && string.IsNullOrEmpty(node.Value<string>()))
                {
                    return null;
                }

                Dtobase dto = (Dtobase)Activator.CreateInstance(type);
                PropertyMeta[] properties = UpidaContext.Current().GetPropertyDefs(type);
                for(int i = 0; i < properties.Length; i++)
                {
                    PropertyMeta propertyDef = properties[i];
                    if(!propertyDef.IsValid)
                    {
                        continue;
                    }

                    JToken propertyValue = node[propertyDef.Name];
                    if(null != propertyValue)
                    {
                        try
                        {
                            if(PropertyMeta.ClassType.Value == propertyDef.PropertyClassType)
                            {
                                propertyDef.Write(dto, this.parseValue(propertyValue, propertyDef));
                                dto.addAssignedField(propertyDef.Name);
                            }
                            else if(PropertyMeta.ClassType.Class == propertyDef.PropertyClassType ||
                                PropertyMeta.ClassType.CustomType == propertyDef.PropertyClassType)
                            {
                                propertyDef.Write(dto, this.Parse(propertyValue, propertyDef.PropertyClass));
                                dto.addAssignedField(propertyDef.Name);
                            }
                            else if(PropertyMeta.ClassType.Collection == propertyDef.PropertyClassType ||
                                PropertyMeta.ClassType.CustomTypeCollection == propertyDef.PropertyClassType)
                            {
                                IList list = (IList)UpidaContext.Current().BuildList(propertyDef.NestedType);
                                foreach(JToken item in propertyValue)
                                {
                                    list.Add(this.Parse(item, propertyDef.NestedType));
                                }

                                propertyDef.Write(dto, list);
                                dto.addAssignedField(propertyDef.Name);
                            }
                        }
                        catch
                        {
                            dto.addWrongField(propertyDef.Name);
                        }
                    }
                }

                return dto;
            }
            catch(Exception ex) {
                throw new Exception("parse exception: ", ex);
            }
        }

        private Object parseValue(JToken node, PropertyMeta propertyDef)
        {
            string text = node.Value<string>();
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            return propertyDef.Parser.parseTextValue(propertyDef.PropertyClass, text);
        }
    }
}