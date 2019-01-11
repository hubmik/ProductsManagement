namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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

        [Required]
        [StringLength(50)]
        public string DeliveryName { get; set; }

        public int Capacity { get; set; }

        public decimal DeliveryPrice { get; set; }

        [InverseProperty(nameof(Model.Orders.Deliveries))]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
