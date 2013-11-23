using System;
using System.Collections;
using System.Collections.Generic;
using iesi = Iesi.Collections;

namespace Upida
{
	public class ListAndSet<T> : List<T>, iesi.Generic.ISet<T>, iesi.ISet
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

		public iesi.Generic.ISet<T> ExclusiveOr(iesi.Generic.ISet<T> a)
		{
			throw new NotImplementedException();
		}

		public iesi.Generic.ISet<T> Intersect(iesi.Generic.ISet<T> a)
		{
			throw new NotImplementedException();
		}

		public bool IsEmpty
		{
			get { return 0 == base.Count; }
		}

		public iesi.Generic.ISet<T> Minus(iesi.Generic.ISet<T> a)
		{
			throw new NotImplementedException();
		}

		public bool RemoveAll(ICollection<T> c)
		{
			throw new NotImplementedException();
		}

		public bool RetainAll(ICollection<T> c)
		{
			throw new NotImplementedException();
		}

		public iesi.Generic.ISet<T> Union(iesi.Generic.ISet<T> a)
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

		public iesi.ISet ExclusiveOr(iesi.ISet a)
		{
			throw new NotImplementedException();
		}

		public iesi.ISet Intersect(iesi.ISet a)
		{
			throw new NotImplementedException();
		}

		public iesi.ISet Minus(iesi.ISet a)
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

		public iesi.ISet Union(iesi.ISet a)
		{
			throw new NotImplementedException();
		}
	}
}