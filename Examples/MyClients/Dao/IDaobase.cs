using System;
using NHibernate;

namespace MyClients.Dao
{
	public interface IDaobase<T>
	{
		void Save(T entity);
		void Update(T entity);
		void Merge(T entity);
		void Delete(T entity);
		T Get(object id);
		T Load(object id);
		ITransaction BeginTransaction();
	}
}