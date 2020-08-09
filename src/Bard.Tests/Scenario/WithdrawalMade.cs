using System;
using Bard.Sample.Api.Model;

namespace Bard.Tests.Scenario
{
    public class WithdrawalMade : Chapter<BankingStoryData>
    {
        public DepositMade Deposit_has_been_made(decimal amount)
        {
            return
                Given(() => new Deposit {Amount = amount})
                    .When(BankingScenarioFunctions.MakeADeposit)
                    .ProceedToChapter<DepositMade>();
        }

        public WithdrawalMade Withdrawal_has_been_made(Func<Withdrawal> modifyRequest)
        {
            return Given(modifyRequest)
                .When(BankingScenarioFunctions.MakeAWithdrawal)
                .ProceedToChapter<WithdrawalMade>();
        }
    }
}