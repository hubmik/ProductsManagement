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
