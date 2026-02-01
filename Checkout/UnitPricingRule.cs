using System;

namespace Checkout
{
    public sealed class UnitPricingRule : IPricingRule
    {
        public string Sku { get; }
        public int UnitPrice { get; }

        public UnitPricingRule(string sku, int unitPrice)
        {
            if (string.IsNullOrWhiteSpace(sku)) throw new ArgumentException("SKU must be provided", nameof(sku));
            if (unitPrice < 0) throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price cannot be negative");

            Sku = sku;
            UnitPrice = unitPrice;
        }

        public int CalculatePrice(int quantity) => checked(quantity * UnitPrice);
    }
}
