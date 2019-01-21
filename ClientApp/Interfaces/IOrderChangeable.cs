using ClientApp.Models;
using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Interfaces
{
    interface IOrderChangeable
    {
        List<string> GetOrderStates(int orderId);
        List<int> GetOrderIds(string accessKey);
        string GetStateOfSpecifiedOrder(int orderId);
    }
}
