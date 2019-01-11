namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
}
