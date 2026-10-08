namespace Ledger.Domain;

public sealed record Money
{
    public long MinorUnits { get; }
    public string Currency { get; }

    public Money(long minorUnits, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
            throw new ArgumentException("Currency must be a 3-letter code", nameof(currency));

        MinorUnits = minorUnits;
        Currency = currency.ToUpperInvariant();
    }

    public static Money Zero(string currency) => new(0, currency);

    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new InvalidOperationException("Cannot add different currencies.");

        return new Money(checked(a.MinorUnits + b.MinorUnits), a.Currency);
    }
}