using System.Collections.Generic;
using Upida;
using UpidaExample.Business.Util;
using UpidaExample.Dao;
using UpidaExample.Domain;

namespace UpidaExample.Business
{
    public class ClientBusiness : BusinessBase
    {
        private IClientDao clientDao;

        public ClientBusiness(TransactionFactory transactionFactory, IMapper mapper, IClientDao clientDao)
            : base(transactionFactory, mapper)
        {
            this.clientDao = clientDao;
        }

        public IList<Client> GetAll()
        {
            IList<Client> items = this.clientDao.GetAll();
            return this.mapper.OutList(items, Levels.GRID);
        }

        public void Save(Client item)
        {
            using (Transaction tx = this.transactionFactory.Start())
            {
                this.mapper.Map(item);
                this.clientDao.Save(item);
                tx.Commit();
            }
        }
    }
}