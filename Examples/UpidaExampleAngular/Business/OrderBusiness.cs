using System;
using System.Collections.Generic;
using Upida;
using Upida.Validation;
using UpidaExampleAngular.Dao;
using UpidaExampleAngular.Domain;

namespace UpidaExampleAngular.Business
{
	public class OrderBusiness : BusinessBase
	{
		private IOrderDao orderDao;

		public OrderBusiness(IMapper mapper, IValidationContext validator, IOrderDao orderDao)
			: base(mapper, validator)
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

		public virtual void Delete(int id)
		{
			var failures = this.validator.CreateFailureList();
			using (var tx = this.orderDao.BeginTransaction())
			{
				Order existing = this.orderDao.GetById(id);
				failures.FailIf(null == existing, "Order does not exist", Severity.Fatal);
				this.validator.Assert(failures);
				long count = this.orderDao.GetCount(existing.Client.Id.Value);
				failures.FailIf(1 == count, "Cannot delete the only order in the client");
				this.validator.Assert(failures);
				this.orderDao.Delete(existing);
				tx.Commit();
			}
		}
	}
}