namespace BudgetTracker.Models
{
    public enum TransactionType
    {
        Income,
        Expense
    }

    public interface ITransaction
    {
        Guid Id { get; set; }
        DateTime Timestamp { get; set; }
        TransactionType Type { get; set; }
        string Description { get; set; }
        decimal Amount { get; set; }
        DateTime Date { get; set; }
    }

    public class Transaction : ITransaction
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        // Date-only value used for file partitioning (date component of Timestamp)
        public DateTime Date { get; set; }
    }
}
