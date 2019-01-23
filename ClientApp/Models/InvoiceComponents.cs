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
        public CompanyData DealerAddress { get; set; }
        public List<OrderedProductsStorage> ProductsDataset { get; set; }
        public int TotalValue { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string CustomerCompanyName { get; set; }
        public string CustomerCountry { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerStreet { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}
