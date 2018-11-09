namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            OrderedProducts = new HashSet<OrderedProducts>();
        }

        [Key]
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public int DeliveryId { get; set; }

        public int StatusId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public virtual Customers Customers { get; set; }

        public virtual Deliveries Deliveries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderedProducts> OrderedProducts { get; set; }

        public virtual OrderStates OrderStates { get; set; }
    }
}
