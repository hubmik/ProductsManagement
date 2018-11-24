using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CustomerApp.Controllers
{
    public class CartController : Controller
    {
        private Model.Abstract.IProductRepository repository;

        public CartController(Model.Abstract.IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(nameof(Index), new ViewModels.CartIndexViewModel
            {
                ReturnUrl = returnUrl,
                Cart = cart
            });
        }

        public ViewResult Checkout()
        {
            //var client = new WcfService.Service1Client();
            //List<Model.Customers> custDet = await client.GetCustomerDataAsync(1);
            //Model.Customers cust = custDet.FirstOrDefault();

            Model.Db context = new Model.Db();
            IQueryable<Model.Customers> custData = context.Customers.Include(x => x.Region).Where(x => x.CustomerId == 1);
            Model.Customers cst = custData.FirstOrDefault();

            return View(nameof(Checkout), cst);
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Model.Products product = repository.
                Products.FirstOrDefault(p => p.ProductId == productId);
            
            if (product != null)
                cart.AddItem(product, 1);

            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Model.Products product = repository.
                Products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
                cart.RemoveLine(product);

            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }
    }
}