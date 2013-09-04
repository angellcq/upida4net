﻿using NHibernate;
using criteria = NHibernate.Criterion;
using System.Collections.Generic;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Dao.Support
{
    public class OrderDao : DaoBase<Order>, IOrderDao
    {
        public OrderDao(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {
        }

        public IList<Order> GetByClient(int clientId)
        {
            return this.Session
                .CreateCriteria<Order>()
                .Add(criteria.Restrictions.Eq("Client.id", clientId))
                .List<Order>();
        }

        public Order GetById(int id)
        {
            return (Order)this.Session
                .CreateCriteria<Order>()
                .Add(criteria.Restrictions.Eq("id", id))
                .SetFetchMode("orderItems", FetchMode.Join)
                .SetFetchMode("client", FetchMode.Join)
                .UniqueResult<Order>();
        }
    }
}