using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Concrete
{
    public class EFProductRepository : Abstract.IProductRepository
    {
        private Db context = new Db();
        public IEnumerable<Products> Products => context.Products;
    }
}
