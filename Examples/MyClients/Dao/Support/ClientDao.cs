using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClients.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace MyClients.Dao.Support
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

		public Client GetById(int id)
		{
			return this.Session
				.CreateCriteria<Client>()
				.Add(Restrictions.Eq("Id", id))
				.SetFetchMode("Logins", FetchMode.Join)
				.UniqueResult<Client>();
		}
	}
}