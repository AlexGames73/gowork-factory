using GoWorkFactoryDataBase.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GoWorkFactoryDataBase
{
    public class GoWorkFactoryDataBaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Database=GoWorkFactory2;Username=postgres;Password=postgres", o => o.SetPostgresVersion(9, 6));
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<MaterialProduct> MaterialProducts { get; set; }
        public virtual DbSet<MaterialRequest> MaterialRequests { get; set; }
        public virtual DbSet<ProductOrder> ProductOrders { get; set; }
    }
}
