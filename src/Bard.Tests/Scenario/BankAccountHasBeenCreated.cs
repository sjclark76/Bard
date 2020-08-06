using System;
using Bard;
using Fluent.Testing.Sample.Api.Model;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public class BankAccountHasBeenCreated : Chapter<BankingStoryData>
    {
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
                .Then<BankAccountHasBeenCreated>();
        }

        public EndChapter<BankingStoryData> BankAccount_has_been_updated(Func<BankAccount>? updateBankAccount = null)
        {
            return When(context =>
                {
                    var update = updateBankAccount?.Invoke();

                    context.Api.Put($"api/bankaccounts/{context.StoryData.BankAccountId}", update);
                })
                .End();
        }

        public DepositMade Deposit_has_been_made(Func<Deposit> configureDeposit)
        {
            return
                Given(configureDeposit)
                    .When(BankingScenarioFunctions.MakeADeposit)
                    .Then<DepositMade>();
        }

        public WithdrawalMade Withdrawal_has_been_made(Func<Withdrawal> modifyRequest)
        {
            return
                Given(modifyRequest)
                    .When(BankingScenarioFunctions.MakeAWithdrawal)
                    .Then<WithdrawalMade>();
        }
    }
}