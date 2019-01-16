using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class UpdatedOrder
    {
        public UpdatedOrder()
        {
            orderid = OrderId;
            orderState = OrderState;
            deliveryDate = DeliveryDate;
        }

        private int orderid;
        private string orderState;
        private DateTime deliveryDate;

        public int OrderId { get; set; }
        public string OrderState { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
