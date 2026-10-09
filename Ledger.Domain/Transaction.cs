namespace Ledger.Domain;

public sealed class Transaction
{
    public Guid Id { get; }
    public DateTimeOffset CreatedAt { get; }
    public IReadOnlyList<Entry> Entries { get; }

    public Transaction(Guid id, DateTimeOffset createdAt, IEnumerable<Entry> entries) {
        var list = entries.ToList();

        if (list.Count < 2) 
            throw new ArgumentException("A transaction needs at least two entries", nameof(entries));

        var total = list.Aggregate(
            Money.Zero(list[0].Amount.Currency),
            (sum, entry) => sum + entry.Amount);

        if (total.MinorUnits != 0) 
            throw new ArgumentException("Entries must sum to zero", nameof(entries));

        Id = id;
        CreatedAt = createdAt;
        Entries = list.AsReadOnly();
    }
}