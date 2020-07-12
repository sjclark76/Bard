using System;
using Fluent.Testing.Library.Given;
using Fluent.Testing.Library.Tests.POST;
using Fluent.Testing.Sample.Api;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public class BankingScenario: BeginAScenario
    {
        public BankAccountHasBeenCreated BankAccount_has_been_created(Action<BankAccount>? configureBankAccount = null)
        {
            return AddStep<BankAccountHasBeenCreated, BankAccount>(() =>
            {
                var bankAccount = new BankAccount
                {
                    CustomerName = "Ranulph Fiennes"
                };

                configureBankAccount?.Invoke(bankAccount);

                var response = Context.Api.Post("api/bankaccount", bankAccount);

                return response.Content<BankAccount>();
            });
        }
    }
}