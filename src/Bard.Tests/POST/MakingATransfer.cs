using Bard;
using Fluent.Testing.Sample.Api.Model;
using Xunit;
using Xunit.Abstractions;

namespace Fluent.Testing.Library.Tests.POST
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
                .That
                .BankAccount_has_been_created()
                .Deposit_has_been_made(() => new Deposit {Amount = 100})
                .GetResult(out BankAccount? bankAccount);

            When
                .Post($"api/bankaccounts/{bankAccount?.Id}/withdrawals", new Withdrawal {Amount = 1000});

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
                .That
                .BankAccount_has_been_created(account => account.CustomerName = "Rich Person")
                .Deposit_has_been_made(() => new Deposit {Amount = 100})
                .GetResult(out BankAccount? richBankAccount);

            Given
                .That
                .BankAccount_has_been_created(account => account.CustomerName = "Poor Person Person")
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

        [Fact]
        public void Transfer_extracting_result_multiple_times()
        {
            Given
                .That
                .BankAccount_has_been_created(account => account.CustomerName = "Rich Person")
                .GetResult(out BankAccount? richBankAccount)
                .Deposit_has_been_made(() => new Deposit
                {
                    Id = richBankAccount?.Id,
                    Amount = 100
                })
                .BankAccount_has_been_created(account => account.CustomerName = "Poor Person Person")
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