using System;
using Bard;
using Fluent.Testing.Sample.Api.Model;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public class BankAccountHasBeenCreated : Chapter<BankAccount>
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

                    var response = context.Api.Post("api/bankaccounts", bankAccount);

                    return response.Content<BankAccount>();
                })
                .Then<BankAccountHasBeenCreated>();
        }
        
        public EndChapter<BankAccount> BankAccount_has_been_updated(Action<BankAccount>? updateBankAccount = null)
        {
            return When(context =>
            {
                var update = context.StoryInput;
                
                updateBankAccount?.Invoke(update);

                context.Api.Put($"api/bankaccounts/{context.StoryInput.Id}", update);
                
                return update;
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