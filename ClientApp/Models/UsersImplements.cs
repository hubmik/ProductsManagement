using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    emp = await client.EmployeesOrdersAsync(Models.UserCredentials.SessionKey);
                }
            }
            catch(TimeoutException ex)
            {
                throw new Exception(ex.ToString());
            }
            return emp;
        }
    }
}
