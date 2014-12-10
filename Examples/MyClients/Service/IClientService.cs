using System.Collections.Generic;
using MyClients.Domain;

namespace MyClients.Service
{
    public interface IClientService
    {
        Client GetById(int id);
        IList<Client> GetAll();
        void Save(Client item);
        void Update(Client item);
        void Delete(int id);
    }
}