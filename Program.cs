using BudgetTracker.Models;
using BudgetTracker.Services;

// Top-level program for BudgetTracker
const string DataDir = "data";
const string LogsDir = "logs";

var storage = new StorageService(DataDir);
var txService = new TransactionService(storage);
var logger = new LoggerService(LogsDir, txService);

bool running = true;
while (running)
{
    Console.WriteLine("\nBudget Tracker — Menu");
    Console.WriteLine("1) Add transaction");
    Console.WriteLine("2) Remove transaction by Id");
    Console.WriteLine("3) Generate report (date range)");
    Console.WriteLine("4) Exit");
    Console.Write("Select> ");
    var line = Console.ReadLine();
    switch (line)
    {
        case "1":
            AddTransactionFlow(txService);
            break;
        case "2":
            RemoveTransactionFlow(txService);
            break;
        case "3":
            ReportFlow(txService);
            break;
        case "4":
            running = false;
            break;
        default:
            Console.WriteLine("Unknown option");
            break;
    }
}

static void AddTransactionFlow(TransactionService svc)
{
    Console.Write("Type (income/expense): ");
    var typeRaw = Console.ReadLine()?.Trim().ToLower();
    if (typeRaw != "income" && typeRaw != "expense")
    {
        Console.WriteLine("Invalid type");
        return;
    }

    Console.Write("Description: ");
    var desc = Console.ReadLine() ?? string.Empty;

    Console.Write("Amount: ");
    if (!decimal.TryParse(Console.ReadLine(), out var amount))
    {
        Console.WriteLine("Invalid amount");
        return;
    }

    var type = typeRaw == "income" ? TransactionType.Income : TransactionType.Expense;
    var tx = svc.AddTransaction(type, desc, amount);
    Console.WriteLine($"Added transaction: Id={tx.Id}");
}

static void RemoveTransactionFlow(TransactionService svc)
{
    Console.Write("Transaction Id (GUID): ");
    var raw = Console.ReadLine();
    if (!Guid.TryParse(raw, out var id))
    {
        Console.WriteLine("Invalid GUID");
        return;
    }

    var ok = svc.RemoveTransaction(id);
    Console.WriteLine(ok ? "Deleted." : "Not found.");
}

static void ReportFlow(TransactionService svc)
{
    Console.Write("Start date (yyyy-mm-dd): ");
    if (!DateTime.TryParse(Console.ReadLine(), out var start))
    {
        Console.WriteLine("Invalid date");
        return;
    }
    Console.Write("End date (yyyy-mm-dd): ");
    if (!DateTime.TryParse(Console.ReadLine(), out var end))
    {
        Console.WriteLine("Invalid date");
        return;
    }

    var list = svc.GetTransactionsBetween(start.Date, end.Date).ToList();
    Console.WriteLine($"Found {list.Count} transactions between {start:yyyy-MM-dd} and {end:yyyy-MM-dd}");
    var incomes = list.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
    var expenses = list.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
    Console.WriteLine($"Total Income: {incomes:C}");
    Console.WriteLine($"Total Expense: {expenses:C}");
    Console.WriteLine($"Net: {(incomes - expenses):C}");
}
