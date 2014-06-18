using NHibernate;
using System.Collections.Generic;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Dao.Support
{
	public class ClientDao : Daobase<Client>, IClientDao
	{
		public ClientDao(SessionFactoryExt sessionFactory)
			: base(sessionFactory)
		{
		}

		public IList<Client> GetAll()
		{
			return this.Session.CreateQuery("from Client")
				.List<Client>();
		}

		public Client GetById(int id)
		{
			return this.Session.Get<Client>(id);
		}

		public long GetCount()
		{
			return this.Session
				.CreateQuery("select count(*) from Client")
				.UniqueResult<long>();
		}
	}
}