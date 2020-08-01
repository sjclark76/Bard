using System;
using Bard;
using Fluent.Testing.Sample.Api;
using Fluent.Testing.Sample.Api.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public class BankingStory : StoryBook
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
        
        public BankAccountHasBeenCreated BankAccount_has_been_created_from_db(Action<BankAccount>? configureBankAccount = null)
        {
            return When(context =>
                {
                    var bankAccount = new BankAccount
                    {
                        CustomerName = "Ranulph Fiennes"
                    };

                    configureBankAccount?.Invoke(bankAccount);

                    var dbContext = context.Services.GetService<BankDbContext>();

                    dbContext.BankAccounts.Add(bankAccount);
                    dbContext.SaveChanges();

                    return bankAccount;
                })
                .Then<BankAccountHasBeenCreated>();
        }
    }
}