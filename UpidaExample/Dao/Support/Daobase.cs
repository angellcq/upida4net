using NHibernate;
using NHibernate.Context;
using System;

namespace UpidaExample.Dao.Support
{
    public class DaoBase<T> : IDaoBase<T>
        where T : class
    {
        private readonly ISessionFactory sessionFactory;

        public DaoBase(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        protected ISession Session
        {
            get
            {
                ISession session;
                if (CurrentSessionContext.HasBind(this.sessionFactory))
                {
                    session = this.sessionFactory.GetCurrentSession();
                }
                else
                {
                    session = this.sessionFactory.OpenSession();
                    CurrentSessionContext.Bind(session);
                }

                return session;
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
    }
}