using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerApp.Models
{
    public class CartLine
    {
        public Model.Products Product { get; set; }
        public int Quantity { get; set; }
    }
}