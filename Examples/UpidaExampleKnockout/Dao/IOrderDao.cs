using System.Collections.Generic;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Dao
{
	public interface IOrderDao : IDaobase<Order>
	{
		IList<Order> GetByClient(int clientId);
		Order GetById(int id);
	}
}