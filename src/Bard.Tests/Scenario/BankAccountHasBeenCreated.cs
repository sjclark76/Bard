using System;
using Bard;
using Fluent.Testing.Sample.Api.Model;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public class BankAccountHasBeenCreated : Chapter<BankAccount>
    {
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
        
        public DepositMade Deposit_has_been_made(decimal amount)
        {
            return
                Given(() => new Deposit {Amount = amount})
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