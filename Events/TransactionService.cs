public class TransactionService
{
    public event EventHandler<TransactionAddedEventArgs>? TransactionAdded;

    public void AddTransaction(Transaction t)
    {
        // Save transaction to repository (not shown here)

        // Fire event
        OnTransactionAdded(t);
    }

    protected virtual void OnTransactionAdded(Transaction t)
    {
        TransactionAdded?.Invoke(
            this,
            new TransactionAddedEventArgs(t)
        );
    }
}
