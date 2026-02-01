using System;
using System.Collections.Generic;

namespace Checkout;

public class Checkout : ICheckout
{
    private readonly Dictionary<string, PricingRule> _pricingRules;
    private readonly Dictionary<string, int> _scannedItems;

    public Checkout(IEnumerable<PricingRule> pricingRules)
    {
        _pricingRules = pricingRules.ToDictionary(r => r.Sku,
            r => new PricingRule(r.Sku, r.UnitPrice, r.Quantity, r.SpecialPrice));
        _scannedItems = new Dictionary<string, int>();
    }
    public void Scan(string item)
    {
        if (!_pricingRules.ContainsKey(item))
            throw new InvalidOperationException($"Unknown SKU: {item}");

        if (_scannedItems.ContainsKey(item))
        {
            _scannedItems[item]++;
        }
        else
        {
            _scannedItems[item] = 1;
        }
    }

    public int GetItemCount(string item) => _scannedItems.TryGetValue(item, out var c) ? c : 0;

    public int GetTotalPrice()
    {
        var total = 0;

        foreach (var kv in _scannedItems)
        {
            var sku = kv.Key;
            var count = kv.Value;

            if (!_pricingRules.TryGetValue(sku, out var rule))
            {
                // If there's no pricing rule, skip or treat as zero-priced
                continue;
            }

            if (rule.Quantity.HasValue && rule.SpecialPrice.HasValue && rule.Quantity.Value > 0)
            {
                var groups = count / rule.Quantity.Value;
                var remainder = count % rule.Quantity.Value;
                total += groups * rule.SpecialPrice.Value + remainder * rule.UnitPrice;
            }
            else
            {
                total += count * rule.UnitPrice;
            }
        }

        return total;
    }
}
