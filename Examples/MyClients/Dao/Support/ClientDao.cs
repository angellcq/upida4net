using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClients.Domain;
using NHibernate;

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
            throw new NotImplementedException();
        }

        public Client GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}