using System;

namespace Checkout
{
    public sealed class MultiPricingRule : IPricingRule
    {
        public string Sku { get; }
        public int UnitPrice { get; }
        public int MultiQuantity { get; }
        public int MultiPrice { get; }

        public MultiPricingRule(string sku, int unitPrice, int multiQuantity, int multiPrice)
        {
            if (string.IsNullOrWhiteSpace(sku)) throw new ArgumentException("SKU must be provided", nameof(sku));
            if (unitPrice < 0) throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price cannot be negative");
            if (multiQuantity <= 0) throw new ArgumentOutOfRangeException(nameof(multiQuantity), "Multi quantity must be > 0");
            if (multiPrice < 0) throw new ArgumentOutOfRangeException(nameof(multiPrice), "Multi price cannot be negative");

            Sku = sku;
            UnitPrice = unitPrice;
            MultiQuantity = multiQuantity;
            MultiPrice = multiPrice;
        }

        public int CalculatePrice(int quantity)
        {
            var groups = quantity / MultiQuantity;
            var remainder = quantity % MultiQuantity;
            return checked(groups * MultiPrice + remainder * UnitPrice);
        }
    }
}
