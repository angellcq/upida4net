using System;

namespace UpidaExampleAngularEF.Dao.Support
{
	public class Daobase<T> : IDaobase<T>
		where T : class
	{
		private readonly MyContextFactory sessionFactory;

		public Daobase(MyContextFactory sessionFactory)
		{
			this.sessionFactory = sessionFactory;
		}

		public MyContext Session
		{
			get
			{
				return this.sessionFactory.GetCurrentSession();
			}
		}
	}
}