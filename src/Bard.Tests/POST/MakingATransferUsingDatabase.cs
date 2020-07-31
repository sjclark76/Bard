using Bard;
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
            Given
                .That
                .BankAccount_has_been_created_from_db(account =>
                {
                    account.CustomerName = "Rich Person";
                    account.Balance = 1000;
                })
                .GetResult(out BankAccount? richBankAccount);

            Given
                .That
                .BankAccount_has_been_created_from_db(account => account.CustomerName = "Poor Person Person")
                .GetResult(out BankAccount? poorBankAccount);

            When
                .Post("api/transfers", new Transfer
                {
                    FromBankAccountId = richBankAccount?.Id,
                    ToBankAccountId = poorBankAccount?.Id,
                    Amount = 100
                });

            Then.Response.ShouldBe.Created();
        }
    }
}