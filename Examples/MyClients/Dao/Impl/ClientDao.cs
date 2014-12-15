using System.Collections.Generic;
using MyClients.Domain;
using NHibernate.Transform;

namespace MyClients.Dao.Impl
{
    public class ClientDao : Daobase<Client>, IClientDao
    {
        public Client GetById(int id)
        {
            return this.SessionFactory
                .GetCurrentSession()
                .CreateQuery("from Client client left outer join fetch client.Logins where client.Id = :id")
                .SetParameter<int>("id", id)
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .UniqueResult<Client>();
        }

        public IList<Client> GetAll()
        {
            return this.SessionFactory
                .GetCurrentSession()
                .CreateQuery("from Client client left outer join fetch client.Logins")
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .List<Client>();
        }

        public long GetCount()
        {
            return this.SessionFactory
                .GetCurrentSession()
                .CreateQuery("select count(*) from Client")
                .UniqueResult<long>();
        }
    }
}