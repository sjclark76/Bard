using Bard.Sample.Api.Model;
using Bard.Tests.Scenario;
using Xunit;
using Xunit.Abstractions;

namespace Bard.Tests.POST
{
    public class MakingATransfer : BankingTestBase
    {
        public MakingATransfer(ITestOutputHelper output) : base(output)
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
                .Deposit_has_been_made((storyData) => new Deposit {Amount = 100})
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
        public void If_the_the_transfer_is_successful_then_an_ok_response_should_be_returned()
        {
            Given
                .BankAccount_has_been_created(account => account.CustomerName = "Rich Person")
                .Deposit_has_been_made((storyData) => new Deposit {Amount = 100})
                .GetResult(out BankingStoryData richBankAccount);

            Given
                .BankAccount_has_been_created(account => account.CustomerName = "Poor Person Person")
                .GetResult(out BankingStoryData poorBankAccount);

            When
                .Post("api/transfers", new Transfer
                {
                    FromBankAccountId = richBankAccount.BankAccountId,
                    ToBankAccountId = poorBankAccount.BankAccountId,
                    Amount = 100
                });

            Then.Response.ShouldBe.Created();
        }

        [Fact]
        public void Transfer_extracting_result_multiple_times()
        {
            Given
                .BankAccount_has_been_created(account => account.CustomerName = "Rich Person")
                .GetResult(out BankingStoryData richBankAccount)
                .Deposit_has_been_made((storyData) => new Deposit
                {
                    Id = richBankAccount.BankAccountId,
                    Amount = 100
                })
                .BankAccount_has_been_created(account => account.CustomerName = "Poor Person Person")
                .GetResult(out BankingStoryData poorBankAccount);

            When
                .Post("api/transfers", new Transfer
                {
                    FromBankAccountId = richBankAccount.BankAccountId,
                    ToBankAccountId = poorBankAccount.BankAccountId,
                    Amount = 100
                });

            Then.Response.ShouldBe.Created();
        }
    }
}