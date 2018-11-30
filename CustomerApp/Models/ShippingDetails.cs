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
        public List<Model.Customers> CustomerData { get; set; }

        private async Task InitializeCustomerData()
        {
            //List<Model.Customers> clientDetail;
            var client = new WcfService.Service1Client();
            CustomerData = await client.GetCustomerDataAsync(1);

        }

        public async Task<Model.Customers> PassCustomerData()
        {
            await InitializeCustomerData();
            return CustomerData.FirstOrDefault();
        }
    }
}