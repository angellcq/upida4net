﻿using NHibernate;
using NHibernate.Context;
using System;

namespace UpidaExampleKnockout.Dao.Support
{
	public class Daobase<T> : IDaobase<T>
		where T : class
	{
		private readonly ISessionFactory sessionFactory;

		public Daobase(ISessionFactory sessionFactory)
		{
			this.sessionFactory = sessionFactory;
		}

		protected ISession Session
		{
			get
			{
				return this.sessionFactory.GetCurrent();
			}
		}

		public void Save(T entity)
		{
			this.Session.Save(entity);
		}

		public void Update(T entity)
		{
			this.Session.Update(entity);
		}

		public void Merge(T entity)
		{
			this.Session.Merge<T>(entity);
		}

		public void Delete(T entity)
		{
			this.Session.Delete(entity);
		}

		public T Get(object id)
		{
			return this.Session.Get<T>(id);
		}

		public T Load(object id)
		{
			return this.Session.Load<T>(id);
		}

		public ITransaction BeginTransaction()
		{
			return this.Session.BeginTransaction();
		}
	}
}