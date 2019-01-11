using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Validations
{
    public class Parser
    {
        public void ParseInput(CustomerApp.Models.Products product, WcfService.ProductExtension productExtension,
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
    }
}
