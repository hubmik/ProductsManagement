using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CustomerApp.Models
{
    public class ShippingDetails
    {
        public int CustomerId { get; set; }
        public int RegionId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public async void InitializeCustomerData()
        {
            using(var client = new WcfService.Service1Client())
            {
                var clientDetail = await client.GetCustomerDataAsync(1);

            }
        }
    }
}