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
        public List<Regions> GetCustomerData(int id)
        {
            List<Regions> custData = null;

            using (var context = new ApplicationDbContext())
            {
                IQueryable<Regions> query = context.Regions
                    .Where(x=>x.Customers.UserId == id)
                    .Include(x=>x.Customers)
                    .Include(s=>s.Customers.ApplicationUser);

                custData = query.ToList();
            }

            return custData;
        }

        public IEnumerable<Deliveries> GetDeliveries()
        {
            using (var context = new ApplicationDbContext())
            {
                IQueryable<Deliveries> res = context.Deliveries;
                return res.ToList();
            }
        }
    }
}