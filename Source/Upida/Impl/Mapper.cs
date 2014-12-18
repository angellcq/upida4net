using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Upida.Impl
{
    /// <summary>
    /// Represents Mapping and Filtering helper class
    /// </summary>
    public class Mapper : IMapper
    {
        private static Type LIST_TYPE = typeof(IList);
        private readonly IUpidaContext context;

        /// <summary>
        /// Initializes new instance of the Mapper class
        /// </summary>
        /// <param name="context"></param>
        public Mapper(IUpidaContext context)
        {
            this.context = context;
        }

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
        /// <typeparam name="T">Type of the source and dest objects (Must derive from Dtobase)</typeparam>
        /// <param name="source">Incoming collection of domain objects</param>
        /// <param name="dest">Persistent collection (ISet or IList)</param>
        public void MapToCollection<T>(IEnumerable<T> source, IEnumerable<T> dest)
            where T : Dtobase
        {
            this.MapToCollection(source, dest, null);
        }

        /// <summary>
        /// Recursively goes through fields of incoming domain object and assigns parents to nested objects
        /// </summary>
        /// <typeparam name="T">Type of the source object (Must derive from Dtobase)</typeparam>
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

        /// <summary>
        /// Recursively copies data from the incoming domain object list to the outgoing one, taking serializations levels into account
        /// </summary>
        /// <typeparam name="T">type of the domain object</typeparam>
        /// <param name="items">incoming domain object list</param>
        /// <param name="level">serialization level</param>
        /// <returns>outgoing domain object list</returns>
        public IList<T> FilterList<T>(IList<T> items, byte level)
            where T : Dtobase
        {
            return (IList<T>)this.FilterList(items, typeof(T), level);
        }

        /// <summary>
        /// Recursively copies data from the incoming domain object to the outgoing one, taking serializations levels into account
        /// </summary>
        /// <typeparam name="T">type of the domain object</typeparam>
        /// <param name="item">incoming domain object</param>
        /// <param name="level">serialization level</param>
        /// <returns>outgoing domain object</returns>
        public T Filter<T>(T item, byte level)
            where T : Dtobase
        {
            return (T)this.Filter(item, typeof(T), level);
        }

        private void MapTo(Dtobase source, Dtobase dest, Type type)
        {
            if (null == source) { return; }

            try
            {
                PropertyMeta[] properties = this.context.GetPropertyDefs(type);
                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyMeta property = properties[i];
                    if (!property.Valid || !property.IsAssigned(source))
                    {
                        continue;
                    }

                    Object sourceValue = property.Read(source);
                    Object destValue = property.Read(dest);

                    if (null == sourceValue ||
                        null == destValue ||
                        PropertyMeta.ClassType.Value == property.PropertyClassType ||
                        PropertyMeta.ClassType.CustomType == property.PropertyClassType ||
                        PropertyMeta.ClassType.CustomTypeCollection == property.PropertyClassType)
                    {
                        property.Write(dest, sourceValue);
                    }
                    else if (PropertyMeta.ClassType.Class == property.PropertyClassType)
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
                    else if (PropertyMeta.ClassType.Collection == property.PropertyClassType)
                    {
                        IEnumerable destList = (IEnumerable)destValue;
                        IEnumerable sourceList = (IEnumerable)sourceValue;
                        this.MapToCollection(sourceList, destList, source);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Mapping failed from '" + source.GetType().Name +
                    "' to '" + dest.GetType().Name + "'", ex);
            }
        }

        private void MapToCollection(IEnumerable sourceCollection, IEnumerable destCollection, Dtobase parent)
        {
            List<Dtobase> destItems = new List<Dtobase>();
            bool firstIteration = true;
            bool child = false;
            foreach (Dtobase item in destCollection)
            {
                if (firstIteration)
                {
                    firstIteration = false;
                    child = item is IChild;
                }

                destItems.Add(item);
            }

            IList destList = null;
            IEnumerable destHashSet = null;
            MethodInfo hashSetAdd = null;
            MethodInfo hashSetClear = null;
            Type destCollectionType = destCollection.GetType();
            if (LIST_TYPE.IsAssignableFrom(destCollectionType))
            {
                destList = (IList)destCollection;
                destList.Clear();
            }
            else // Treat as ISet<?>
            {
                destHashSet = (IEnumerable)destCollection;
                Type hashSetType = destCollection.GetType();
                hashSetAdd = hashSetType.GetMethod("Add");
                hashSetClear = hashSetType.GetMethod("Clear");
                hashSetClear.Invoke(destHashSet, null);
            }

            List<Dtobase> sourceItems = new List<Dtobase>();
            foreach (Dtobase item in sourceCollection)
            {
                sourceItems.Add(item);
                Dtobase matchedDestItem = null;
                foreach (Dtobase destItem in destItems)
                {
                    if (object.Equals(destItem, item))
                    {
                        matchedDestItem = destItem;
                        break;
                    }
                }

                if (null != matchedDestItem)
                {
                    this.MapTo(item, matchedDestItem, item.GetType());
                    if (null != destList)
                    {
                        destList.Add(matchedDestItem);
                    }
                    else
                    {
                        hashSetAdd.Invoke(destHashSet, new object[] { matchedDestItem });
                    }
                }
                else
                {
                    if (null != destList)
                    {
                        destList.Add(item);
                    }
                    else
                    {
                        hashSetAdd.Invoke(destHashSet, new object[] { item });
                    }

                    if (child)
                    {
                        ((IChild)item).ConnectToParent(parent);
                    }
                }
            }

            if (sourceItems.Count < destItems.Count)
            {
                foreach (Dtobase original in destItems)
                {
                    bool itemDeleted = true;
                    foreach (Dtobase updated in sourceItems)
                    {
                        if (object.Equals(original, updated))
                        {
                            itemDeleted = false;
                        }
                    }
                }
            }
        }

        private void Map(Dtobase source, Type type)
        {
            PropertyMeta property = null;
            try
            {
                PropertyMeta[] properties = this.context.GetPropertyDefs(type);
                for (int i = 0; i < properties.Length; i++)
                {
                    property = properties[i];
                    if (!property.Valid)
                    {
                        continue;
                    }

                    object sourceValue = property.Read(source);
                    if (null != sourceValue)
                    {
                        if (PropertyMeta.ClassType.Class == property.PropertyClassType)
                        {
                            this.Map((Dtobase)sourceValue, property.PropertyClass);
                        }
                        else if (PropertyMeta.ClassType.Collection == property.PropertyClassType)
                        {
                            IEnumerable sourceSet = (IEnumerable)sourceValue;
                            foreach (object item in sourceSet)
                            {
                                this.Map((Dtobase)item, property.InnerGenericClass);
                                if (item is IChild)
                                {
                                    (item as IChild).ConnectToParent(source);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Mapping failed. Type:'" + source.GetType().Name + "'. Name:'" + property.Name + "'", ex);
            }
        }

        private IList FilterList(IEnumerable items, Type type, byte level)
        {
            if (null == items)
            {
                return null;
            }

            IList list = (IList)this.context.BuildList(type);

            foreach (Dtobase item in items)
            {
                Dtobase dto = this.Filter(item, type, level);
                list.Add(dto);
            }

            return list;
        }

        private Dtobase Filter(Dtobase item, Type type, byte level)
        {
            try
            {
                if (null == item)
                {
                    return null;
                }

                Dtobase dto = (Dtobase)Activator.CreateInstance(type);
                PropertyMeta[] properties = this.context.GetPropertyDefs(type);
                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyMeta property = properties[i];
                    if (!property.Valid || !property.HasLevel(level))
                    {
                        continue;
                    }

                    Object value = property.Read(item);
                    if (PropertyMeta.ClassType.Value == property.PropertyClassType ||
                        PropertyMeta.ClassType.CustomType == property.PropertyClassType ||
                        PropertyMeta.ClassType.CustomTypeCollection == property.PropertyClassType)
                    {
                        property.Write(dto, value);
                    }
                    else if (PropertyMeta.ClassType.Class == property.PropertyClassType)
                    {
                        byte nestedLevel = property.DtoNestedLevel;
                        if (level != property.DtoLevel)
                        {
                            nestedLevel = (byte)(nestedLevel + level - property.DtoLevel);
                        }

                        property.Write(dto,
                            this.Filter((Dtobase)value, property.PropertyClass, nestedLevel));

                    }
                    else if (PropertyMeta.ClassType.Collection == property.PropertyClassType)
                    {
                        byte nestedLevel = property.DtoNestedLevel;
                        if (level != property.DtoLevel)
                        {
                            nestedLevel = (byte)(nestedLevel + level - property.DtoLevel);
                        }

                        property.Write(dto,
                            this.FilterList((IEnumerable)value, property.InnerGenericClass, nestedLevel));
                    }
                }

                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Filtering failed. Type:'" + item.GetType().Name +
                    "'. Level:'" + level + "'", ex);
            }
        }
    }
}