using Fluent.Testing.Sample.Api.Model;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public class DepositMade : ScenarioStep<BankAccount>
    {
        public DepositMade Deposit_has_been_made(decimal amount)
        {
            return
                Given(() => new Deposit {Amount = 50})
                    .When(BankingScenarioFunctions.MakeADeposit)
                    .Then<DepositMade>();
        }

        public WithdrawalMade Withdrawal_has_been_made(decimal amount)
        {
            return Given(() => new Withdrawal {Amount = amount})
                .When(BankingScenarioFunctions.MakeAWithdrawal)
                .Then<WithdrawalMade>();
        }
    }
}