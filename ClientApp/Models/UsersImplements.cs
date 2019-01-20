using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClientApp.Models
{
    public class UsersImplements : UserIdentification
    {
        //public async Task<List<Orders>> DownloadOrdersForEmployees(Customers customer)
        //{
        //    List<Orders> orders = null;
        //    using (var client = new WcfService.Service1Client())
        //    {
        //        orders = await client.GetOrdersAsync(UserCredentials.SessionKey);
        //    }

        //    return orders;
        //}

        public async Task<List<Orders>> TransferAllOrders()
        {
            List<Orders> list = null;
            using (var client = new WcfService.Service1Client())
            {
                list = await client.TakeOrdersAsync(UserCredentials.SessionKey);
            }
            return list;
        }

        public async Task<List<WcfService.OrderContext>> OrdersForEmployees()
        {
            List<WcfService.OrderContext> emp = null;
            try
            {
                using (var client = new WcfService.Service1Client())
                {
                    emp = await client.EmployeesOrdersAsync(UserCredentials.SessionKey);
                }
            }
            catch(TimeoutException ex)
            {
                throw new Exception(ex.ToString());
            }
            return emp;
        }
        
        public List<Orders> OrdersForSpecifiedEmployee(string accessKey)
        {
            List<Orders> list = null;

            using (var context = new ApplicationDbContext())
            {
                IQueryable<Orders> query = context.Orders
                    .Where(x => x.Employees.AccessKey == accessKey)
                    .Include(x => x.Customers)
                    .Include(x => x.Deliveries)
                    .Include(x => x.Customers.Regions)
                    .Include(x => x.OrderStates);

                list = query.ToList();
            }

            return list;
        }
    }
}
