using CustomerApp.Models;
using CustomerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CustomerApp.Controllers
{
    public class OrderController : Controller
    {
        public bool isCartAlreadyDisposed = false;
        // GET: Order
        public ActionResult Index()
        {
            OrderContext orderContext = new OrderContext();
            OrdersViewModel ordersVM = new OrdersViewModel
            {
                CompanyName = orderContext.GetCompanyName(),
                OrdersCollection = orderContext.GetOrders(),
            };
            ordersVM.CustomerOrders = ordersVM.OrdersCollection.FirstOrDefault().Customers.FirstOrDefault().Orders;

            return View(nameof(Index), ordersVM);
        }

        public async Task<ActionResult> CurrentOrder(CartSummaryViewModel csVM, Cart cart)
        {
            var crt = cart;
            int orderId;
            MailSender mailSender = new MailSender();
            OrderContext orderContext = new OrderContext();
            orderId = orderContext.SaveOrder(csVM);
            orderContext.SetOrderedProducts(cart, orderId);
            SessionProcess.IsSessionDisposed = true;
            await mailSender.Send(csVM, cart);

            if (Session["Cart"] != null)
            {
                Session["Cart"] = null;
            }
            return View(nameof(CurrentOrder), new CartIndexViewModel { Cart = crt });
        }
    }
}