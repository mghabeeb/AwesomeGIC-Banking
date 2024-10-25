using AwesomeGICBanking.Controllers;
using AwesomeGICBanking.Domain.Models;
using System.Globalization;

namespace AwesomeGICBanking
{
    public class Program
    {         
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Welcome to AwesomeGIC Bank! What would you like to do?");
                Console.WriteLine("[T] Input transactions");
                Console.WriteLine("[I] Define interest rules");
                Console.WriteLine("[P] Print statement");
                Console.WriteLine("[Q] Quit");
                Console.Write("> ");

                string option = Console.ReadLine().ToUpper();

                switch(option)
                {
                    case "T":
                        Transactions.InputTransactions();
                        break;
                    case "I":
                        InterestRules.DefineInterestRules();
                        break;
                    case "P":
                        Statements.PrintStatements();
                        break;
                    case "Q":
                        Console.WriteLine("Thank you for banking with AwesomeGIC Bank.");
                        Console.WriteLine("Have a nice day!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

    }
}
