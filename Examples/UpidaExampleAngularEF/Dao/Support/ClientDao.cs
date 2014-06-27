using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UpidaExampleAngularEF.Domain;

namespace UpidaExampleAngularEF.Dao.Support
{
	public class ClientDao : Daobase<Client>, IClientDao
	{
		public ClientDao(MyContextFactory sessionFactory)
			: base(sessionFactory)
		{
		}

		public IList<Client> GetAll()
		{
			return this.Session.Clients.ToList();
		}

		public Client GetById(int id)
		{
			return this.Session.Clients.Where(c => c.Id == id).FirstOrDefault();
		}

		public long GetCount()
		{
			return this.Session.Clients.Count();
		}

		public void Save(Client item)
		{
			this.Session.Clients.Add(item);
			this.Session.SaveChanges();
		}

		public void Delete(Client item)
		{
			this.Session.Clients.Remove(item);
		}
	}
}