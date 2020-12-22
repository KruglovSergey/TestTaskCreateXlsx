using System.Data.Entity;

namespace TestTask.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        : base("ConnectionString")
        {
        }

        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderComponents> OrderComponents { get; set; }
        public virtual DbSet<ProductLibrary> ProductLibraries { get; set; }
        public virtual DbSet<Shops> Shops { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>()
                .Property(e => e.DateOrderAgreed)
                .HasPrecision(2);

            modelBuilder.Entity<Orders>()
                .Property(e => e.DateDraftOrder)
                .HasPrecision(2);

            modelBuilder.Entity<Orders>()
                .HasOptional(e => e.Shops);

            modelBuilder.Entity<OrderComponents>()
                .HasOptional(e => e.Orders)
                .WithRequired(e => e.OrderComponents);

            modelBuilder.Entity<ProductLibrary>()
                .HasMany(e => e.OrderComponents)
                .WithRequired(e => e.ProductLibrary)
                .HasForeignKey(e => e.ProductCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shops>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Shops)
                .HasForeignKey(e => e.ShopsId)
                .WillCascadeOnDelete(false);
        }
    }
}