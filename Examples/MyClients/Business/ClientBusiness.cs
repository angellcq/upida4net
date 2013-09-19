using System.Collections.Generic;
using MyClients.Dao;
using MyClients.Domain;
using NHibernate;
using Upida;

namespace MyClients.Business
{
    public class ClientBusiness
    {
        private IMapper mapper;
        private IClientDao clientDao;

        public ClientBusiness(IMapper mapper, IClientDao clientDao)
        {
            this.mapper = mapper;
            this.clientDao = clientDao;
        }

        public Client GetById(int id)
        {
            Client item = this.clientDao.GetById(id);
            return this.mapper.Out(item, Levels.FULL);
        }

        public IList<Client> GetAll()
        {
            IList<Client> items = this.clientDao.GetAll();
            return this.mapper.OutList(items, Levels.GRID);
        }

        public void Save(Client item)
        {
            using (ITransaction tx = this.clientDao.BeginTransaction())
            {
                this.mapper.Map(item);
                this.clientDao.Save(item);
                tx.Commit();
            }
        }

        public void Update(Client item)
        {
            using (ITransaction tx = this.clientDao.BeginTransaction())
            {
                Client existing = this.clientDao.GetById(item.Id.Value);
                this.mapper.MapTo(item, existing);
                this.clientDao.Merge(existing);
                tx.Commit();
            }
        }
    }
}