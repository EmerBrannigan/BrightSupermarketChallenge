namespace Checkout
{
    public interface IPricingRule
    {
        string Sku { get; }
        int CalculatePrice(int quantity);
    }
}
