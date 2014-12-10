using NHibernate;

namespace MyClients.Database
{
    public interface ISessionFactoryEx
    {
        ISession GetCurrentSession();
    }
}