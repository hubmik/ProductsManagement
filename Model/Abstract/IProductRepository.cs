using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<CustomerApp.Models.Products> Products { get; }
    }
}
