namespace Ledger.Domain;

public sealed record Entry(Guid AccountId, Money Amount);