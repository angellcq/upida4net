using System;
using NHibernate;
using NHibernate.Context;

namespace UpidaExampleAngular.Dao.Support
{
	internal static class SessionFactoryExtension
	{
		public static ISession GetCurrent(this ISessionFactory sessionFactory)
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