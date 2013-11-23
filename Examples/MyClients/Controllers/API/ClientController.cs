using System.Collections.Generic;
using System.Web.Http;
using MyClients.Business;
using MyClients.Domain;

namespace MyClients.Controllers.API
{
	public class ClientController : ApiController
	{
		private ClientBusiness clientBusiness;

		public ClientController(ClientBusiness clientBusiness)
		{
			this.clientBusiness = clientBusiness;
		}

		public Client GetById(int id)
		{
			return this.clientBusiness.GetById(id);
		}

		public IList<Client> getAll()
		{
			return this.clientBusiness.GetAll();
		}

		public void Save(Client item)
		{
			this.clientBusiness.Save(item);
		}

		public void Update(Client item)
		{
			this.clientBusiness.Update(item);
		}
	}
}