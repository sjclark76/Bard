using System;
using Bard;
using Fluent.Testing.Sample.Api;
using Fluent.Testing.Sample.Api.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public class BankingStoryData
    {
        public decimal Balance { get; set; }
        public int BankAccountId { get; set; }
    }

    public class BankingStory : StoryBook<BankingStoryData>
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

                    context.StoryData.BankAccountId = response.Content<BankAccount>().Id;
                })
                .Then<BankAccountHasBeenCreated>();
        }

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

                    dbContext.BankAccounts.Add(bankAccount);
                    dbContext.SaveChanges();

                    context.StoryData.BankAccountId = bankAccount.Id;
                })
                .Then<BankAccountHasBeenCreated>();
        }
    }
}