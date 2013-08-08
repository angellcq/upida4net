using NHibernate;
using System.Collections.Generic;
using UpidaExampleStraight.Domain;

namespace UpidaExampleStraight.Dao.Support
{
    public class ClientDao : DaoBase<Client>, IClientDao
    {
        public ClientDao(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {
        }

        public IList<Client> GetAll()
        {
            return this.Session
                .CreateCriteria<Client>()
                .List<Client>();
        }
    }
}