﻿using System;
using System.Collections.Generic;
using Upida;
using UpidaExampleStraight.Dao;
using UpidaExampleStraight.Domain;

namespace UpidaExampleStraight.Business
{
    public class OrderBusiness : BusinessBase
    {
        private IOrderDao orderDao;

        public OrderBusiness(IMapper mapper, IOrderDao orderDao)
            : base(mapper)
        {
            this.orderDao = orderDao;
        }

        public IList<Order> GetByClient(int clientId)
        {
            IList<Order> items = this.orderDao.GetByClient(clientId);
            return this.mapper.OutList(items, Levels.GRID);
        }

        public Order Get(int id)
        {
            Order item = this.orderDao.GetById(id);
            return this.mapper.Out(item, Levels.DEEP);
        }

        public Order GetFull(int id)
        {
            Order item = this.orderDao.GetById(id);
            return this.mapper.Out(item, Levels.FULL);
        }

        public void Save(Order item)
        {
            using (var tx = this.orderDao.BeginTransaction())
            {
                this.mapper.Map(item);
                item.CreatedOn = DateTime.Now;
                this.orderDao.Save(item);
                tx.Commit();
            }
        }

        public void Update(Order item)
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