using AwesomeGICBanking.Domain.Models;
using System.Globalization;

namespace AwesomeGICBanking.Controllers
{
    public class Statements
    { 
        public static void PrintStatements() {
            Console.WriteLine("Please enter account and month to generate the statement <Account> <Year><Month>:");
            Console.Write("> ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return;

            try
            {
                var parts = input.Split(' ');
                if (parts.Length != 2)
                {
                    Console.WriteLine("Invalid input format. Please try again.");
                    return;
                }

                string account = parts[0];
                string yearMonth = parts[1];    
                

                if (!Transactions.accounts.ContainsKey(account))
                {
                    Console.WriteLine("Account does not exist.");
                    return;
                }

                DateTime startDate = DateTime.ParseExact(yearMonth + "01", "yyyyMMdd", CultureInfo.InvariantCulture);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);

                BankAccount bankAccount = Transactions.accounts[account];
                var transactions = bankAccount.Transactions.Where(t => t.Date >= startDate && t.Date <= endDate).ToList();

                Console.WriteLine($"Account: {account}");
                decimal balance = 0;

                foreach (var txn in transactions)
                {
                    balance = txn.Type == "D" ? balance + txn.Amount : balance - txn.Amount;
                    Console.WriteLine($"| {txn.Date:yyyyMMdd} | {txn.TransactionId} | {txn.Type} | {txn.Amount:F2} | {balance:F2} |");
                }

                // Apply interest, assuming last rule is applied for the month and balance stays constant during the period.
                var applicableRules = InterestRules.interestRules.Where(r => r.Date <= endDate).OrderBy(r => r.Date).LastOrDefault();
                if (applicableRules != null)
                {
                    decimal interest = (balance * applicableRules.Rate) / 365;
                    Console.WriteLine($"| {endDate:yyyyMMdd} |         | I    | {interest:F2} | {balance + interest:F2} |");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
