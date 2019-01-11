using CustomerApp.Models;
using Microsoft.AspNet.Identity;
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

        public ActionResult Checkout(Cart cart, string returnUrl)
        {
            if(cart.Lines.Count() == 0)
            {
                return RedirectToAction(nameof(Index), new { returnUrl });
            }
            ShippingDetails shippingDetails = new ShippingDetails();
            ApplicationUser result = shippingDetails.GetCustomerData(System.Web.HttpContext.Current.User.Identity.GetUserId<int>());
            ViewModels.CartSummaryViewModel cartSummaryVM = new ViewModels.CartSummaryViewModel();

            cartSummaryVM = new ViewModels.CartSummaryViewModel
            {
                City = result.Customers.FirstOrDefault().Region.City,
                CompanyName = result.Customers.FirstOrDefault().CompanyName,
                Country = result.Customers.FirstOrDefault().Region.Country,
                Email = result.Email,
                Street = result.Customers.FirstOrDefault().Region.Street,
                SelectedDeliveryId = cartSummaryVM.SelectedDeliveryId
            };
            return View(nameof(Checkout), cartSummaryVM);
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Products product = repository.
                Products.FirstOrDefault(p => p.ProductId == productId);
            int quantity;

            quantity = cart.GetProductsCollectionSize(product);

            if (product != null)
                cart.AddItem(product, quantity);

            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Products product = repository.
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