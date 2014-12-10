using System.Collections.Generic;
using System.Web.Http;
using MyClients.Domain;
using MyClients.Service;

namespace MyClients.Controllers.API
{
    public class ClientController : ApiController
    {
        public IClientService ClientService { get; set; }

        public Client GetById(int id)
        {
            return this.ClientService.GetById(id);
        }

        public IList<Client> GetAll()
        {
            return this.ClientService.GetAll();
        }

        public void Save(Client item)
        {
            this.ClientService.Save(item);
        }

        public void Update(Client item)
        {
            this.ClientService.Update(item);
        }

        [HttpPost]
        public void Delete(int id)
        {
            this.ClientService.Delete(id);
        }
    }
}