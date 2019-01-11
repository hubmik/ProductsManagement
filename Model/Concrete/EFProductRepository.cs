using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Concrete
{
    public class EFProductRepository : Abstract.IProductRepository
    {
        private CustomerApp.Models.ApplicationDbContext context = new CustomerApp.Models.ApplicationDbContext();
        public IEnumerable<CustomerApp.Models.Products> Products => context.Products;
    }
}
