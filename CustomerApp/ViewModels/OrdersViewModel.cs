using CustomerApp.Models;
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
        public IEnumerable<Orders> CustomerOrders { get; set; }
        public bool IsOrderedChecked { get; set; }
        public bool IsAcceptedChecked { get; set; }
    }
}