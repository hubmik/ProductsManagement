using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerApp.ViewModels
{
    public class ProductsListViewModel
    { 
        public IEnumerable<Model.Products> Products { get; set; }
    }
}