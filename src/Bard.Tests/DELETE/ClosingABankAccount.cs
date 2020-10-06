using Bard.Tests.Scenario;
using Xunit;
using Xunit.Abstractions;

namespace Bard.Tests.DELETE
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
                .BankAccount_has_been_created()
                .GetResult(out BankingStoryData bankAccount);

            When
                .Delete($"api/bankaccounts/{bankAccount.BankAccountId}");

            Then.Response.ShouldBe.NoContent();
        }
        
        [Fact]
        public void If_the_request_is_successful_then_an_ok_no_content_response_should_be_returned1()
        {
            BankingStoryData? storyData = null;
            
            Given
                .BankAccount_has_been_created()
                .GetResult(result => storyData = result);

            When
                .Delete($"api/bankaccounts/{storyData?.BankAccountId}");

            Then.Response.ShouldBe.NoContent();
        }
        
        [Fact]
        public void If_the_request_is_successful_then_an_ok_no_content_response_should_be_returned2()
        {
            var storyData = 
            
            Given
                .BankAccount_has_been_created()
                .GetResult();

            When
                .Delete($"api/bankaccounts/{storyData.BankAccountId}");

            Then.Response.ShouldBe.NoContent();
        }
    }
}