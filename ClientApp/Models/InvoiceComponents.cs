using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class InvoiceComponents
    {
        public InvoiceComponents()
        {
            this.InvoiceDate = DateTime.UtcNow;
        }

        public CompanyData DealerAddress { get; set; }
        public List<OrderedProducts> ProductsDataset { get; set; }
        public int TotalValue { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
    }
}
