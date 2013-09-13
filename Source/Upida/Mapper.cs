using Iesi.Collections;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Upida
{
    public class Mapper : IMapper
    {
        private static Type SET_TYPE = typeof(ISet);
        private static Type LIST_TYPE = typeof(IList);

        /// <summary>
        /// Recursively copies fields from incoming source object to persistent dest object.
        /// </summary>
        /// <typeparam name="T">Must derive from Dtobase</typeparam>
        /// <param name="source">Incoming source object must be Dtobase derived</param>
        /// <param name="dest">Persistent dets object must be Dtobase derived</param>
        public void MapTo<T>(T source, T dest)
            where T : Dtobase
        {
            this.MapTo(source, dest, typeof(T));
        }

        /// <summary>
        /// Recursively copies fields from incoming collection of domain objects to the persistent collection
        /// </summary>
        /// <param name="type">Type of the source and dest object</param>
        /// <param name="sourceList">Incoming collection of domain objects</param>
        /// <param name="destSet">Persistent collection (ISet or IList)</param>
        public void MapToCollection<T>(IEnumerable<T> source, IEnumerable<T> dest)
            where T : Dtobase
        {
            this.MapToCollection(source, dest, null);
        }

        private void MapTo(Dtobase source, Dtobase dest, Type type)
        {
            if(null == source) { return; }

            try
            {
                PropertyMeta[] properties = UpidaContext.Current().GetPropertyDefs(type);
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
                        if (object.Equals(sourceValue, destValue))
                        {
                            this.MapTo((Dtobase)sourceValue, (Dtobase)destValue, sourceValue.GetType());
                        }
                        else
                        {
                            property.Write(dest, sourceValue);
                        }
                    }
                    else if(PropertyMeta.ClassType.Collection == property.PropertyClassType)
                    {
                        IEnumerable destList = (IEnumerable)destValue;
                        IEnumerable sourceList = (IEnumerable)sourceValue;
                        this.MapToCollection(sourceList, destList, source);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Map error", ex);
            }
        }

        private void MapToCollection(IEnumerable sourceCollection, IEnumerable destCollection, Dtobase parent)
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
                throw new ApplicationException("Collection is neither IList nor iesi.ISet: " + destCollectionType.FullName);
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
                    this.MapTo(item, matchedDestItem, item.GetType());
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
                        ((IChild)item).ConnectToParent(parent);
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
        /// Recursively goes through fields of incoming domain object collection and assigns parents to nested objects
        /// </summary>
        /// <typeparam name="T">Must derive from Dtobase</typeparam>
        /// <param name="source">Incoming domain object collection</param>
        public void MapCollection<T>(IEnumerable<T> source)
            where T : Dtobase
        {
            foreach (T item in source)
            {
                this.Map<T>(item);
            }
        }

        private void Map(Dtobase source, Type type)
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
                                this.Map((Dtobase)item, property.NestedType);
                                if(item is IChild)
                                {
                                    (item as IChild).ConnectToParent(source);
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

        /// <summary>
        /// Recursively copies data from the incoming domain object list to the outgoing one, taking serializations levels into account
        /// </summary>
        /// <typeparam name="T">type of the domain object</typeparam>
        /// <param name="items">incoming domain object list</param>
        /// <param name="level">serialization level</param>
        /// <returns>outgoing domain object list</returns>
        public IList<T> OutList<T>(IList<T> items, byte level)
            where T : Dtobase
        {
            return (IList<T>)this.OutList(items, typeof(T), level);
        }

        /// <summary>
        /// Recursively copies data from the incoming domain object to the outgoing one, taking serializations levels into account
        /// </summary>
        /// <typeparam name="T">type of the domain object</typeparam>
        /// <param name="item">incoming domain object</param>
        /// <param name="level">serialization level</param>
        /// <returns>outgoing domain object</returns>
        public T Out<T>(T item, byte level)
            where T : Dtobase
        {
            return (T)this.Out(item, typeof(T), level);
        }

        private IList OutList(IEnumerable items, Type type, byte level)
        {
            IList list = (IList)UpidaContext.Current().BuildList(type);

            foreach (Dtobase item in items)
            {
                Dtobase dto = this.Out(item, type, level);
                list.Add(dto);
            }

            return list;
        }

        private Dtobase Out(Dtobase item, Type type, byte level)
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
                    if(!property.IsValid || !property.HasLevel(level))
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
                        byte nestedLevel = property.Annotation.Nested;
                        if (level != property.Annotation.Value)
                        {
                            nestedLevel = (byte)(nestedLevel + level - property.Annotation.Value);
                        }

                        property.Write(dto,
                            this.Out((Dtobase)value, property.PropertyClass, nestedLevel));

                    }
                    else if(PropertyMeta.ClassType.Collection ==  property.PropertyClassType)
                    {
                        byte nestedLevel = property.Annotation.Nested;
                        if (level != property.Annotation.Value)
                        {
                            nestedLevel = (byte)(nestedLevel + level - property.Annotation.Value);
                        }

                        property.Write(dto,
                            this.OutList((IEnumerable)value, property.NestedType, nestedLevel));
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