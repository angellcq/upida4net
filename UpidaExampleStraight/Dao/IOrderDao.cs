using System.Collections.Generic;
using UpidaExampleStraight.Domain;

namespace UpidaExampleStraight.Dao
{
    public interface IOrderDao : IDaoBase<Order>
    {
        IList<Order> GetByClient(int clientId);
        Order GetById(int id);
    }
}