using System.Collections.Generic;
using System.Linq;
using UpidaExampleAngularEF.Domain;

namespace UpidaExampleAngularEF.Dao.Support
{
	public class OrderDao : Daobase<Order>, IOrderDao
	{
		public OrderDao(MyContextFactory sessionFactory)
			: base(sessionFactory)
		{
		}

		public IList<Order> GetByClient(int clientId)
		{
			return this.Session.Orders
				.Where(o => o.Client.Id == clientId)
				.ToList();
		}

		public Order GetById(int id)
		{
			return this.Session.Orders
				.Where(o => o.Id == id)
				.FirstOrDefault();
		}

		public long GetCount(int clientId)
		{
			return this.Session.Orders.Where(o => o.Client.Id == clientId).Count();
		}

		public void Save(Order item)
		{
			this.Session.Orders.Add(item);
		}

		public void Delete(Order item)
		{
			this.Session.Orders.Remove(item);
		}
	}
}