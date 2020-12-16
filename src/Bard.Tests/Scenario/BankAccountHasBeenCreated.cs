using System;
using Bard.Sample.Api;
using Bard.Sample.Api.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Bard.Tests.Scenario
{
    public class BankAccountHasBeenCreated : Chapter<BankingStoryData>
    {
        public BankAccountHasBeenCreated BankAccount_has_been_created_from_db(
            Action<BankAccount>? configureBankAccount = null)
        {
            return When(context =>
                {
                    var bankAccount = new BankAccount
                    {
                        CustomerName = "Ranulph Fiennes"
                    };

                    configureBankAccount?.Invoke(bankAccount);

                    var dbContext = context.Services.GetService<BankDbContext>();

                    dbContext?.BankAccounts.Add(bankAccount);
                    dbContext?.SaveChanges();

                    context.StoryData.BankAccountId = bankAccount.Id;
                })
                .ProceedToChapter<BankAccountHasBeenCreated>();
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

        public EndChapter<BankingStoryData> BankAccount_has_been_updated(Func<BankAccount>? updateBankAccount = null)
        {
            return When(context =>
                {
                    var update = updateBankAccount?.Invoke();

                    context.Api.Put($"api/bankaccounts/{context.StoryData.BankAccountId}", update);
                })
                .End();
        }

        public DepositMade Deposit_has_been_made(Func<BankingStoryData, Deposit> configureDeposit)
        {
            return
                Given(configureDeposit)
                    .When(BankingScenarioFunctions.MakeADeposit)
                    .ProceedToChapter<DepositMade>();
        }

        public WithdrawalMade Withdrawal_has_been_made(Func<BankingStoryData, Withdrawal> modifyRequest)
        {
            return
                Given(modifyRequest)
                    .When(BankingScenarioFunctions.MakeAWithdrawal)
                    .ProceedToChapter<WithdrawalMade>();
        }
    }
}