using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CustomerApp.Models
{
    public class ShippingDetails
    {
        public ApplicationUser GetCustomerData(int id)
        {
            ApplicationUser custData = null;

            using (var context = new ApplicationDbContext())
            {
                IQueryable<ApplicationUser> apUs = context.Users
                    .Where(x => x.Id == id)
                    .Include(x => x.Customers)
                    .Include(x => x.Customers.Select(reg => reg.Region));
                custData = apUs.FirstOrDefault();
            }

            //using (var context = new ApplicationDbContext())
            //{
            //    IQueryable<ApplicationUser> res = context.Users.Where(x => x.Id == id)
            //        .Include(x => x.Customers)
            //        .Include(x => x.Customers.Select(reg => reg.Region));

            //    custData = res.FirstOrDefault();
            //}

            return custData;
        }

        public IEnumerable<Deliveries> GetDeliveries()
        {
            using (var context = new Model.Db())
            {
                IQueryable<Deliveries> res = context.Deliveries;
                return res.ToList();
            }
        }
    }
}