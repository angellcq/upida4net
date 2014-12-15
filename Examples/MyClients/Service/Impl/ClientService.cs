using System.Collections.Generic;
using MyClients.Dao;
using MyClients.Domain;
using MyClients.Validation.Common;
using NHibernate;
using Upida;

namespace MyClients.Service.Impl
{
    public class ClientService : IClientService
    {
        public IMapper Mapper { get; set; }
        public IValidationFacade Validator { get; set; }
        public IClientDao ClientDao { get; set; }

        public Client GetById(int id)
        {
            Client item = this.ClientDao.GetById(id);
            return this.Mapper.Filter(item, Levels.DEEP);
        }

        public IList<Client> GetAll()
        {
            IList<Client> items = this.ClientDao.GetAll();
            return this.Mapper.FilterList(items, Levels.GRID);
        }

        public void Save(Client item)
        {
            this.Validator.AssertClientForSave(item);
            using (ITransaction tx = this.ClientDao.BeginTransaction())
            {
                this.Mapper.Map(item);
                this.ClientDao.Save(item);
                tx.Commit();
            }
        }

        public void Update(Client item)
        {
            this.Validator.AssertClientForUpdate(item);
            using (ITransaction tx = this.ClientDao.BeginTransaction())
            {
                Client existing = this.ClientDao.GetById(item.Id.Value);
                this.Mapper.MapTo(item, existing);
                this.ClientDao.Merge(existing);
                tx.Commit();
            }
        }

        public void Delete(int id)
        {
            using (ITransaction tx = this.ClientDao.BeginTransaction())
            {
                Client existing = this.ClientDao.GetById(id);
                this.Validator.AssertClientExists(existing);
                long count = this.ClientDao.GetCount();
                this.Validator.AssertMoreThanOneClient(count);
                this.ClientDao.Delete(existing);
                tx.Commit();
            }
        }
    }
}