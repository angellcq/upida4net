using System.Data.Entity;
using UpidaExampleAngularEF.Domain;

namespace UpidaExampleAngularEF.Dao.Support
{
	public class MyContext : DbContext
	{
		public MyContext(string connString)
			: base(connString)
		{
		}

		public DbSet<Client> Clients { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; } 
	}
}