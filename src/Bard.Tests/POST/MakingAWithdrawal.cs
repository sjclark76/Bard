using Bard.Sample.Api.Model;
using Bard.Tests.Scenario;
using Xunit;
using Xunit.Abstractions;

namespace Bard.Tests.POST
{
    public class MakingAWithdrawal : BankingTestBase
    {
        public MakingAWithdrawal(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void If_a_withdrawal_is_made_for_bank_account_that_does_not_exist_then_a_404_should_be_returned()
        {
            When
                .Post("api/bankaccounts/1234/withdrawals", new Deposit {Amount = 100});

            Then
                .Response
                .ShouldBe
                .NotFound();
        }

        [Fact]
        public void
            If_a_withdrawal_is_requested_but_there_are_insufficient_funds_then_a_bad_request_should_be_returned()
        {
            Given
                .BankAccount_has_been_created()
                .Deposit_has_been_made(_ => new Deposit {Amount = 100})
                .GetResult(out BankingStoryData bankAccount);

            When
                .Post($"api/bankaccounts/{bankAccount.BankAccountId}/withdrawals", new Withdrawal {Amount = 1000});

            Then
                .Response
                .ShouldBe
                .BadRequest
                .WithMessage("Insufficient Funds to make withdrawal.");
        }

        [Fact]
        public void If_the_withdrawal_is_successful_then_an_ok_response_should_be_returned()
        {
            Given
                .BankAccount_has_been_created()
                .Deposit_has_been_made(_ => new Deposit {Amount = 100})
                .GetResult(out BankingStoryData bankAccount);

            When
                .Post($"api/bankaccounts/{bankAccount.BankAccountId}/withdrawals", new Withdrawal {Amount = 100});

            Then.Response.ShouldBe.Ok();
        }
    }
}