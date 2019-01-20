using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Interfaces
{
    interface IProductsRepository
    {
        void UpdateProducts(int orderId);
        bool UpdateProducts(Products product);
        bool InsertProducts(Products product);
    }
}
