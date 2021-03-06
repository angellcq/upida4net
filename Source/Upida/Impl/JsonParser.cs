﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections;

namespace Upida.Impl
{
    /// <summary>
    /// Represents JSON parsing utility
    /// </summary>
    public class JsonParser : IJsonParser
    {
        private IUpidaContext context;

        /// <summary>
        /// Initializes new instance of the JsonParser class
        /// </summary>
        /// <param name="context"></param>
        public JsonParser(IUpidaContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Parses JSON data into domain object
        /// </summary>
        /// <typeparam name="T">domain object type</typeparam>
        /// <param name="node">JSON tree</param>
        /// <returns>parsed domain object instance</returns>
        public T Parse<T>(JToken node)
            where T : Dtobase
        {
            return (T)this.Parse(node, typeof(T));
        }

        /// <summary>
        /// Parses JSON data into domain object
        /// </summary>
        /// <param name="node">JSON tree</param>
        /// <param name="type">domain object type</param>
        /// <returns>parsed domain object instance</returns>
        public object Parse(JToken node, Type type)
        {
            try
            {
                Dtobase dto = (Dtobase)Activator.CreateInstance(type);
                if (!node.HasValues)
                {
                    return dto;
                }

                PropertyMeta[] properties = this.context.GetPropertyDefs(type);
                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyMeta propertyDef = properties[i];
                    if (!propertyDef.Valid)
                    {
                        continue;
                    }

                    JToken propertyValue = node[propertyDef.Name];
                    if (null != propertyValue)
                    {
                        try
                        {
                            if (PropertyMeta.ClassType.Value == propertyDef.PropertyClassType)
                            {
                                dto.AddAssignedField(propertyDef.Name);
                                propertyDef.Write(dto, this.ParseValue(propertyValue, propertyDef));
                            }
                            else if (PropertyMeta.ClassType.Class == propertyDef.PropertyClassType ||
                                PropertyMeta.ClassType.CustomType == propertyDef.PropertyClassType)
                            {
                                dto.AddAssignedField(propertyDef.Name);
                                propertyDef.Write(dto, this.Parse(propertyValue, propertyDef.PropertyClass));
                            }
                            else if (PropertyMeta.ClassType.Collection == propertyDef.PropertyClassType ||
                                PropertyMeta.ClassType.CustomTypeCollection == propertyDef.PropertyClassType)
                            {
                                IList list = (IList)this.context.BuildList(propertyDef.InnerGenericClass);
                                foreach (JToken item in propertyValue)
                                {
                                    list.Add(this.Parse(item, propertyDef.InnerGenericClass));
                                }

                                propertyDef.Write(dto, list);
                                dto.AddAssignedField(propertyDef.Name);
                            }
                        }
                        catch
                        {
                            dto.AddWrongField(propertyDef.Name);
                        }
                    }
                }

                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("JSON parsing failed", ex);
            }
        }

        /// <summary>
        /// Parses JSON data into a list of domain objects
        /// </summary>
        /// <param name="node">JSON tree</param>
        /// <param name="type">domain object type</param>
        /// <returns>parsed domain object instance</returns>
        public IEnumerable ParseList(JToken node, Type type)
        {
            IList list = (IList)this.context.BuildList(type);
            foreach (JToken item in node.Children())
            {
                list.Add(this.Parse(item, type));
            }

            return list;
        }

        private object ParseValue(JToken node, PropertyMeta propertyDef)
        {
            string text = node.Value<string>();
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            return propertyDef.Parser.ParseTextValue(propertyDef.PropertyClass, text);
        }
    }
}