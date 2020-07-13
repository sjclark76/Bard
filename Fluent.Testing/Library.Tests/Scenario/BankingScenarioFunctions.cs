using System;
using Fluent.Testing.Sample.Api.Model;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public static class BankingScenarioFunctions
    {
        public static readonly Func<ScenarioContext, BankAccount, Deposit, BankAccount> MakeADeposit = (context, input, request) =>
        {
            var response = context.Api.Post($"api/bankaccounts/{input.Id}/deposits", new Deposit {Amount = 100});

            return response.Content<BankAccount>();
        };

        public static readonly Func<ScenarioContext, BankAccount, Withdrawal, BankAccount> MakeAWithdrawal =
            (context, input, request) =>
            {
                var response = context.Api.Post($"api/bankaccounts/{input.Id}/withdrawals",
                    request);

                return response.Content<BankAccount>();
            };
    }
}