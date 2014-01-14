using NHibernate;
using NHibernate.Context;

namespace UpidaExampleKnockout.Dao.Support
{
	public class SessionFactoryExt
	{
		private ISessionFactory sessionFactory;

		public SessionFactoryExt(ISessionFactory sessionFactory)
		{
			this.sessionFactory = sessionFactory;
		}

		public virtual ISession GetCurrentSession()
		{
			ISession session = null;
			if (CurrentSessionContext.HasBind(sessionFactory))
			{
				session = sessionFactory.GetCurrentSession();
			}
			else
			{
				session = sessionFactory.OpenSession();
				CurrentSessionContext.Bind(session);
			}

			return session;
		}
	}
}