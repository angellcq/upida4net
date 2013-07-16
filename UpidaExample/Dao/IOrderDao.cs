using System.Collections.Generic;
using UpidaExample.Domain;

namespace UpidaExample.Dao
{
    public interface IOrderDao : IDaoBase<Order>
    {
        IList<Order> GetByClient(int clientId);
        Order GetById(int id);
    }
}