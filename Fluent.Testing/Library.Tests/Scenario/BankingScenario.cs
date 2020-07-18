using System;
using Fluent.Testing.Sample.Api.Model;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public class BankingScenario : BeginAScenario
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
        
        public BankAccountHasBeenCreated BankAccount_has_been_created2(Action<BankAccount>? configureBankAccount = null)
        {
           return Given(() =>
           {
               var bankAccount = new BankAccount
               {
                   CustomerName = "Ranulph Fiennes"
               };

               return bankAccount;
           })
               .When((context, bankAccount) =>
               {
                   var response = context.Api.Post("api/bankaccounts", bankAccount);

                   return response.Content<BankAccount>();
               })
               .Then<BankAccountHasBeenCreated>();

        }
    }
}