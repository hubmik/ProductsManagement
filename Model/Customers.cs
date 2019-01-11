namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
        public virtual CustomerApp.Models.ApplicationUser ApplicationUser { get; set; }
        
        [InverseProperty(nameof(Model.Orders.Customers))]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
