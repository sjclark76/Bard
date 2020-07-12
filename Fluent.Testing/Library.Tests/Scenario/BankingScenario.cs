using System;
using Fluent.Testing.Library.Given;
using Fluent.Testing.Sample.Api.Model;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public class BankingScenario : BeginAScenario
    {
        public BankAccountHasBeenCreated BankAccount_has_been_created(Action<BankAccount>? configureBankAccount = null)
        {
            return AddStep<BankAccountHasBeenCreated, BankAccount>((context) =>
            {
                var bankAccount = new BankAccount
                {
                    CustomerName = "Ranulph Fiennes"
                };

                configureBankAccount?.Invoke(bankAccount);

                var response = context.Api.Post("api/bankaccounts", bankAccount);

                return response.Content<BankAccount>();
            });
        }
    }
}