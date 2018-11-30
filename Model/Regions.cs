namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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

        [InverseProperty(nameof(Model.Customers.Region))]
        public virtual ICollection<Customers> Customers { get; set; }

        [InverseProperty(nameof(Model.Employees.Region))]
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
