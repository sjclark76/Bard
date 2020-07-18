using System;
using Fluent.Testing.Library.Given;
using Fluent.Testing.Sample.Api.Model;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public class WithdrawalMade : ScenarioStep<BankAccount>
    {
        public DepositMade Deposit_has_been_made(decimal amount)
        {
            return
                Given(() => new Deposit{Amount = amount})
                    .When(BankingScenarioFunctions.MakeADeposit)
                    .Then<DepositMade>();
        }

        public WithdrawalMade Withdrawal_has_been_made(Func<Withdrawal> modifyRequest)
        {
            return Given(modifyRequest)
                .When(BankingScenarioFunctions.MakeAWithdrawal)
                .Then<WithdrawalMade>();
        }
    }
}