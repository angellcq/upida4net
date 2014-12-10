using NHibernate;
using NHibernate.Context;

namespace MyClients.Database.Impl
{
    public class SessionFactoryEx : ISessionFactoryEx
    {
        private ISessionFactory sessionFactory;

        public SessionFactoryEx(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public ISession GetCurrentSession()
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