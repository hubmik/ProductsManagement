using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CustomerApp.Models;
using Server.DTO;

namespace Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<Customers> GetCustomerData(int customerId)
        {
            List<Customers> custDet;
            using (var context = new ApplicationDbContext())
            {
                IQueryable<Customers> custData = context.Customers
                    .Include(cust => cust.Region)
                    .Where(cust => cust.CustomerId == customerId);

                custDet = custData.ToList();
            }
            return custDet;
        }

        public List<Products> GetProducts(Products product, ProductExtension productExtension)
        {
            List<Products> products = new List<Products>();
            IQueryable<Products> pr;            
            
            using (var context = new ApplicationDbContext())
            {
                
                pr = !string.IsNullOrEmpty(product.ProductName) || product.UnitPrice > 0 || product.Quantity > 0 ||
                    productExtension.QuantityFrom > 0
                    || productExtension.QuantityTo > 0 || productExtension.UnitPriceFrom > 0 || productExtension.UnitPriceTo > 0
                    ? context.Products.
                        Where(prd =>
                    prd.ProductName == product.ProductName ||
                    prd.Quantity == product.Quantity ||
                    prd.UnitPrice == product.UnitPrice ||
                    (prd.Quantity >= productExtension.QuantityFrom && prd.Quantity <= productExtension.QuantityTo) ||
                    (prd.UnitPrice >= productExtension.UnitPriceFrom && prd.UnitPrice <= productExtension.UnitPriceTo)
                    )
                    : context.Products;

                products = pr.ToList();

                //var res = context.Products.Where(p => p.Quantity == product.Quantity && p.UnitPrice == product.UnitPrice);

                //if (productExtension.QuantityFrom > 0 && productExtension.QuantityTo > 0)
                //    pr = context.Products
                //        .Where(p =>
                //        p.Quantity >= productExtension.QuantityFrom && p.Quantity <= productExtension.QuantityTo);


                //if (productExtension.UnitPriceFrom > 0 && productExtension.UnitPriceTo > 0)
                //    pr = context.Products.Where(p => p.UnitPrice >= productExtension.UnitPriceFrom && p.UnitPrice <= productExtension.UnitPriceTo);
                //.Include(prd => prd.OrderedProducts.Select(ord => ord.Orders.Customers)
                //.ToList();

                //context.Products.Add(new Products() { ProductName = "CPU", });
                //products[0].ProductName += " zmienione";
                //context.Products.Remove(products[0]);

                //context.SaveChanges();
            }

            //prod = db.Products.Select(prd => new { prd.ProductName, prd.Quantity, prd.UnitPrice });

            return products;//[0].OrderedProducts.First().Orders.Customers;
        }

        public List<ProductExtension> ProductExtensions()
        {
            return new List<ProductExtension>();
        }

        public void AddProduct(Products product)
        {
            Products productAdd;
            using (var context = new ApplicationDbContext())
            {
                productAdd = new Products
                {
                    ProductName = product.ProductName,
                    Quantity = product.Quantity,
                    UnitPrice = product.UnitPrice,
                };
                context.Products.Add(productAdd);
                context.SaveChanges();
            }
        }

        public void DeleteProduct(Products product)
        {
            List<Products> productsList;
            using (var context = new ApplicationDbContext())
            {
                productsList = context.Products.ToList();
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }

        public void UpdateProduct(Products product)
        {
            List<Products> products;
            using (var context = new ApplicationDbContext())
            {
                products = context.Products.ToList();
                products[products.IndexOf(product)] = product;
                context.SaveChanges();
            }
        }

        public List<Orders> GetOrders(Customers specifiedCustomer)
        {
            List<Orders> orders = new List<Orders>();
            using (var context = new ApplicationDbContext())
            {
                orders = context.Orders.ToList();
            }
            return orders;
        }
    }

}
