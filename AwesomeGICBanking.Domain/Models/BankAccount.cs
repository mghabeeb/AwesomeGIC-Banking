namespace AwesomeGICBanking.Domain.Models
{
    public class BankAccount
    {
        public string AccountNumber { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public decimal Balance { get; set; }
    }
}