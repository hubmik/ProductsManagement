using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class OrderedProductsStorage
    {
        private string productName;
        private int quantity;
        private decimal orderedValue;

        public OrderedProductsStorage()
        {
            this.productName = ProductName;
            this.quantity = Quantity;
            this.orderedValue = OrderedValue;
        }

        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal OrderedValue { get; set; }

        public decimal GetTotalValue(int orderId)
        {
            decimal totalValue = 0;
            using (var context = new ApplicationDbContext())
            {
                IQueryable<decimal> query = context.Orders
                    .Where(x => x.OrderId == orderId)
                    .Select(x => x.Value);

                totalValue = query.FirstOrDefault();
            }
            return totalValue;
        }
    }
}
