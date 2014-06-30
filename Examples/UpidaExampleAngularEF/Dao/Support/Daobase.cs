using System;
using UpidaExampleAngularEF.Domain;

namespace UpidaExampleAngularEF.Dao.Support
{
	public class Daobase<T> : IDaobase<T>
		where T : class
	{
		private readonly DbSessionFactory sessionFactory;

		public Daobase(DbSessionFactory sessionFactory)
		{
			this.sessionFactory = sessionFactory;
		}

		public DbSession Session
		{
			get
			{
				return this.sessionFactory.GetCurrentSession();
			}
		}

		public void SaveChanges()
		{
			this.Session.SaveChanges();
		}
	}
}