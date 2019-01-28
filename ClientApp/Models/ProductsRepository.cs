using ClientApp.Interfaces;
using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClientApp.Models
{
    public class ProductsRepository : ProductExtension, IProductsRepository
    {
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
            }

            return products;
        }

        public void UpdateProducts(int orderId)
        {
            List<OrderedProducts> orderedProducts = null;
            using (var context = new ApplicationDbContext())
            {
                IQueryable<OrderedProducts> query = context.OrderedProducts
                    .Where(x => x.Orders.OrderId == orderId)
                    .Include(x => x.Products.ProductsCollections);
                    
                orderedProducts = query.ToList();
                foreach (var item in orderedProducts)
                {
                    item.Products.Quantity -= item.Quantity / item.Products.ProductsCollections.CollectionSize;
                }
                context.SaveChanges();
            }
        }

        public List<OrderedProducts> QueryUpdateProducts(int orderId)
        {
            List<OrderedProducts> list = new List<OrderedProducts>();
            using (var context = new ApplicationDbContext())
            {
                IQueryable<OrderedProducts> query = context.OrderedProducts
                    .Where(x => x.Orders.OrderId == orderId)
                    .Include(x => x.Products.ProductsCollections);                    

                list = query.ToList();
            }
            return list;
        }

        public bool InsertProducts(Products productToInsert)
        {
            bool isChangesCommited = false;
            List<string> productsList = null;
            Products product;

            using (var context = new ApplicationDbContext())
            {
                IQueryable<string> productsQuery = context.Products.Select(x => x.ProductName);
                productsList = productsQuery.ToList();
                if (productsList.Contains(productToInsert.ProductName))
                    return false;
                
                product = new Products()
                {
                    ProductName = productToInsert.ProductName,
                    UnitPrice = productToInsert.UnitPrice,
                    Quantity = productToInsert.Quantity,
                    CollectionId = productToInsert.CollectionId
                };

                context.Products.Add(product);
                context.SaveChanges();
                isChangesCommited = true;
            }
            if (isChangesCommited)
                return true;
            else
                return false;
        }

        public List<int> GetCollections()
        {
            List<int> list = null;
            using (var db = new ApplicationDbContext())
            {
                IQueryable<int> query = db.ProductsCollections.Select(x => x.CollectionSize);
                list = query.ToList();
            }
            return list;
        }

        public List<string> GetProductNames()
        {
            List<string> list = null;
            using (var context = new ApplicationDbContext())
            {
                IQueryable<string> query = context.Products
                    .Select(x=>x.ProductName);
                list = query.ToList();
            }
            return list;
        }

        public bool UpdateProducts(Products product)
        {
            Products prd = null;
            bool isSuccess = false;
            using (var context = new ApplicationDbContext())
            {
                var prod = context.Products
                    .Where(x => x.ProductId == product.ProductId);
                prd = prod.FirstOrDefault();
                prd.ProductName = product.ProductName;
                prd.Quantity = product.Quantity;
                prd.UnitPrice = product.UnitPrice;
                prd.CollectionId = product.CollectionId;
                context.SaveChanges();
                isSuccess = true;
            }

            if (isSuccess)
                return true;
            else
                return false;
        }
    }
}
