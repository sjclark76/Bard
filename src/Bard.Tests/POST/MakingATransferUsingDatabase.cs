using Fluent.Testing.Sample.Api.Model;
using Xunit;
using Xunit.Abstractions;

namespace Fluent.Testing.Library.Tests.POST
{
    public class MakingATransferUsingDatabase : BankingTestBase
    {
        public MakingATransferUsingDatabase(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void If_the_the_transfer_is_successful_then_an_ok_response_should_be_returned()
        {
            var richBankAccount = new BankAccount();
            var poorBankAccount = new BankAccount();

            Given
                .That
                .BankAccount_has_been_created_from_db(account =>
                {
                    account.CustomerName = "Rich Person";
                    account.Balance = 1000;
                })
                .UseResult(account => richBankAccount = account);

            Given
                .That
                .BankAccount_has_been_created_from_db(account => account.CustomerName = "Poor Person Person")
                .UseResult(account => poorBankAccount = account);

            When
                .Post("api/transfers", new Transfer
                {
                    FromBankAccountId = richBankAccount.Id,
                    ToBankAccountId = poorBankAccount.Id,
                    Amount = 100
                });

            Then.Response.ShouldBe.Created();
        }
    }
}