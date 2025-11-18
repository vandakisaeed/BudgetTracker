namespace BudgetTracker.Models
{
    public enum TransactionType
    {
        Income,
        Expense
    }

    public interface ITransaction
    {
        int Id { get; set; }
        DateTime Timestamp { get; set; }
        TransactionType Type { get; set; }
        string Description { get; set; }
        decimal Amount { get; set; }
    }

    public class Transaction : ITransaction
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
