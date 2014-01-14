using NHibernate;
using System.Collections.Generic;
using UpidaExampleAngular.Domain;

namespace UpidaExampleAngular.Dao.Support
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