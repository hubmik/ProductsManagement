using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerApp.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Models.Products> Products { get; set; }
        public Models.Products Product { get; set; }
        public Models.ProductsCollections ProductsCollections { get; set; }
    }
}