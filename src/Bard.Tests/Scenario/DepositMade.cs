using System;
using Bard;
using Fluent.Testing.Sample.Api.Model;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public class DepositMade : Chapter<BankAccount>
    {
        public DepositMade Deposit_has_been_made(decimal amount)
        {
            return
                Given(() => new Deposit {Amount = 50})
                    .When(BankingScenarioFunctions.MakeADeposit)
                    .Then<DepositMade>();
        }

        public WithdrawalMade Withdrawal_has_been_made(decimal amount)
        {
            return Given(() => new Withdrawal {Amount = amount})
                .When(BankingScenarioFunctions.MakeAWithdrawal)
                .Then<WithdrawalMade>();
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

                    var response = context.Api.Post("api/bankaccounts", bankAccount);

                    return response.Content<BankAccount>();
                })
                .Then<BankAccountHasBeenCreated>();
        }
    }
}