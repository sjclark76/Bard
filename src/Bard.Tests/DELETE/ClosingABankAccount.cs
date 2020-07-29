using Fluent.Testing.Sample.Api.Model;
using Xunit;
using Xunit.Abstractions;

namespace Fluent.Testing.Library.Tests.DELETE
{
    public class ClosingABankAccount : BankingTestBase
    {
        public ClosingABankAccount(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void If_a_request_is_made_to_close_a_bank_account_that_does_not_exist_then_a_404_should_be_returned()
        {
            When
                .Delete("api/bankaccounts/1234");

            Then
                .Response
                .ShouldBe
                .NotFound();
        }

        [Fact]
        public void If_the_request_is_successful_then_an_ok_no_content_response_should_be_returned()
        {
            var bankAccount = new BankAccount();

            Given
                .That
                .BankAccount_has_been_created()
                .UseResult(account => bankAccount = account);

            When
                .Delete($"api/bankaccounts/{bankAccount.Id}");

            Then.Response.ShouldBe.NoContent();
        }
    }
}