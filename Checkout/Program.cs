using System;
using Checkout;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var pricingRules = new List<IPricingRule>
        {
            new MultiPricingRule("A", 50, 3, 130),
            new MultiPricingRule("B", 30, 2, 45),
            new UnitPricingRule("C", 20),
            new UnitPricingRule("D", 15)
        };

        var checkout = new Checkout.Checkout(pricingRules);
        var items = new[] { "A", "B", "C", "D", "A", "B", "A" };
        foreach (var item in items) checkout.Scan(item);

        Console.WriteLine($"Total: {checkout.GetTotalPrice()}"); // Expected: 210
    }
}
