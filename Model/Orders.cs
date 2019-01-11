namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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

        public DateTime OrderDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customers Customers { get; set; }

        [ForeignKey(nameof(DeliveryId))]
        public virtual Deliveries Deliveries { get; set; }

        [InverseProperty(nameof(Model.OrderedProducts.Orders))]
        public virtual ICollection<OrderedProducts> OrderedProducts { get; set; }

        [ForeignKey(nameof(StatusId))]
        public virtual OrderStates OrderStates { get; set; }
    }
}
