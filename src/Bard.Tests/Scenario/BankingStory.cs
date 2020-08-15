using System;
using Bard.Sample.Api;
using Bard.Sample.Api.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Bard.Tests.Scenario
{
    public class BankingStoryData
    {
        public decimal Balance { get; set; }
        public int BankAccountId { get; set; }
    }

    public class BankingStory : StoryBook<BankingStoryData>
    {
        public EndChapter<BankingStoryData> Nothing_much_happens()
        {
            return When(context => { })
                .End();
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

                    context.StoryData.BankAccountId = response.Content<BankAccount>().Id;
                })
                .ProceedToChapter<BankAccountHasBeenCreated>();
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

                    context.Writer.LogObject(bankAccount);
                    context.StoryData.BankAccountId = bankAccount.Id;
                })
                .ProceedToChapter<BankAccountHasBeenCreated>();
        }
    }
}