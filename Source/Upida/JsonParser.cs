using Newtonsoft.Json.Linq;
using System;
using System.Collections;

namespace Upida
{
	public class JsonParser : IJsonParser
	{
		// <summary>
		/// Parses JSON data into domain object
		/// </summary>
		/// <typeparam name="T">domain object type</typeparam>
		/// <param name="form">JSON tree</param>
		/// <returns>parsed domain object</returns>
		public T Parse<T>(JToken node)
			where T : Dtobase
		{
			return (T)this.Parse(node, typeof(T));
		}

		public object Parse(JToken node, Type type)
		{
			try
			{
				if (!node.HasValues && string.IsNullOrEmpty(node.Value<string>()))
				{
					return null;
				}

				Dtobase dto = (Dtobase)Activator.CreateInstance(type);
				PropertyMeta[] properties = UpidaContext.Current().GetPropertyDefs(type);
				for (int i = 0; i < properties.Length; i++)
				{
					PropertyMeta propertyDef = properties[i];
					if (!propertyDef.IsValid)
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
								propertyDef.Write(dto, this.parseValue(propertyValue, propertyDef));
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
								IList list = (IList)UpidaContext.Current().BuildList(propertyDef.NestedType);
								foreach (JToken item in propertyValue)
								{
									list.Add(this.Parse(item, propertyDef.NestedType));
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
				throw new Exception("parse exception: ", ex);
			}
		}

		private object parseValue(JToken node, PropertyMeta propertyDef)
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