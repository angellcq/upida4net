using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using UpidaExampleAngularEF.Domain;

namespace UpidaExampleAngularEF.Dao.Support
{
	public class DbSession : DbContext
	{
		public DbSession(string connString)
			: base(connString)
		{
			//Database.SetInitializer<DbSession>(null);
		}

		public DbSet<Client> Clients { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Client>()
				.ToTable("Client");
			modelBuilder.Entity<Client>()
				.HasMany(m => m.Orders)
				.WithRequired(m => m.Client)
				.HasForeignKey(m => m.Client_Id);
			
			modelBuilder.Entity<Order>()
				.ToTable("Order");
			modelBuilder.Entity<Order>()
				.HasRequired(m => m.Client);
			modelBuilder.Entity<Order>()
				.HasMany(m => m.OrderItems)
				.WithRequired(m => m.Order)
				.HasForeignKey(m => m.Order_Id)
				.WillCascadeOnDelete();

			modelBuilder.Entity<OrderItem>()
				.ToTable("OrderItem");
			modelBuilder.Entity<OrderItem>()
				.HasRequired(m => m.Order)
				.WithMany(m => m.OrderItems)
				.HasForeignKey(m => m.Order_Id)
				.WillCascadeOnDelete();
		} 
	}
}