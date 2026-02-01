using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout
{
    public class PricingRule
    {
        public string Sku { get; }
        public int UnitPrice { get; }
        public int? Quantity { get; }
        public int? SpecialPrice { get; }

        public PricingRule(string sku, int unitPrice, int? quantity = null, int? specialPrice = null)
        {
            Sku = sku;
            UnitPrice = unitPrice;
            Quantity = quantity;
            SpecialPrice = specialPrice;
        }
    }
}