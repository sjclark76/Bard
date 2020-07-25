using System;
using Bard;
using Fluent.Testing.Sample.Api.Model;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public static class BankingScenarioFunctions
    {
        public static readonly Func<IScenarioContext, BankAccount, Deposit, BankAccount> MakeADeposit =
            (context, input, request) =>
            {
                var response = context.Api.Post($"api/bankaccounts/{input.Id}/deposits", request);

                return response.Content<BankAccount>();
            };

        public static readonly Func<IScenarioContext, BankAccount, Withdrawal, BankAccount> MakeAWithdrawal =
            (context, input, request) =>
            {
                context.Api.Post($"api/bankaccounts/{input.Id}/withdrawals",
                    request);

                input.Balance -= request.Amount.GetValueOrDefault();

                return input;
            };
    }
}