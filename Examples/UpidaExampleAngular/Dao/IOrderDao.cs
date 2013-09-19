using System.Collections.Generic;
using UpidaExampleAngular.Domain;

namespace UpidaExampleAngular.Dao
{
    public interface IOrderDao : IDaobase<Order>
    {
        IList<Order> GetByClient(int clientId);
        Order GetById(int id);
    }
}