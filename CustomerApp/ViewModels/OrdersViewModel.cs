using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerApp.ViewModels
{
    public class OrdersViewModel
    {
        public string CompanyName { get; set; }
        public string FullName { get; set; }
        public IEnumerable<Models.ApplicationUser> OrdersCollection { get; set; }
        public IEnumerable<Models.Orders> CustomerOrders { get; set; }
        public IEnumerable<Models.OrderedProducts> CustomersOrderedProducts { get; set; }
    }
}