using System.Collections.Generic;
using System.Linq;
using UpidaExampleAngularEF.Domain;

namespace UpidaExampleAngularEF.Dao.Support
{
	public class OrderDao : Daobase<Order>, IOrderDao
	{
		public OrderDao(DbSessionFactory sessionFactory)
			: base(sessionFactory)
		{
		}

		public IList<Order> GetByClient(int clientId)
		{
			return this.Session.Orders
				.Where(o => o.Client_Id == clientId)
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
			return this.Session.Orders.Where(o => o.Client_Id == clientId).Count();
		}

		public void Save(Order item)
		{
			this.Session.Orders.Add(item);
		}

		public void Update(Order item)
		{
			this.Session.OrderItems.Local
				.Where(m => m.Order_Id == null)
				.ToList()
				.ForEach(m => this.Session.OrderItems.Remove(m));
		}

		public void Delete(Order item)
		{
			this.Session.OrderItems.RemoveRange(item.OrderItems);
			this.Session.Orders.Remove(item);
		}
	}
}