using System;
using Fluent.Testing.Library.Given;
using Fluent.Testing.Sample.Api.Model;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public class BankAccountHasBeenCreated : ScenarioStep<BankAccount>
    {
        public DepositMade Deposit_has_been_made(Action<Deposit> createRequest)
        {
            return
                CreateRequest(createRequest)
                    .CallApi(BankingScenarioFunctions.MakeADeposit)
                    .GoToNextStep<DepositMade>();
        }

        public WithdrawalMade Withdrawal_has_been_made(Action<Withdrawal> modifyRequest)
        {
            return
                CreateRequest(modifyRequest)
                    .CallApi(BankingScenarioFunctions.MakeAWithdrawal)
                    .GoToNextStep<WithdrawalMade>();
        }
    }
}