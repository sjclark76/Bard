using System;
using Bard.Sample.Api.Model;

namespace Bard.Tests.Scenario
{
    public static class BankingScenarioFunctions
    {
        public static readonly Action<ScenarioContext<BankingStoryData>, Deposit> MakeADeposit =
            (context, request) =>
            {
                var response = context.Api.Post($"api/bankaccounts/{context.StoryData.BankAccountId}/deposits",
                    request);
                
                response.ShouldBe.Ok();
            };

        public static readonly Action<ScenarioContext<BankingStoryData>, Withdrawal> MakeAWithdrawal =
            (context, request) =>
            {
                context.Api.Post($"api/bankaccounts/{context.StoryData.BankAccountId}/withdrawals",
                    request);

                context.StoryData.Balance -= request.Amount.GetValueOrDefault();
            };
    }
}