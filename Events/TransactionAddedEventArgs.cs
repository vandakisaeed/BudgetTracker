
using BudgetTracker.Models;
using System.Transactions;
namespace BudgetTracker.Events
{
    public class TransactionAddedEventArgs : EventArgs
    {
        public Transaction Transaction { get; }

        public TransactionAddedEventArgs(Transaction transaction)
        {
            Transaction = transaction;
        }
    }

}
