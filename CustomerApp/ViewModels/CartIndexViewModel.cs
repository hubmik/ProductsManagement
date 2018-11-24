using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerApp.ViewModels
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}