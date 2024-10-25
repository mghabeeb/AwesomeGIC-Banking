using AwesomeGICBanking.Domain.Models;
using System.Globalization;

namespace AwesomeGICBanking.Controllers
{
    public class Transactions
    {
        public static Dictionary<string, BankAccount> accounts = new Dictionary<string, BankAccount>();
        public static void InputTransactions()
        {
            while (true)
            {
                Console.WriteLine("Please enter transaction details in <Date> <Account> <Type> <Amount> format (or enter blank to go back to main menu):");
                Console.WriteLine("> ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) return;

                try
                {
                    var parts = input.Split(' ');
                    if (parts.Length != 4)
                    {
                        Console.WriteLine("Invalid input format. Please try again.");
                        continue;
                    }

                    DateTime date = DateTime.ParseExact(parts[0], "yyyyMMdd", CultureInfo.InvariantCulture);
                    string account = parts[1];
                    string type = parts[2].ToUpper();
                    decimal amount = decimal.Parse(parts[3]);

                    if (amount < 0)
                    {
                        Console.WriteLine("Amount must be greater than zero.");
                        continue;
                    }

                    if (!accounts.ContainsKey(account))
                    {
                        accounts[account] = new BankAccount { AccountNumber = account };
                    }

                    BankAccount bankAccount = accounts[account];

                    if (type == "W" && bankAccount.Balance < amount)
                    {
                        Console.WriteLine("Insufficient balance for withdrawal.");
                        continue;
                    }

                    // Generate transaction id
                    int txtCount = bankAccount.Transactions.Count(t => t.Date == date) + 1;
                    string transactionId = $"{date:yyyyMMdd}-{txtCount:D2}";

                    Transaction transaction = new Transaction
                    {
                        Date = date,
                        Account = account,
                        Type = type,
                        Amount = amount,
                        TransactionId = transactionId
                    };

                    bankAccount.Transactions.Add(transaction);

                    if (type == "D")
                    {
                        bankAccount.Balance += amount;
                    }

                    if (type == "W")
                    {
                        bankAccount.Balance -= amount;
                    }

                    Console.WriteLine($"Account: {account}");

                    foreach (var txn in bankAccount.Transactions)
                    {
                        Console.WriteLine($"| {txn.Date:yyyyMMdd} | {txn.TransactionId} | {txn.Type} | {txn.Amount:F2} |");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
