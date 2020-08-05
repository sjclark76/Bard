using Bard;
using Fluent.Testing.Library.Tests.Scenario;
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
            Given
                .That
                .BankAccount_has_been_created()
                .GetResult(out BankingStoryData? bankAccount);

            When
                .Delete($"api/bankaccounts/{bankAccount?.BankAccountId}");

            Then.Response.ShouldBe.NoContent();
        }
    }
}