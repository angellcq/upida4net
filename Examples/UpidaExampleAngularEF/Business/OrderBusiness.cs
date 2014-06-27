using System;
using System.Collections.Generic;
using System.Transactions;
using Upida;
using Upida.Validation;
using UpidaExampleAngularEF.Dao;
using UpidaExampleAngularEF.Domain;

namespace UpidaExampleAngularEF.Business
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
			using (TransactionScope tx = new TransactionScope())
			{
				this.mapper.Map(item);
				item.CreatedOn = DateTime.Now;
				this.orderDao.Save(item);
				tx.Complete();
			}
		}

		public virtual void Update(Order item)
		{
			using (TransactionScope tx = new TransactionScope())
			{
				Order existing = this.orderDao.GetById(item.Id.Value);
				this.mapper.MapTo(item, existing);
				tx.Complete();
			}
		}

		public virtual void Delete(int id)
		{
			var failures = this.validator.CreateFailureList();
			using (TransactionScope tx = new TransactionScope())
			{
				Order existing = this.orderDao.GetById(id);
				failures.FailIfNull(existing, null, "Order does not exist", Severity.Fatal);
				this.validator.Assert(failures);
				long count = this.orderDao.GetCount(existing.Client.Id.Value);
				failures.FailIfEqual(1, count, null, "Cannot delete the only order in the client");
				this.validator.Assert(failures);
				this.orderDao.Delete(existing);
				tx.Complete();
			}
		}
	}
}