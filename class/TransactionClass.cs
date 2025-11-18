using System;
using System.Collections.Generic;
using System.Linq; // Needed for RemoveAll/FirstOrDefault

using BudgetTracker.Models;
public class TransactionClass
{
    // Initialize the list using the standard C# syntax
    private readonly List<ITransaction> _listTransaction = [];



    public ITransaction AddTransaction(
        int Id,
        DateTime Timestamp,
        TransactionType Type,
        string Description,
        decimal Amount)
    {
        // Check for duplicate ID *before* creating and adding the new transaction.
        if (_listTransaction.Any(t => t.Id == Id))
        {
            Console.WriteLine($"Error: Transaction ID {Id} already exists.");
            return null; // Indicate failure
        }

        // 1. Create a *new* object for the transaction being added.
        var newTransaction = new Transaction
        {
            Id = Id,
            Timestamp = Timestamp,
            Type = Type,
            Description = Description,
            Amount = Amount
        };

        // 2. Add the new object to the list.
        _listTransaction.Add(newTransaction);
        Console.WriteLine($"Transaction {Id} added successfully.");

        return newTransaction;
    }

    public bool DeleteTransaction(int Id)
    {
        // Use LINQ's RemoveAll to efficiently remove items matching the condition.
        // It returns the count of elements removed.
        int removedCount = _listTransaction.RemoveAll(t => t.Id == Id);

        if (removedCount > 0)
        {
            Console.WriteLine($"Transaction ID {Id} deleted.");
            return true;
        }
        else
        {
            Console.WriteLine($"Error: Transaction ID {Id} not found.");
            return false;
        }
    }

    // Optional: A getter to view the list of transactions
    public IReadOnlyList<ITransaction> ListTransactions => _listTransaction;
}