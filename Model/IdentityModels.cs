using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CustomerApp.Models;

namespace CustomerApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole,
        CustomUserClaim>
    {
        [InverseProperty(nameof(Models.Customers.ApplicationUser))]
        public virtual ICollection<Customers> Customers { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole, 
        int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("CompanyDB")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ApplicationDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Deliveries> Deliveries { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<OrderedProducts> OrderedProducts { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderStates> OrderStates { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }
        public virtual DbSet<ProductsCollections> ProductsCollections { get; set; }
    }

    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public partial class Customers
    {
        [Key]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public int UserId { get; set; }

        [ForeignKey(nameof(Region))]
        public int RegionId { get; set; }

        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }

        [ForeignKey(nameof(RegionId))]
        public virtual Regions Region { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser ApplicationUser { get; set; }
        
        [InverseProperty(nameof(Models.Orders.Customers))]
        public virtual ICollection<Orders> Orders { get; set; }
    }

    public partial class Deliveries
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Deliveries()
        //{
        //    Orders = new HashSet<Orders>();
        //}

        [Key]
        public int DeliveryId { get; set; }

        [Required]
        [StringLength(50)]
        public string DeliveryType { get; set; }

        [InverseProperty(nameof(Models.Orders.Deliveries))]
        public virtual ICollection<Orders> Orders { get; set; }
    }

    public partial class Employees
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string JobPosition { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime HireDate { get; set; }

        [InverseProperty(nameof(Models.Orders.Employees))]
        public virtual ICollection<Orders> Orders { get; set; }
    }

    public partial class OrderedProducts
    {
        public int OrderedProductsId { get; set; }

        [ForeignKey(nameof(Orders))]
        public int OrderId { get; set; }

        [ForeignKey(nameof(Products))]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Orders Orders { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Products Products { get; set; }
    }

    public partial class Orders
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Orders()
        //{
        //    OrderedProducts = new HashSet<OrderedProducts>();
        //}

        [Key]
        public int OrderId { get; set; }

        [ForeignKey(nameof(Customers))]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(Deliveries))]
        public int DeliveryId { get; set; }

        [ForeignKey(nameof(OrderStates))]
        public int StatusId { get; set; }

        [ForeignKey(nameof(Employees))]
        public int EmployeeId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customers Customers { get; set; }

        [ForeignKey(nameof(DeliveryId))]
        public virtual Deliveries Deliveries { get; set; }
        
        [ForeignKey(nameof(StatusId))]
        public virtual OrderStates OrderStates { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public virtual Employees Employees { get; set; }

        [InverseProperty(nameof(Models.OrderedProducts.Orders))]
        public virtual ICollection<OrderedProducts> OrderedProducts { get; set; }
    }

    public partial class OrderStates
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public OrderStates()
        //{
        //    Orders = new HashSet<Orders>();
        //}

        [Key]
        public int StatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [InverseProperty(nameof(Models.Orders.OrderStates))]
        public virtual ICollection<Orders> Orders { get; set; }
    }

    public partial class Products
    {
        [Key]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductsCollections))]
        public int CollectionId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        [ForeignKey(nameof(CollectionId))]
        public virtual ProductsCollections ProductsCollections { get; set; }

        [InverseProperty(nameof(Models.OrderedProducts.Products))]
        public virtual ICollection<OrderedProducts> OrderedProducts { get; set; }
    }

    public partial class Regions
    {
        [Key]
        public int RegionId { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string Street { get; set; }

        [InverseProperty(nameof(Models.Customers.Region))]
        public virtual ICollection<Customers> Customers { get; set; }
    }

    public partial class ProductsCollections
    {
        [Key]
        public int CollectionId { get; set; }

        public int CollectionSize { get; set; }

        [InverseProperty(nameof(Models.Products.ProductsCollections))]
        public virtual ICollection<Products> Products { get; set; }
    }
}