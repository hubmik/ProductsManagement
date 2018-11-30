namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Db : DbContext
    {
        public Db()
            : base("name=Db")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Deliveries> Deliveries { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<OrderedProducts> OrderedProducts { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderStates> OrderStates { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deliveries>()
                .Property(e => e.DeliveryPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Deliveries>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Deliveries)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderStates>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.OrderStates)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.UnitPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.OrderedProducts)
                .WithRequired(e => e.Products)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Regions>()
                .HasMany(e => e.Customers)
                .WithRequired(e => e.Region)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Regions>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Region)
                .WillCascadeOnDelete(false);
        }
    }
}
