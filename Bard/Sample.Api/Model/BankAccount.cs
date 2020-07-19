namespace Fluent.Testing.Sample.Api.Model
{
    public class BankAccount
    {
        public int? Id { get; set; }

        public bool IsActive { get; set; }

        public string? CustomerName { get; set; }

        public decimal Balance { get; set; }

        public void DepositFunds(Deposit deposit)
        {
            Balance += deposit.Amount.GetValueOrDefault();
        }

        public bool CanMake(Withdrawal withdrawal)
        {
            return Balance > withdrawal.Amount.GetValueOrDefault();
        }

        public void WithdrawFunds(Withdrawal withdrawal)
        {
            Balance -= withdrawal.Amount.GetValueOrDefault();
        }
    }
}