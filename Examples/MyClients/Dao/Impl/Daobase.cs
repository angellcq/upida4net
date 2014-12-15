using MyClients.Database;
using NHibernate;

namespace MyClients.Dao.Impl
{
    public class Daobase<T> : IDaobase<T>
        where T : class
    {
        public ISessionFactoryEx SessionFactory { get; set; }

        public void Save(T entity)
        {
            this.SessionFactory
                .GetCurrentSession()
                .Save(entity);
        }

        public void Update(T entity)
        {
            this.SessionFactory
                .GetCurrentSession()
                .Update(entity);
        }

        public void Merge(T entity)
        {
            this.SessionFactory
                .GetCurrentSession()
                .Merge<T>(entity);
        }

        public void Delete(T entity)
        {
            this.SessionFactory
                .GetCurrentSession()
                .Delete(entity);
        }

        public T Get(object id)
        {
            return this.SessionFactory
                .GetCurrentSession()
                .Get<T>(id);
        }

        public T Load(object id)
        {
            return this.SessionFactory
                .GetCurrentSession()
                .Load<T>(id);
        }

        public ITransaction BeginTransaction()
        {
            return this.SessionFactory
                .GetCurrentSession()
                .BeginTransaction();
        }
    }
}