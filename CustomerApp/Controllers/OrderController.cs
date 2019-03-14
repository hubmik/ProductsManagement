using CustomerApp.Models;
using CustomerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace CustomerApp.Controllers
{
    public class OrderController : Controller
    {
        public bool isCartAlreadyDisposed = false;
        // GET: Order
        public ActionResult Index(OrdersViewModel ordersViewModel)
        {
            OrderContext orderContext = new OrderContext();
            OrdersViewModel ordersVM = new OrdersViewModel
            {
                CompanyName = orderContext.GetCompanyName(),
                CustomerOrders = orderContext.GetOrders(true, true),
            };
            
            return View(nameof(Index), ordersVM);
        }

        [HttpPost]
        public ActionResult Index(bool IsOrderedChecked, bool IsAcceptedChecked)
        {
            OrderContext orderContext = new OrderContext();
            OrdersViewModel ordersVM = new OrdersViewModel
            {
                CompanyName = orderContext.GetCompanyName(),
                CustomerOrders = orderContext.GetOrders(IsOrderedChecked, IsAcceptedChecked)
            };

            return View(nameof(Index), ordersVM);
        }

        public async Task<ActionResult> CurrentOrder(CartSummaryViewModel csVM, Cart cart)
        {
            var crt = cart;
            int orderId;
            MailSender mailSender = new MailSender();
            OrderContext orderContext = new OrderContext();
            orderId = orderContext.SaveOrder(csVM, cart);
            orderContext.SetOrderedProducts(cart, orderId);
            SessionProcess.IsSessionDisposed = true;
            await mailSender.Send(csVM, cart);

            if (Session["Cart"] != null)
            {
                Session["Cart"] = null;
            }
            return View(nameof(CurrentOrder), new CartIndexViewModel { Cart = crt });
        }

        public ActionResult OrderDetails(int id)
        {
            List<OrderedProducts> list = null;
            using (var context = new ApplicationDbContext())
            {
                IQueryable<OrderedProducts> query = context.OrderedProducts
                    .Where(x => x.OrderId == id)
                    .Include(x => x.Products)
                    .Include(x => x.Orders.Regions);
                list = query.ToList();
            }

            return View(list);
        }
    }
}