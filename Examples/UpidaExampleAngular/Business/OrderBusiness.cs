﻿using System;
using System.Collections.Generic;
using Upida;
using UpidaExampleAngular.Dao;
using UpidaExampleAngular.Domain;

namespace UpidaExampleAngular.Business
{
	public class OrderBusiness : BusinessBase
	{
		private IOrderDao orderDao;

		public OrderBusiness(IMapper mapper, IOrderDao orderDao)
			: base(mapper)
		{
			this.orderDao = orderDao;
		}

		public virtual IList<Order> GetByClient(int clientId)
		{
			IList<Order> items = this.orderDao.GetByClient(clientId);
			return this.mapper.FilterList(items, Levels.GRID);
		}

		public virtual Order Get(int id)
		{
			Order item = this.orderDao.GetById(id);
			return this.mapper.Filter(item, Levels.DEEP);
		}

		public virtual Order GetFull(int id)
		{
			Order item = this.orderDao.GetById(id);
			return this.mapper.Filter(item, Levels.FULL);
		}

		public virtual void Save(Order item)
		{
			using (var tx = this.orderDao.BeginTransaction())
			{
				this.mapper.Map(item);
				item.CreatedOn = DateTime.Now;
				this.orderDao.Save(item);
				tx.Commit();
			}
		}

		public virtual void Update(Order item)
		{
			using (var tx = this.orderDao.BeginTransaction())
			{
				Order existing = this.orderDao.Load(item.Id);
				this.mapper.MapTo(item, existing);
				this.orderDao.Update(existing);
				tx.Commit();
			}
		}
	}
}