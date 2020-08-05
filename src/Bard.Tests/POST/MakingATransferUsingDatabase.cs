using Bard;
using Fluent.Testing.Library.Tests.Scenario;
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
                .GetResult(out BankingStoryData? bankAccount1);

            var richBankAccountId = bankAccount1?.BankAccountId;
            
            Given
                .That
                .BankAccount_has_been_created_from_db(account => account.CustomerName = "Poor Person Person")
                .GetResult(out BankingStoryData? bankAccount2);

            var poorBankAccountId = bankAccount2?.BankAccountId;

            When
                .Post("api/transfers", new Transfer
                {
                    FromBankAccountId = richBankAccountId,
                    ToBankAccountId = poorBankAccountId,
                    Amount = 100
                });

            Then.Response.ShouldBe.Created();
        }
    }
}