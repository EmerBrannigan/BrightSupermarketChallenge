# BrightSupermarketChallenge
A small library that calculates the total amount at checkout, applying unit and multi-buy discounts (discounts are assigned to Stock Keeping Units / SKUs).

---

## Quick Start ✅

### Prerequisites
- **.NET 8 SDK** (or newer). Verify with:

```bash
dotnet --version
```
- **git** (to clone the repo)

### Clone the repository

```bash
git clone https://your-repo-url.git
cd BrightSupermarketChallenge
```

> Note: replace `https://your-repo-url.git` with the repository URL or use your local path.

### Build the solution

From repository root:

```bash
dotnet build
```

### Run the unit tests

The project includes a comprehensive test suite demonstrating expected behaviour. Run:

```bash
dotnet test
```

You should see all tests pass. Example behaviours covered by tests:
- Multi-buy discounts (e.g., 3 for a fixed price)
- Standard unit pricing
- Mixed baskets with correct totals

### Example: Try the library in a small console app

3. Run the Project:

```bash
dotnet run --project Checkout
```

You can input various different SKUs to get the total calculated.

---

## Project Structure 🔧
- `Checkout/` — core library containing `Checkout`, `IPricingRule`, `UnitPricingRule`, `MultiPricingRule`.
- `Checkout.Tests/` — xUnit test project covering behaviour and edge cases.

