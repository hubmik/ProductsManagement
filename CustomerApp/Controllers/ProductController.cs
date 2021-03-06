﻿using Model;
using Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace CustomerApp.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ActionResult Sort(string sortOrder)
        {
            var context = new Models.ApplicationDbContext();
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.QuantitySortParam = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";
            ViewBag.PriceSortParam = sortOrder == "Price" ? "price_desc" : "Price";
            
            List<Models.Products> list = context.Products.ToList();
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

            return View(nameof(List), list);
        }

        public ViewResult List(string productName)
        {
            ViewModels.ProductsListViewModel model = new ViewModels.ProductsListViewModel();

            using (var context = new Models.ApplicationDbContext())
            {
                model.Products = context.Products
                    .Include(x => x.ProductsCollections).ToList();
            }

            Models.Products prod = new Models.Products
            {
                ProductName = productName
            };

            return View(nameof(List), model);
        }
    }
}