using System;
using Checkout;
using System.Collections.Generic;
using System.Linq;

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

        Console.WriteLine("Enter items as comma-separated SKUs (e.g. A,B,C). Enter 'q' or an empty line to exit.");

        while (true)
        {
            Console.Write("\nItems: ");
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line) || line.Trim().Equals("q", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting.");
                break;
            }

            var items = line.Split(new[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => s.Trim().ToUpperInvariant())
                            .ToArray();

            var checkout = new Checkout.Checkout(pricingRules);
            var unknown = new List<string>();

            foreach (var item in items)
            {
                try
                {
                    checkout.Scan(item);
                }
                catch (InvalidOperationException)
                {
                    unknown.Add(item);
                }
            }

            if (unknown.Any()) Console.WriteLine($"Warning: unknown SKUs ignored: {string.Join(", ", unknown)}");

            Console.WriteLine($"Total: {checkout.GetTotalPrice()}");
        }
    }
}
