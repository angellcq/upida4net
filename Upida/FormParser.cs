using System;
using System.Collections;
using System.Collections.Specialized;

namespace Upida
{
    public class FormParser : IFormParser
    {
        /// <summary>
        /// Parses HTTP POST key-value pairs into domain object
        /// </summary>
        /// <typeparam name="T">domain object type</typeparam>
        /// <param name="form">key-Value pair collection</param>
        /// <returns>parsed domain object</returns>
        public T Parse<T>(NameValueCollection form)
            where T : Dtobase
        {
            string[] keys = new string[form.Count];
            int index = 0;
            foreach(string key in form)
            {
                keys[index] = key;
                index++;
            }

            return (T)this.Parse("", keys, form, typeof(T));
        }

        private Dtobase Parse(string path, string[] keys, NameValueCollection form, Type type)
        {
            try
            {
                Dtobase dto = (Dtobase)Activator.CreateInstance(type);
                PropertyMeta[] properties = UpidaContext.Current().GetPropertyDefs(type);
                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyMeta propertyDef = properties[i];
                    if (!propertyDef.IsValid)
                    {
                        continue;
                    }

                    string propertyPath = path + propertyDef.Name;
                    string propertyValue = form[propertyPath];
                    try
                    {
                        if (PropertyMeta.ClassType.Value == propertyDef.PropertyClassType)
                        {
                            if (null != propertyValue)
                            {
                                dto.AddAssignedField(propertyDef.Name);
                                propertyDef.Write(dto, this.ParseValue(propertyValue, propertyDef));
                            }
                        }
                        else if (PropertyMeta.ClassType.Class == propertyDef.PropertyClassType || PropertyMeta.ClassType.CustomType == propertyDef.PropertyClassType)
                        {
                            if (this.HasChildren(propertyPath + ".", keys))
                            {
                                dto.AddAssignedField(propertyDef.Name);
                                Dtobase obj = (Dtobase)this.Parse(propertyPath + ".", keys, form, propertyDef.PropertyClass);
                                propertyDef.Write(dto, obj);
                            }
                        }
                        else if (PropertyMeta.ClassType.Collection == propertyDef.PropertyClassType ||
                                PropertyMeta.ClassType.CustomTypeCollection == propertyDef.PropertyClassType)
                        {

                            if (propertyDef.Annotation.HasNested &&
                                this.HasChildren(propertyPath + "[", keys))
                            {
                                IList list = (IList)UpidaContext.Current().BuildList(propertyDef.NestedType);
                                int index = 0, attempt = 0;
                                while (true)
                                {
                                    String elementPath = propertyPath + "[" + index + "].";
                                    if (this.HasChildren(elementPath, keys))
                                    {
                                        Dtobase obj = (Dtobase)this.Parse(elementPath, keys, form, propertyDef.NestedType);
                                        list.Add(obj);
                                        attempt = 0;
                                    }
                                    else
                                    {
                                        attempt++;
                                    }

                                    index++;
                                    if (attempt >= 10)
                                    {
                                        break;
                                    }
                                }

                                if (list.Count > 0)
                                {
                                    propertyDef.Write(dto, list);
                                    dto.AddAssignedField(propertyDef.Name);
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        dto.AddWrongField(propertyDef.Name);
                    }
                }

                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("parse exception: ", ex);
            }
        }

        private object ParseValue(String text, PropertyMeta propertyDef)
        {
            if(string.IsNullOrEmpty(text)) {
                return null;
            }

            return propertyDef.Parser.ParseTextValue(propertyDef.PropertyClass, text);
        }

        private bool HasChildren(string prefix, string[] keys)
        {
            for(int i=0; i<keys.Length; i++) {
                if(null != keys[i] && keys[i].StartsWith(prefix)) {
                    return true;
                }
            }

            return false;
        }
    }
}