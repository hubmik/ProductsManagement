namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderStates
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderStates()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        public int StatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [InverseProperty(nameof(Model.Orders.OrderStates))]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
