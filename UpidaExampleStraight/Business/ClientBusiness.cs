using System.Collections.Generic;
using Upida;
using UpidaExampleStraight.Business.Util;
using UpidaExampleStraight.Dao;
using UpidaExampleStraight.Domain;

namespace UpidaExampleStraight.Business
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