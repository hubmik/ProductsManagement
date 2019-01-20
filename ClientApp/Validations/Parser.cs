using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClientApp.Validations
{
    public class Parser
    {
        public void ParseInput(Products product, WcfService.ProductExtension productExtension,
            string name,
            string quantity,
            string unitPrice,
            string quantityFrom,
            string quantityTo,
            string unitPriceFrom,
            string unitPriceTo)
        {
            product.ProductName = name;
            if (int.TryParse(quantity, out int res))
                product.Quantity = int.Parse(quantity);
            if (decimal.TryParse(unitPrice, out decimal result))
                product.UnitPrice = decimal.Parse(unitPrice);
            if (int.TryParse(quantityFrom, out res))
                productExtension.QuantityFrom = int.Parse(quantityFrom);
            if (int.TryParse(quantityTo, out res))
                productExtension.QuantityTo = int.Parse(quantityTo);
            if (decimal.TryParse(unitPriceFrom, out result))
                productExtension.UnitPriceFrom = decimal.Parse(unitPriceFrom);
            if (decimal.TryParse(unitPriceTo, out result))
                productExtension.UnitPriceTo = decimal.Parse(unitPriceTo);
        }

        public Products ParseInput(string name, string quantity, string price, int size, bool update)
        {
            Products product = new Products();

            if (string.IsNullOrWhiteSpace(name) || size <= 0)
                return null;
            if (string.IsNullOrWhiteSpace(quantity) && string.IsNullOrWhiteSpace(price))
                return null;

            if (!string.IsNullOrWhiteSpace(quantity))
                product.Quantity = int.Parse(quantity);
            if (!string.IsNullOrWhiteSpace(price))
                product.UnitPrice = decimal.Parse(price);

            product.ProductName = name;

            using (var context = new ApplicationDbContext())
            {
                var q = context.Products
                    .Where(x => x.ProductsCollections.CollectionSize == size)
                    .Select(x => x.CollectionId);
                product.CollectionId = q.FirstOrDefault();
                product.ProductId = context.Products
                    .Where(x => x.ProductName == product.ProductName)
                    .Select(x => x.ProductId)
                    .FirstOrDefault();

                if (product.UnitPrice <= 0)
                    product.UnitPrice = context.Products.Where(x => x.ProductId == product.ProductId).Select(x => x.UnitPrice).FirstOrDefault();
                if(product.Quantity <= 0)
                    product.Quantity = context.Products.Where(x => x.ProductId == product.ProductId).Select(x => x.Quantity).FirstOrDefault();
            }
            
            return product;
        }

        public Products ParseInput(string name, string quantity, string price, int size)
        {
            Products product = new Products();
            bool parseSuccedded = false;
            int productsCollectionId;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(quantity) || string.IsNullOrWhiteSpace(price) || size <= 0)
                return null;
            
            if (parseSuccedded = int.TryParse(quantity, out int quantRes))
                product.Quantity = int.Parse(quantity);
            if (parseSuccedded = decimal.TryParse(price, out decimal priceRes))
                product.UnitPrice = decimal.Parse(price);

            using (var context = new ApplicationDbContext())
            {
                productsCollectionId = context.Products
                    .Where(x => x.ProductsCollections.CollectionSize == size)
                    .Select(x => x.ProductsCollections.CollectionId).FirstOrDefault();
            }

            product.CollectionId = productsCollectionId;
            if (product.CollectionId == 0)
                parseSuccedded = false;
            product.ProductName = name;

            if (parseSuccedded == false)
                return null;
            return product;
        }
    }
}
