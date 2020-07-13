using System;
using Fluent.Testing.Library.Given;
using Fluent.Testing.Sample.Api.Model;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public class BankAccountHasBeenCreated : ScenarioStep<BankAccount>
    {
        public DepositMade Deposit_has_been_made(Action<Deposit>? modifyRequest = null)
        {
            return
                ForRequest(modifyRequest)
                    .Returns(BankingScenarioFunctions.MakeADeposit)
                    .ContinueTo<DepositMade>();
        }

        public WithdrawalMade Withdrawal_has_been_made(Action<Withdrawal>? modifyRequest = null)
        {
            return
                ForRequest(modifyRequest)
                    .Returns(BankingScenarioFunctions.MakeAWithdrawal)
                    .ContinueTo<WithdrawalMade>();
        }
    }
}