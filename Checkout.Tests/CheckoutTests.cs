using FluentAssertions;
using Checkout;
using System.Collections.Generic;

namespace Checkout.Tests;

public class CheckoutTests
{
    private IEnumerable<PricingRule> CreateStandardPricingRules()
    {
        return new List<PricingRule>
        {
            new PricingRule("A", 50, 3, 130),
            new PricingRule("B", 30, 2, 45),
            new PricingRule("C", 20),
            new PricingRule("D", 15)
        };
    }

    [Fact]
    public void Scan_ValidItem_IncreasesItemCount()
    {
        // Arrange
        var checkout = new Checkout(CreateStandardPricingRules());


        // Act
        checkout.Scan("A");

        // Assert
        checkout.GetItemCount("A").Should().Be(1);
    }

    [Fact]
    public void Scan_UnknownItem_ThrowsInvalidOperationException()
    {
        // Arrange
        var checkout = new Checkout(CreateStandardPricingRules());

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => checkout.Scan("Z"));
    }
}