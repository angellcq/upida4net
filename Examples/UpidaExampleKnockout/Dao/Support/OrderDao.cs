﻿using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Dao.Support
{
	public class OrderDao : Daobase<Order>, IOrderDao
	{
		public OrderDao(SessionFactoryExt sessionFactory)
			: base(sessionFactory)
		{
		}

		public IList<Order> GetByClient(int clientId)
		{
			return this.Session.CreateQuery("from Order o where o.Client.Id = :clientId")
				.SetParameter<int>("clientId", clientId)
				.SetResultTransformer(Transformers.DistinctRootEntity)
				.List<Order>();
		}

		public Order GetById(int id)
		{
			return this.Session.CreateQuery("from Order o inner join fetch o.Client left outer join fetch o.OrderItems where o.Id = :id")
				.SetParameter<int>("id", id)
				.SetResultTransformer(Transformers.DistinctRootEntity)
				.UniqueResult<Order>();
		}

		public long GetCount(int clientId)
		{
			return this.Session.CreateQuery("select count(*) from Order o where o.Client.Id = :clientId")
				.SetParameter<int>("clientId", clientId)
				.UniqueResult<long>();
		}
	}
}