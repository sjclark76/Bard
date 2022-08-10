using System;
using Bard.Sample.Api.Model;

namespace Bard.Tests.Scenario
{
    public class DepositMade : Chapter<BankingStoryData>
    {
        public DepositMade Deposit_has_been_made(decimal amount)
        {
            return
                Given(_ =>
                    {
                        return new Deposit {Amount = 50};
                    })
                    .When(BankingScenarioFunctions.MakeADeposit)
                    .ProceedToChapter<DepositMade>();
        }

        public WithdrawalMade Withdrawal_has_been_made(decimal amount)
        {
            return Given(_ => new Withdrawal {Amount = amount})
                .When(BankingScenarioFunctions.MakeAWithdrawal)
                .ProceedToChapter<WithdrawalMade>();
        }

        public BankAccountHasBeenCreated BankAccount_has_been_created(Action<BankAccount>? configureBankAccount = null)
        {
            return When(context =>
                {
                    var bankAccount = new BankAccount
                    {
                        CustomerName = "Ranulph Fiennes"
                    };

                    configureBankAccount?.Invoke(bankAccount);

                    context.Api.Post("api/bankaccounts", bankAccount);
                })
                .ProceedToChapter<BankAccountHasBeenCreated>();
        }
    }
}