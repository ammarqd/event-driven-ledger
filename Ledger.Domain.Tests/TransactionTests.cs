using Xunit;

namespace Ledger.Domain.Tests;

public class TransactionTests
{
    [Fact]
    public void SingleEntry_Throws()
    {
        var entries = new List<Entry> { new(Guid.NewGuid(), new Money(0, "GBP")) };

        Assert.Throws<ArgumentException>(
            () => new Transaction(Guid.NewGuid(), DateTimeOffset.UtcNow, entries));
    }

    [Fact]
    public void BalancedEntries_Succeeds()
    {
        var entries = new[]
        {
            new Entry(Guid.NewGuid(), new Money(-1000, "GBP")),
            new Entry(Guid.NewGuid(), new Money(1000, "GBP")),
        };

        var tx = new Transaction(Guid.NewGuid(), DateTimeOffset.UtcNow, entries);

        Assert.Equal(2, tx.Entries.Count);
    }

    [Fact]
    public void UnbalancedEntries_Throws()
    {
        var entries = new[]
        {
            new Entry(Guid.NewGuid(), new Money(-1000, "GBP")),
            new Entry(Guid.NewGuid(), new Money(900, "GBP")),
        };

        Assert.Throws<ArgumentException>(
            () => new Transaction(Guid.NewGuid(), DateTimeOffset.UtcNow, entries));
    }
}