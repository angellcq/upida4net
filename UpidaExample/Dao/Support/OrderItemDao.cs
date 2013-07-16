using NHibernate;
using UpidaExample.Domain;

namespace UpidaExample.Dao.Support
{
    public class OrderItemDao : DaoBase<OrderItem>, IOrderItemDao
    {
        public OrderItemDao(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {
        }
    }
}