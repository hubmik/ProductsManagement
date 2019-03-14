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
            ViewModels.CartSummaryViewModel cartSummaryVM = new ViewModels.CartSummaryViewModel();
            ShippingDetails shippingDetails = new ShippingDetails();
            Regions defaultResult = shippingDetails
                .GetCustomerData(System.Web.HttpContext.Current.User.Identity.GetUserId<int>())
                .Where(x => x.IsDefault)
                .FirstOrDefault();

            List<Regions> regionsList = shippingDetails.GetCustomerData(System.Web.HttpContext.Current.User.Identity.GetUserId<int>());
            
            ViewBag.RegionsList = new SelectList(regionsList, "RegionId", "RegionId");
            cartSummaryVM = new ViewModels.CartSummaryViewModel
            {
                RegionId = defaultResult.RegionId,
                City = defaultResult.City,
                CompanyName = defaultResult.Customers.CompanyName,
                Country = defaultResult.Country,
                Email = defaultResult.Customers.ApplicationUser.Email,
                Street = defaultResult.Street,
                SelectedDeliveryId = cartSummaryVM.SelectedDeliveryId
            };
            return View(nameof(Checkout), cartSummaryVM);
        }

        public PartialViewResult GetAddressData(int RegionId)
        {
            ViewModels.CartSummaryViewModel cartSummaryVM = new ViewModels.CartSummaryViewModel();
            ShippingDetails shippingDetails = new ShippingDetails();
            List<Regions> regionsList = shippingDetails.GetCustomerData(System.Web.HttpContext.Current.User.Identity.GetUserId<int>());
            Regions region = regionsList.Where(x => x.RegionId == RegionId).FirstOrDefault();
            cartSummaryVM = new ViewModels.CartSummaryViewModel()
            {
                City = region.City,
                CompanyName = region.Customers.CompanyName,
                Country = region.Country,
                Email = region.Customers.ApplicationUser.Email,
                RegionId = region.RegionId,
                SelectedDeliveryId = cartSummaryVM.SelectedDeliveryId,
                Street = region.Street
            };
            return PartialView("_CheckoutContent", cartSummaryVM);
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