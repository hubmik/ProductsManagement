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
    class OrderModifier : IOrderChangeable
    {
        public List<string> GetOrderStates()
        {
            List<string> states = null;
            using (var context = new ApplicationDbContext())
            {
                var query = context.OrderStates.Select(x => x.Status);
                states = query.ToList();
            }
            return states;
        }

        public List<Orders> GetOrders(string accessKey)
        {
            List<Orders> orders = new List<Orders>();

            using (var context = new ApplicationDbContext())
            {
                var query = context.Orders
                    .Where(x => x.Employees.AccessKey == accessKey)
                    .Include(x => x.OrderStates);
                orders = query.ToList();
            }

            return orders;
        }

        public List<int> GetOrderIds(string accessKey)
        {
            List<int> orderIds = null;
            using (var context = new ApplicationDbContext())
            {
                IQueryable<int> query = context.Orders.Where(x => x.Employees.AccessKey == accessKey).Select(x=>x.OrderId);
                orderIds = query.ToList();
            }
            return orderIds;
        }

        public List<OrderedProductsStorage> GetOrderedProducts(int orderId)
        {
            List<OrderedProductsStorage> orderedProducts = new List<OrderedProductsStorage>();

            using (var context = new ApplicationDbContext())
            {
                IQueryable<OrderedProductsStorage> query = context.OrderedProducts
                    .Where(x => x.Orders.OrderId == orderId)
                    .Include(x => x.Products)
                    .Select(x => new OrderedProductsStorage
                    {
                        ProductName = x.Products.ProductName,
                        OrderedValue = x.Quantity * x.Products.UnitPrice,
                        Quantity = x.Quantity
                    });

                orderedProducts = query.ToList();
            }

            return orderedProducts;
        }

        public void UpdateOrder()
        {

        }
    }
}
