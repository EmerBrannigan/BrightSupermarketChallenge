using FluentAssertions;
using Checkout;
using System.Collections.Generic;

namespace Checkout.Tests;

public class CheckoutTests
{
    private IEnumerable<IPricingRule> CreateStandardPricingRules()
    {
        return new List<IPricingRule>
        {
            new MultiPricingRule("A", 50, 3, 130),
            new MultiPricingRule("B", 30, 2, 45),
            new UnitPricingRule("C", 20),
            new UnitPricingRule("D", 15)
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

    [Fact]
    public void GetTotalPrice_EmptyBasket_ReturnsZero()
    {
        var checkout = new Checkout(CreateStandardPricingRules());
        checkout.GetTotalPrice().Should().Be(0);
    }

    [Fact]
    public void GetTotalPrice_AppliesMultiPricing_A()
    {
        var checkout = new Checkout(CreateStandardPricingRules());
        checkout.Scan("A");
        checkout.Scan("A");
        checkout.Scan("A");
        checkout.GetTotalPrice().Should().Be(130);
    }

    [Fact]
    public void GetTotalPrice_AppliesMultiPricing_B()
    {
        var checkout = new Checkout(CreateStandardPricingRules());
        checkout.Scan("B");
        checkout.Scan("B");
        checkout.GetTotalPrice().Should().Be(45);
    }

    [Fact]
    public void GetTotalPrice_MixedBasket_ReturnsExpectedTotal()
    {
        var checkout = new Checkout(CreateStandardPricingRules());
        var items = new[] { "A", "B", "C", "D", "A", "B", "A" };
        foreach (var i in items) checkout.Scan(i);
        // A x3 => 130, B x2 => 45, C => 20, D => 15 => total 210
        checkout.GetTotalPrice().Should().Be(210);
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData("A", 50)]
    [InlineData("A,A,A", 130)]
    [InlineData("B,B", 45)]
    [InlineData("A,A,A,A", 180)]
    [InlineData("A,A,A,A,A,A", 260)]
    [InlineData("A,B,C", 100)]
    public void GetTotalPrice_VariousBaskets_ReturnsExpectedTotal(string itemsCsv, int expected)
    {
        var checkout = new Checkout(CreateStandardPricingRules());
        if (!string.IsNullOrEmpty(itemsCsv))
        {
            foreach (var s in itemsCsv.Split(',')) checkout.Scan(s);
        }

        checkout.GetTotalPrice().Should().Be(expected);
    }
}