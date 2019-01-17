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
    public class ProductsRepository : IProductsRepository
    {
        public void UpdateProducts(int orderId)
        {
            List<OrderedProducts> orderedProducts = null;
            List<Products> productsList = null;
            using (var context = new ApplicationDbContext())
            {
                IQueryable<OrderedProducts> query = context.OrderedProducts
                    .Where(x => x.Orders.OrderId == orderId)
                    .Include(x => x.Products.ProductsCollections)
                    //.Include(x => x.Products.ProductsCollections)
                    ;

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
                    .Include(x => x.Products.ProductsCollections)
                    //.Include(x => x.Products.ProductsCollections)
                    ;

                list = query.ToList();
            }
            return list;
        }
    }
}
