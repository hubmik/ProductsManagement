using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class ProductExtension
    {
        public int QuantityFrom { get; set; }

        public int QuantityTo { get; set; }

        public decimal UnitPriceFrom { get; set; }

        public decimal UnitPriceTo { get; set; }
    }
}
