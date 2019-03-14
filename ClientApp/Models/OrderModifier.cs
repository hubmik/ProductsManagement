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
    public class OrderModifier : IOrderChangeable
    {
        public List<string> GetOrderStates(int orderId)
        {
            List<string> states = null;
            using (var context = new ApplicationDbContext())
            {
                int order = context.Orders
                    .Where(x => x.OrderId == orderId)
                    .Select(x => x.StatusId)
                    .FirstOrDefault();

                if (order != 1)
                {
                    IQueryable<string> lastTwo = context.OrderStates
                        .OrderBy(x => x.StatusId)
                        .Skip(1)
                        .Select(x => x.Status);

                    states = lastTwo.ToList();
                }
                else
                {
                    IQueryable<string> query = context.OrderStates.Select(x => x.Status);
                    states = query.ToList();
                }
            }
            return states;
        }

        public bool IsOrderedStatusChanged(string accessKey)
        {
            using (var context = new ApplicationDbContext())
            {
                IQueryable<Orders> query = context.Orders
                    .Where(x => x.Employees.AccessKey == accessKey)
                    ;
            }
            return false;
        }

        public List<Orders> GetOrders(string accessKey)
        {
            List<Orders> orders = new List<Orders>();

            using (var context = new ApplicationDbContext())
            {
                IQueryable<Orders> query = context.Orders
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
                IQueryable<int> query = context.Orders
                    .Where(x => x.Employees.AccessKey == accessKey)
                    .Include(x => x.OrderStates)
                    .OrderBy(x => x.StatusId)
                    .Select(x => x.OrderId);

                var qr = context.Orders
                    .Where(x => x.Employees.AccessKey == accessKey)
                    .Include(x => x.OrderStates)
                    .OrderBy(x => x.StatusId == 1).ToList();

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

        public virtual void UpdateOrder(UpdatedOrder updatedOrder)
        {
            List<OrderStates> statusIds = GetStatusesId();
            using (var context = new ApplicationDbContext())
            {
                var c = context.Orders
                    .Where(x => x.OrderId == updatedOrder.OrderId)
                    .Include(x => x.OrderStates).FirstOrDefault();

                int id = context.OrderStates
                    .Where(x => x.Status == updatedOrder.OrderState)
                    .Select(x => x.StatusId)
                    .FirstOrDefault();

                c.StatusId = id;
                c.DeliveryDate = updatedOrder.DeliveryDate;
                context.SaveChanges();
            }
        }

        private List<OrderStates> GetStatusesId()
        {
            List<OrderStates> list = new List<OrderStates>();
            using (var context = new ApplicationDbContext())
            {
                IQueryable<OrderStates> q = context.OrderStates;
                list = q.ToList();
            }

            return list;
        }

        public string GetStateOfSpecifiedOrder(int orderId)
        {
            string state = null;

            using (var context = new ApplicationDbContext())
            {
                state = context.Orders
                    .Where(x => x.OrderId == orderId)
                    .Include(x => x.OrderStates)
                    .Select(x => x.OrderStates.Status)
                    .FirstOrDefault();
            }

            return state;
        }

        public string SelectStateForSpecifiedOrder(int orderId)
        {
            string curentStatus = GetStateOfSpecifiedOrder(orderId);
            string valueToSet = null;

            using (var context = new ApplicationDbContext())
            {
                IQueryable<string> c = context.Orders
                    .Where(x => x.OrderId == orderId)
                    .Include(x => x.OrderStates)
                    .Select(x => x.OrderStates.Status);

                valueToSet = c.FirstOrDefault();
            }

            return valueToSet;
        }

        public DateTime GetDeliveryDate(int orderId)
        {
            DateTime? deliveryDate = DateTime.UtcNow;

            using (var context = new ApplicationDbContext())
            {
                deliveryDate = context.Orders
                    .Where(x => x.OrderId == orderId)
                    .Select(x => x.DeliveryDate)
                    .FirstOrDefault();
            }

            if (deliveryDate == null)
                deliveryDate = DateTime.UtcNow;

            return (DateTime)deliveryDate;
        }
    }
}
