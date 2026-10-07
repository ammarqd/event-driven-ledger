using Ledger.Domain;
using Xunit;

namespace Ledger.Domain.Tests;

public class MoneyTests
{
    [Fact]
    public void Constructor_StoresAmountAndCurrency()
    {
        var money = new Money(1050, "GBP");

        Assert.Equal(1050, money.MinorUnits);
        Assert.Equal("GBP", money.Currency);
    }

    [Fact]
    public void Constructor_RejectsTwoLetterCurrency()
    {
        Assert.Throws<ArgumentException>(() => new Money(100, "GB"));
    }
}