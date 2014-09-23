using System;
using System.Collections;
using System.Collections.Generic;

namespace Upida
{
	public class ListAndSet<T> : List<T>, ISet<T>
	{
		public ListAndSet()
		{
		}

		public ListAndSet(int initialCapacity)
			: base(initialCapacity)
		{
		}

		public ListAndSet(IEnumerable<T> collection)
			: base(collection)
		{
		}

		public new bool Add(T o)
		{
			base.Add(o);
			return true;
		}

		public bool AddAll(ICollection<T> c)
		{
			throw new NotImplementedException();
		}

		public bool ContainsAll(ICollection<T> c)
		{
			throw new NotImplementedException();
		}

		public bool IsEmpty
		{
			get { return 0 == base.Count; }
		}

		public bool RemoveAll(ICollection<T> c)
		{
			throw new NotImplementedException();
		}

		public bool RetainAll(ICollection<T> c)
		{
			throw new NotImplementedException();
		}

		public object Clone()
		{
			throw new NotImplementedException();
		}

		public bool Add(object o)
		{
			base.Add((T)o);
			return true;
		}

		public bool AddAll(ICollection c)
		{
			throw new NotImplementedException();
		}

		public bool Contains(object o)
		{
			return base.Contains((T)o);
		}

		public bool ContainsAll(ICollection c)
		{
			throw new NotImplementedException();
		}

		public bool Remove(object o)
		{
			return base.Remove((T)o);
		}

		public bool RemoveAll(ICollection c)
		{
			throw new NotImplementedException();
		}

		public bool RetainAll(ICollection c)
		{
			throw new NotImplementedException();
		}


		public void ExceptWith(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		public void IntersectWith(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		public bool IsSubsetOf(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		public bool IsSupersetOf(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		public bool Overlaps(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		public bool SetEquals(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}

		public void UnionWith(IEnumerable<T> other)
		{
			throw new NotImplementedException();
		}
	}
}