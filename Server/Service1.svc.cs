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

        public bool IsUserAuthenticated(string accessKey)
        {
            string key = accessKey;
            List<string> list = null;

            using (var context = new ApplicationDbContext())
            {
                IQueryable<string> keysList = context.Employees.Select(x => x.AccessKey);
                list = keysList.ToList();
            }
            if (list.Any(key.Contains))
                return true;

            return false;
        }

        public Employees GetUserCredentials(string accessKey)
        {
            Employees employee = null;
            using (var context = new ApplicationDbContext())
            {
                var query = context.Employees
                    .FirstOrDefault(x => x.AccessKey == accessKey);

                employee = query as Employees;
            }

            return employee;
        }

        public List<Orders> TakeOrders(string accessKey)
        {
            List<Orders> orders = new List<Orders>();
            IQueryable<Orders> query = null;

            using (var context = new ApplicationDbContext())
            {
                //orders = context.Orders;
                    
                    //.Where(x => x.Employees.AccessKey == accessKey)
                    //.Include(x => x.Customers.Select(s => s.Orders
                    //  .Select(em => em.Employees)
                    //  .Select(or => or.Orders.Select(d => d.Deliveries)
                    //  .Select(ord => ord.Orders.Select(st => st.OrderStates)
                    //  ))));

                //orders = query.ToList();
            }

            return orders;
        }

        public List<OrderContext> EmployeesOrders(string accessKey)
        {
            List<OrderContext> orderContext = new List<OrderContext>();
            OrderContext order = new OrderContext();
            string key = accessKey;
            int colIndex;
            using (var db = new System.Data.SqlClient.SqlConnection
                (System.Configuration.ConfigurationManager.ConnectionStrings["CompanyDB"].ConnectionString))
            {
                db.Open();
                using (var cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.Connection = db;
                    cmd.CommandText =
                        "select Orders.OrderId, Orders.DeliveryDate, Orders.OrderDate, Customers.CompanyName, Regions.Country, Regions.City, Regions.Street, Deliveries.DeliveryType, OrderStates.Status " +
                        "from Orders " +
                        "inner join Employees on Orders.EmployeeId = Employees.EmployeeId " +
                        "inner join Customers on Orders.CustomerId = Customers.CustomerId " +
                        "inner join Regions on Customers.RegionId = Regions.RegionId " +
                        "inner join Deliveries on Orders.DeliveryId = Deliveries.DeliveryId " +
                        "inner join OrderStates on Orders.StatusId = OrderStates.StatusId " +
                        "where Employees.AccessKey = @accessKey";
                    cmd.Parameters.AddWithValue("@accessKey", key);
                    
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            colIndex = dr.GetOrdinal("DeliveryDate");
                            order.DeliveryDate = dr.IsDBNull(colIndex) ? null : string.Format("{0:d/M/yyyy}", dr["DeliveryDate"]);

                            orderContext.Add(new OrderContext()
                            {
                                City = (string)dr["City"],
                                CompanyName = (string)dr["CompanyName"],
                                Country = (string)dr["Country"],
                                DeliveryDate = order.DeliveryDate,
                                DeliveryType = (string)dr["DeliveryType"],
                                OrderDate = (DateTime)dr["OrderDate"],
                                OrderId = (int)dr["OrderId"],
                                Status = (string)dr["Status"],
                                Street = (string)dr["Street"]
                            });
                        }
                    }
                }
            }

            return orderContext;
        }
    }

}
