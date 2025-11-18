using BudgetTracker.Events;
using BudgetTracker.Models;

namespace BudgetTracker.Services;

public class TransactionService
{
    private readonly StorageService _storage;

    public event EventHandler<TransactionAddedEventArgs>? TransactionAdded;

    public TransactionService(StorageService storage)
    {
        _storage = storage;
    }

    public Transaction AddTransaction(TransactionType type, string description, decimal amount, DateTime? timestamp = null)
    {
        var now = timestamp ?? DateTime.UtcNow;
        var tx = new Transaction
        {
            Id = Guid.NewGuid(),
            Timestamp = now,
            Date = now.Date,
            Type = type,
            Description = description,
            Amount = amount
        };

        _storage.SaveTransaction(tx);

        TransactionAdded?.Invoke(this, new TransactionAddedEventArgs(tx));

        return tx;
    }

    public bool RemoveTransaction(Guid id)
    {
        return _storage.DeleteTransaction(id);
    }

    public IEnumerable<Transaction> GetTransactionsBetween(DateTime startInclusive, DateTime endInclusive)
    {
        return _storage.GetTransactionsBetween(startInclusive, endInclusive);
    }
}
