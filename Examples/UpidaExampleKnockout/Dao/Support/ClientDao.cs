using NHibernate;
using System.Collections.Generic;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Dao.Support
{
    public class ClientDao : Daobase<Client>, IClientDao
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