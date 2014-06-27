using System.Collections.Generic;
using UpidaExampleAngularEF.Domain;

namespace UpidaExampleAngularEF.Dao
{
	public interface IOrderDao : IDaobase<Order>
	{
		IList<Order> GetByClient(int clientId);
		Order GetById(int id);
		long GetCount(int clientId);
		void Save(Order item);
		void Delete(Order item);
	}
}