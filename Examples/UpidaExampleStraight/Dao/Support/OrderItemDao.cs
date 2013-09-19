using NHibernate;
using UpidaExampleStraight.Domain;

namespace UpidaExampleStraight.Dao.Support
{
    public class OrderItemDao : Daobase<OrderItem>, IOrderItemDao
    {
        public OrderItemDao(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {
        }
    }
}