using System;
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

        private bool AddAddress(ViewModels.CartSummaryViewModel data)
        {
            List<Regions> list = null;
            using (var context = new ApplicationDbContext())
            {
                IQueryable<Regions> query = context.Regions
                    .Where(x => x.Customers.UserId == SessionProcess.SessionIdentifier);

                list = query.ToList();
            }
            foreach (var item in list)
            {
                if (item.City == data.City && item.Country == data.Country && item.Street == data.Street)
                    return false;
            }
            return true;
        }

        private int InsertRegion(ViewModels.CartSummaryViewModel data)
        {
            using (var context = new ApplicationDbContext())
            {
                Regions region = new Regions
                {
                    City = data.City,
                    Country = data.Country,
                    IsDefault = false,
                    Street = data.Street,
                    CustomerId = context.Customers
                        .Where(x => x.UserId == SessionProcess.SessionIdentifier)
                        .Select(x => x.CustomerId)
                        .FirstOrDefault()
                };
                context.Regions.Add(region);
                context.SaveChanges();

                return region.RegionId;
            }
        }

        public int SaveOrder(ViewModels.CartSummaryViewModel dataToSave, Cart cart)
        {
            int regionId = 0;
            bool isAddressAdded = false;
            if (isAddressAdded = AddAddress(dataToSave))
                regionId = InsertRegion(dataToSave);

            using (var context = new ApplicationDbContext())
            {
                int custId;
                DateTime myDateTime = DateTime.Now;
                string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Orders order = null;

                var customer = context.Customers
                    .Where(x => x.UserId == this._usrId)
                    .Select(cid => cid.CustomerId);
                custId = customer.FirstOrDefault();

                if (isAddressAdded)
                {
                    order = new Orders
                    {
                        CustomerId = custId,
                        DeliveryId = dataToSave.SelectedDeliveryId,
                        StatusId = 1,
                        OrderDate = myDateTime,
                        EmployeeId = AssignEmployeeToOrder(),
                        RegionId = regionId,
                        Value = cart.ComputeTotalValue()
                    };
                }
                else
                {
                    order = new Orders
                    {
                        CustomerId = custId,
                        DeliveryId = dataToSave.SelectedDeliveryId,
                        StatusId = 1,
                        OrderDate = myDateTime,
                        EmployeeId = AssignEmployeeToOrder(),
                        RegionId = dataToSave.RegionId,
                        Value = cart.ComputeTotalValue()
                    };
                }

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

        public IEnumerable<Orders> GetOrders(bool isOrderedChecked, bool isAcceptedChecked)
        {
            IEnumerable<Orders> orders = null;

            using (var context = new ApplicationDbContext())
            {
                IQueryable<Orders> query = null;
                if (isAcceptedChecked && isOrderedChecked || !isAcceptedChecked && !isOrderedChecked)
                {
                    query = context.Orders
                        .Where(x => x.Customers.UserId == this._usrId)
                        .Include(x => x.Employees)
                        .Include(x => x.OrderStates)
                        .Include(x => x.Deliveries)
                        .OrderBy(x=>x.OrderDate);
                }
                else if (isOrderedChecked)
                {
                    query = context.Orders
                        .Where(x => x.Customers.UserId == this._usrId && x.StatusId == 1)
                        .Include(x => x.Employees)
                        .Include(x => x.OrderStates)
                        .Include(x => x.Deliveries)
                        .OrderBy(x=>x.OrderDate);
                }
                else if (isAcceptedChecked)
                {
                    query = context.Orders
                        .Where(x => x.Customers.UserId == this._usrId && x.StatusId == 2)
                        .Include(x => x.Employees)
                        .Include(x => x.OrderStates)
                        .Include(x => x.Deliveries)
                        .OrderBy(x=>x.OrderDate);
                }

                orders = query.ToList();
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