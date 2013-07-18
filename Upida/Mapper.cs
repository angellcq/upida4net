using Iesi.Collections;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Upida
{
    public class Mapper
    {
        private static Type SET_TYPE = typeof(ISet);
        private static Type LIST_TYPE = typeof(IList);

        /// <summary>
        /// Recursively copies fields from incoming source object to persistent dest object.
        /// </summary>
        /// <param name="source">Incoming source object must be Dtobase derived</param>
        /// <param name="dest">Persistent dets object must be Dtobase derived</param>
        public void MapTo(Dtobase source, Dtobase dest)
        {
            if(null == source) { return; }
            try
            {
                PropertyMeta[] properties = UpidaContext.Current().GetPropertyDefs(source.GetType());
                for(int i = 0; i < properties.Length; i++)
                {
                    PropertyMeta property = properties[i];
                    if(!property.IsValid || !property.isAssigned(source))
                    {
                        continue;
                    }

                    Object sourceValue = property.Read(source);
                    Object destValue = property.Read(dest);

                    if(null == sourceValue ||
                        null == destValue ||
                        PropertyMeta.ClassType.Value == property.PropertyClassType ||
                        PropertyMeta.ClassType.CustomType == property.PropertyClassType ||
                        PropertyMeta.ClassType.CustomTypeCollection == property.PropertyClassType)
                    {
                        property.Write(dest, sourceValue);
                    }
                    else if(PropertyMeta.ClassType.Class == property.PropertyClassType)
                    {
                        this.MapTo((Dtobase)sourceValue, (Dtobase)destValue);
                    }
                    else if(PropertyMeta.ClassType.Collection == property.PropertyClassType)
                    {
                        IEnumerable destList = (IEnumerable)destValue;
                        IEnumerable sourceList = (IEnumerable)sourceValue;
                        this.MapTo(sourceList, destList, source);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Map error", ex);
            }
        }

        /// <summary>
        /// Recursively copies fields from incoming collection of domain objects to the persistent collection
        /// </summary>
        /// <param name="sourceList">Incoming collection of domain objects</param>
        /// <param name="destSet">Persistent collection (ISet or IList)</param>
        /// <param name="parent"></param>
        public void MapTo(IEnumerable sourceCollection, IEnumerable destCollection, Dtobase parent)
        {
            List<Dtobase> destItems = new List<Dtobase>();
            foreach (Dtobase item in destCollection)
            {
                destItems.Add(item);
            }

            IList destList = null;
            ISet destSet = null;
            Type destCollectionType = destCollection.GetType();
            if (SET_TYPE.IsAssignableFrom(destCollectionType))
            {
                destSet = (ISet)destCollection;
                destSet.Clear();
            }
            else if (LIST_TYPE.IsAssignableFrom(destCollectionType))
            {
                destList = (IList)destCollection;
                destList.Clear();
            }
            else
            {
                throw new ApplicationException("Collection is neither IList nor ISet: " + destCollectionType.FullName);
            }

            foreach (Dtobase item in sourceCollection)
            {
                Dtobase matchedDestItem = null;
                foreach (Dtobase destItem in destItems)
                {
                    if(object.Equals(destItem, item))
                    {
                        matchedDestItem = destItem;
                        break;
                    }
                }

                if(null != matchedDestItem)
                {
                    this.MapTo(item, matchedDestItem);
                    if (null != destSet)
                    {
                        destSet.Add(matchedDestItem);
                    }
                    else if (null != destList)
                    {
                        destList.Add(matchedDestItem);
                    }
                }
                else
                {
                    if (null != destSet)
                    {
                        destSet.Add(item);
                    }
                    else if (null != destList)
                    {
                        destList.Add(item);
                    }

                    if(item is IChild)
                    {
                        ((IChild)item).connectToParent(parent);
                    }
                }
            }
        }

        /// <summary>
        /// Recursively goes through fields of incoming domain object and assigns parents to nested objects
        /// </summary>
        /// <typeparam name="T">Must derive from Dtobase</typeparam>
        /// <param name="source">Incoming domain object</param>
        public void Map<T>(T source)
            where T : Dtobase
        {
            this.Map(source, typeof(T));
        }

        /// <summary>
        /// Recursively goes through fields of incoming domain object and assigns parents to nested objects
        /// </summary>
        /// <typeparam name="T">Must derive from Dtobase</typeparam>
        /// <param name="source">Incoming domain object</param>
        /// <param name="type">Type of the incoming domain object</param>
        public void Map(Dtobase source, Type type)
        {
            try
            {
                PropertyMeta[] properties = UpidaContext.Current().GetPropertyDefs(type);
                for(int i = 0; i < properties.Length; i++)
                {
                    PropertyMeta property = properties[i];
                    if(!property.IsValid)
                    {
                        continue;
                    }

                    Object sourceValue = property.Read(source);
                    if(null != sourceValue)
                    {
                        if(PropertyMeta.ClassType.Class == property.PropertyClassType)
                        {
                            this.Map((Dtobase)sourceValue, property.PropertyClass);
                        }
                        else if(PropertyMeta.ClassType.Collection == property.PropertyClassType)
                        {
                            IEnumerable sourceSet = (IEnumerable)sourceValue;
                            foreach(Object item in sourceSet)
                            {
                                if(item is IChild)
                                {
                                    ((IChild) item).connectToParent(source);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("map error", ex);
            }
        }

        public IList<T> OutList<T>(IList<T> items, byte rule)
            where T : Dtobase
        {
            return (IList<T>)this.OutList(items, typeof(T), rule);
        }

        public T Out<T>(T item, byte rule)
            where T : Dtobase
        {
            return (T)this.Out(item, typeof(T), rule);
        }

        private IList OutList(IEnumerable items, Type type, byte rule)
        {
            IList list = (IList)UpidaContext.Current().BuildList(type);

            foreach (Dtobase item in items)
            {
                Dtobase dto = this.Out(item, type, rule);
                list.Add(dto);
            }

            return list;
        }

        private Dtobase Out(Dtobase item, Type type, byte rule)
        {
            try
            {
                if(null == item) {
                    return null;
                }

                Dtobase dto = (Dtobase)Activator.CreateInstance(type);
                PropertyMeta[] properties = UpidaContext.Current().GetPropertyDefs(type);
                for(int i = 0; i < properties.Length; i++)
                {
                    PropertyMeta property = properties[i];
                    if(!property.IsValid || !property.HasRule(rule))
                    {
                        continue;
                    }

                    Object value = property.Read(item);
                    if(PropertyMeta.ClassType.Value == property.PropertyClassType ||
                        PropertyMeta.ClassType.CustomType == property.PropertyClassType ||
                        PropertyMeta.ClassType.CustomTypeCollection == property.PropertyClassType)
                    {
                        property.Write(dto, value);
                    }
                    else if(PropertyMeta.ClassType.Class == property.PropertyClassType)
                    {
                        property.Write(dto,
                            this.Out((Dtobase)value, property.PropertyClass, property.Annotation.Nested));

                    }
                    else if(PropertyMeta.ClassType.Collection ==  property.PropertyClassType)
                    {
                        property.Write(dto,
                            this.OutList((IEnumerable)value, property.NestedType, property.Annotation.Nested));
                    }
                }

                return dto;
            }
            catch(Exception ex)
            {
                throw new Exception("Serialize error", ex);
            }
        }
    }
}