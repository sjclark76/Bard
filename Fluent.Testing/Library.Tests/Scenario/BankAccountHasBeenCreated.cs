using System;
using Fluent.Testing.Library.Given;
using Fluent.Testing.Sample.Api.Model;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public static class BankingScenarioFunctions
    {
        public static readonly ScenarioStepAction<BankAccount, BankAccount> MakeADeposit = (context, input) =>
        {
            var response = context.Api.Post($"api/bankaccounts/{input.Id}/deposits", new Deposit {Amount = 100});

            return response.Content<BankAccount>();
        };
        
        public static readonly ScenarioStepAction<BankAccount, BankAccount> MakeAWithdrawal = (context, input) =>
        {
            var response = context.Api.Post($"api/bankaccounts/{input.Id}/withdrawals",
                new Withdrawal {Amount = 100});

            return response.Content<BankAccount>();
        };
    }
    public class BankAccountHasBeenCreated : ScenarioStep<BankAccount>
    {
        public DepositMade Deposit_has_been_made(Action<BankAccount>? modifyRequest = null)
        {
            return AddStep<DepositMade, BankAccount>(BankingScenarioFunctions.MakeADeposit);
        }

        public WithdrawalMade Withdrawal_has_been_made()
        {
            return AddStep<WithdrawalMade, BankAccount>(BankingScenarioFunctions.MakeAWithdrawal);
        }
    }

    public class DepositMade : ScenarioStep<BankAccount>
    {
        public DepositMade Deposit_has_been_made()
        {
            return AddStep<DepositMade, BankAccount>(BankingScenarioFunctions.MakeADeposit);
        }

        public WithdrawalMade Withdrawal_has_been_made()
        {
            return AddStep<WithdrawalMade, BankAccount>(BankingScenarioFunctions.MakeAWithdrawal);
        }
    }

    public class WithdrawalMade : ScenarioStep<BankAccount>
    {
        public DepositMade Deposit_has_been_made()
        {
            return AddStep<DepositMade, BankAccount>(BankingScenarioFunctions.MakeADeposit);
        }

        public WithdrawalMade Withdrawal_has_been_made()
        {
            return AddStep<WithdrawalMade, BankAccount>(BankingScenarioFunctions.MakeAWithdrawal);
        }
    }
}