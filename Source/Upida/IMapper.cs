﻿using System;
using System.Collections.Generic;

namespace Upida
{
	public interface IMapper
	{
		/// <summary>
		/// Recursively copies fields from incoming source object to persistent dest object.
		/// </summary>
		/// <param name="source">incoming source object must be Dtobase derived</param>
		/// <param name="dest">persistent dets object must be Dtobase derived</param>
		void MapTo<T>(T source, T dest) where T : Dtobase;

		/// <summary>
		/// Recursively copies fields from incoming collection of domain objects to the persistent collection
		/// </summary>
		/// <param name="type">Type of the source and dest object</param>
		/// <param name="sourceList">Incoming collection of domain objects</param>
		/// <param name="destSet">Persistent collection (ISet or IList)</param>
		void MapToCollection<T>(IEnumerable<T> source, IEnumerable<T> dest) where T : Dtobase;

		/// <summary>
		/// Recursively goes through fields of incoming domain object and assigns parents to nested objects
		/// </summary>
		/// <typeparam name="T">must derive from Dtobase</typeparam>
		/// <param name="source">incoming domain object</param>
		void Map<T>(T source) where T : Dtobase;

		/// <summary>
		/// Recursively copies data from the incoming domain object list to the outgoing one, taking serializations levels into account
		/// If a property does not conform to the requested serialization level - it will be assigned NULL
		/// </summary>
		/// <typeparam name="T">must derive from Dtobase</typeparam>
		/// <param name="items">incoming domain object list</param>
		/// <param name="level">serialization level</param>
		/// <returns>outgoing domain object list</returns>
		IList<T> FilterList<T>(IList<T> items, byte level) where T : Dtobase;

		/// <summary>
		/// Recursively copies data from the incoming domain object to the outgoing one, taking serializations levels into account
		/// If a property does not conform to the requested serialization level - it will be assigned NULL
		/// </summary>
		/// <typeparam name="T">must derive from Dtobase</typeparam>
		/// <param name="item">incoming domain object</param>
		/// <param name="level">serialization level</param>
		/// <returns>outgoing domain object</returns>
		T Filter<T>(T item, byte level) where T : Dtobase;
	}
}