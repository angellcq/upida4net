using System.Collections.Generic;
using MyClients.Dao;
using MyClients.Domain;
using NHibernate;
using Upida;
using Upida.Validation;

namespace MyClients.Business
{
    public class ClientBusiness
    {
        private IMapper mapper;
        private IValidationContext validator;
        private IClientDao clientDao;

        public ClientBusiness(IMapper mapper, IValidationContext validator, IClientDao clientDao)
        {
            this.mapper = mapper;
            this.validator = validator;
            this.clientDao = clientDao;
        }

        public Client GetById(int id)
        {
            Client item = this.clientDao.GetById(id);
            return this.mapper.Out(item, Levels.DEEP);
        }

        public IList<Client> GetAll()
        {
            IList<Client> items = this.clientDao.GetAll();
            return this.mapper.OutList(items, Levels.GRID);
        }

        public void Save(Client item)
        {
            this.validator.AssertValid(item, Groups.SAVE);
            using (ITransaction tx = this.clientDao.BeginTransaction())
            {
                this.mapper.Map(item);
                this.clientDao.Save(item);
                tx.Commit();
            }
        }

        public void Update(Client item)
        {
            this.validator.AssertValid(item, Groups.UPDATE);
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