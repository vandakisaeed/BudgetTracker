using System.Text.Json;
using BudgetTracker.Models;

namespace BudgetTracker.Services
{
    public class StorageService
    {
        private readonly string _baseDirectory;

        public StorageService(string baseDirectory)
        {
            _baseDirectory = baseDirectory;

            // Create the directory if it does not exist
            if (!Directory.Exists(_baseDirectory))
            {
                Directory.CreateDirectory(_baseDirectory);
            }
        }

        // Save transaction to a JSON file for its date
        public void SaveTransaction(ITransaction transaction)
        {
            string path = GetFilePath(transaction.Date);

            string json = JsonSerializer.Serialize(transaction, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.AppendAllText(path, json + Environment.NewLine);
        }

        // Read all transactions for a specific date
        public List<ITransaction>? ReadTransactions(DateTime date)
        {
            string path = GetFilePath(date);

            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist.");
                return null;
            }

            var lines = File.ReadAllLines(path);
            var results = new List<ITransaction>();

            foreach (var line in lines)
            {
                var item = JsonSerializer.Deserialize<Transaction>(line);
                if (item != null)
                    results.Add(item);
            }

            return results;
        }

        // Build file path based on date
        private string GetFilePath(DateTime date)
        {
            string fileName = date.ToString("yyyy-MM-dd") + ".json";
            return Path.Combine(_baseDirectory, fileName);
        }
    }
}
