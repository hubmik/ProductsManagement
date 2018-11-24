using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CustomerApp.Controllers
{
    public class OrderController : AsyncController
    {
        public async Task<ViewResult> Orders()
        {
            return View(nameof(Orders));
        }

        public ViewResult Checkout()
        {
            //Model.Customers customer = new Model.Customers { CustomerId = 1 };
            ViewModels.CartSummaryViewModel cartSummaryViewModel = null;
            //cartSummaryViewModel.Details = cartSummaryViewModel.Details.InitializeCustomerData(customer);
            return View(nameof(Checkout), cartSummaryViewModel);
        }
    }
}