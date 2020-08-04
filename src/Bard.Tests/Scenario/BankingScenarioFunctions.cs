using System;
using Bard;
using Fluent.Testing.Sample.Api.Model;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public static class BankingScenarioFunctions
    {
        public static readonly Func<ScenarioContext<BankAccount>, Deposit, BankAccount> MakeADeposit =
            (context, request) =>
            {
                var response = context.Api.Post($"api/bankaccounts/{context.StoryData?.Id}/deposits", request);

                return response.Content<BankAccount>();
            };

        public static readonly Func<ScenarioContext<BankAccount>, Withdrawal, BankAccount> MakeAWithdrawal =
            (context, request) =>
            {
                context.Api.Post($"api/bankaccounts/{context.StoryData.Id}/withdrawals",
                    request);
                
                context.StoryData.Balance -= request.Amount.GetValueOrDefault();

                return context.StoryData;
            };
    }
}