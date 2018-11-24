using Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CustomerApp.Controllers
{
    public class ProductController : AsyncController
    {
        private IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ActionResult Sort(string sortOrder)
        {
            var context = new Model.Db();
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.QuantitySortParam = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";
            ViewBag.PriceSortParam = sortOrder == "Price" ? "price_desc" : "Price";

            List<Model.Products> list = context.Products.ToList();
            switch (sortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(x => x.ProductName).ToList();
                    break;
                case "Price":
                    list = list.OrderBy(x => x.UnitPrice).ToList();
                    break;
                case "price_desc":
                    list = list.OrderByDescending(x => x.UnitPrice).ToList();
                    break;
                case "Quantity":
                    list = list.OrderBy(x => x.Quantity).ToList();
                    break;
                case "quantity_desc":
                    list = list.OrderByDescending(x => x.Quantity).ToList();
                    break;
                default:
                    list = list.OrderBy(x => x.ProductName).ToList();
                    break;
            }

            return View(nameof(ProductsList), list);
        }

        public async Task<ViewResult> ProductsList(string productName)
        {
            ViewModels.ProductsListViewModel model = new ViewModels.ProductsListViewModel();
            
            Model.Products prod = new Model.Products
            {
                ProductName = productName
            };
            WcfService.ProductExtension productExtension = new WcfService.ProductExtension();

            using (var client = new WcfService.Service1Client())
            {
                model.Products = await client.GetProductsAsync(prod, productExtension);
            }

            return View(nameof(ProductsList), model);
        }
    }
}