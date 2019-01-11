using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CustomerApp.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public int GetProductsCollectionSize(Products product)
        {
            int quantity;
            using (var context = new ApplicationDbContext())
            {
                var query = context.Products
                    .Where(x => x.ProductId == product.ProductId)
                    .Include(x => x.ProductsCollections)
                    .Select(x => x.ProductsCollections.CollectionSize);
                quantity = query.FirstOrDefault();
            }
            return quantity;
        }

        public void AddItem(Products product, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Product.ProductId == product.ProductId)
                .FirstOrDefault();

            if (line == null)
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            else
                line.Quantity += quantity;
        }

        public void RemoveLine(Products product)
        {
            lineCollection.RemoveAll(p => p.Product.ProductId == product.ProductId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.UnitPrice * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines => lineCollection;
    }
}