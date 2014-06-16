using System.Collections.Generic;
using MyClients.Domain;
using NHibernate;
using NHibernate.Transform;

namespace MyClients.Dao.Support
{
	public class ClientDao : Daobase<Client>, IClientDao
	{
		public ClientDao(SessionFactoryExt sessionFactory)
			: base(sessionFactory)
		{
		}

		public Client GetById(int id)
		{
			return this.Session
				.CreateQuery("from Client client left outer join fetch client.Logins where client.Id = :id")
				.SetParameter<int>("id", id)
				.SetResultTransformer(Transformers.DistinctRootEntity)
				.UniqueResult<Client>();
		}

		public IList<Client> GetAll()
		{
			return this.Session
				.CreateQuery("from Client client left outer join fetch client.Logins")
				.SetResultTransformer(Transformers.DistinctRootEntity)
				.List<Client>();
		}

		public long GetCount()
		{
			return this.Session
				.CreateQuery("select count(*) from Client")
				.UniqueResult<long>();
		}
	}
}