using System;
using Bard.Internal;
using Fluent.Testing.Sample.Api.Model;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public static class BankingScenarioFunctions
    {
        public static readonly Func<ScenarioContext<BankAccount>, Deposit, BankAccount> MakeADeposit =
            (context, request) =>
            {
                var response = context.Api.Post($"api/bankaccounts/{context.StoryInput?.Id}/deposits", request);

                return response.Content<BankAccount>();
            };

        public static readonly Func<ScenarioContext<BankAccount>, Withdrawal, BankAccount> MakeAWithdrawal =
            (context, request) =>
            {
                context.Api.Post($"api/bankaccounts/{context.StoryInput?.Id}/withdrawals",
                    request);
                
                context.StoryInput.Balance -= request.Amount.GetValueOrDefault();

                return context.StoryInput;
            };
    }
}