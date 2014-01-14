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
	}
}