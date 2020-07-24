namespace Fluent.Testing.Sample.Api.Model
{
    public class BankAccount
    {
        public int? Id { get; set; }

        public bool IsActive { get; set; }

        public string? CustomerName { get; set; }

        public decimal Balance { get; set; }

        public void DepositFunds(decimal amount)
        {
            Balance += amount;
        }

        public bool HasFunds(decimal amount)
        {
            return Balance > amount;
        }

        public void WithdrawFunds(decimal amount)
        {
            Balance -= amount;
        }
    }
}