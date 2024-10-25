using AwesomeGICBanking.Domain.Models;
using System.Globalization;

namespace AwesomeGICBanking.Controllers
{
    public class InterestRules
    {
        public static List<InterestRule> interestRules = new List<InterestRule>();
        public static void DefineInterestRules() {
            while (true)
            {
                Console.WriteLine("Please enter interest rules details in <Date> <RuleId> <Rate in %> format (or enter blank to go back to main menu):");
                Console.Write("> ");
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) return;

                try
                {
                    var parts = input.Split(' ');
                    if (parts.Length != 3)
                    {
                        Console.WriteLine("Invalid input format. Please try again.");
                        continue;
                    }

                    DateTime date = DateTime.ParseExact(parts[0], "yyyyMMdd", CultureInfo.InvariantCulture);
                    string ruleId = parts[1];
                    decimal rate = decimal.Parse(parts[2]);

                    if (rate <= 0 || rate >= 100)
                    {
                        Console.WriteLine("Rate must be between 0 and 100.");
                        continue;
                    }

                    // Remove existing rules for same date
                    interestRules.RemoveAll(r => r.Date == date);

                    InterestRule rule = new InterestRule
                    {
                        Date = date,
                        RuleId = ruleId,
                        Rate = rate
                    };

                    interestRules.Add(rule);
                    interestRules = interestRules.OrderBy(r => r.Date).ToList();

                    Console.WriteLine("Interest rules:");
                    foreach (var r in interestRules)
                    {
                        Console.WriteLine($"| {r.Date:yyyyMMdd} | {r.RuleId} | {r.Rate:F2}");
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
