﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using Model;

namespace CustomerApp.Models
{
    public class OrderContext : OrderEmployeeAssigner
    {
        private readonly int _usrId;

        public OrderContext() => this._usrId = HttpContext.Current.User.Identity.GetUserId<int>();

        public int SaveOrder(ViewModels.CartSummaryViewModel dataToSave)
        {
            using (var context = new ApplicationDbContext())
            {
                int custId;
                DateTime myDateTime = DateTime.Now;
                string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                
                var customer = context.Customers
                    .Where(x => x.UserId == this._usrId)
                    .Select(cid => cid.CustomerId);
                custId = customer.FirstOrDefault();

                Orders order = new Orders
                {                    
                    CustomerId = custId,
                    DeliveryId = dataToSave.SelectedDeliveryId,
                    StatusId = 1,
                    OrderDate = myDateTime,
                    EmployeeId = AssignEmployeeToOrder()
                };

                context.Orders.Add(order);
                context.SaveChanges();

                return order.OrderId;
            }
        }

        public void SetOrderedProducts(Cart orderedProducts, int orderId)
        {
            using (var context = new ApplicationDbContext())
            {
                foreach (var item in orderedProducts.Lines)
                {
                    context.OrderedProducts.Add(new OrderedProducts
                    {
                        OrderId = orderId,
                        ProductId = item.Product.ProductId,
                        Quantity = item.Quantity,                        
                    });
                }
                
                context.SaveChanges();
            }
        }

        public IEnumerable<ApplicationUser> GetOrders()
        {
            IEnumerable<ApplicationUser> orders = null;

            using (var context = new ApplicationDbContext())
            {
                var q = context.Users
                    .Where(x => x.Id == this._usrId)
                    .Include(x => x.Customers.Select(s => s.Orders
                      .Select(em => em.Employees)
                      .Select(or => or.Orders.Select(d => d.Deliveries)
                      .Select(ord => ord.Orders.Select(st => st.OrderStates)
                      ))));

                orders = q.ToList();
            }
            
            return orders;
        }

        public string GetCompanyName()
        {
            string name = "";

            using (var context = new ApplicationDbContext())
            {
                var query = context.Users
                    .Where(x => x.Id == this._usrId)
                    .Include(x=>x.Customers).FirstOrDefault();
                name = query.Customers.FirstOrDefault().CompanyName;
            }

            return name;
        }
    }
}