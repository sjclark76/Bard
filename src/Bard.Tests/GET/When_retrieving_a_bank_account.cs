using Fluent.Testing.Sample.Api.Model;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Fluent.Testing.Library.Tests.GET
{
    // ReSharper disable once InconsistentNaming
    public class When_retrieving_a_bank_account : BankingTestBase
    {
        public When_retrieving_a_bank_account(ITestOutputHelper output) : base(output)
        {
        }

        private const string ApiBankaccounts = "api/bankaccounts";

        [Fact]
        public void For_a_customer_that_does_not_exist()
        {
            When
                .Get($"{ApiBankaccounts}/1234");

            Then
                .Response
                .ShouldBe
                .NotFound();
        }

        [Fact]
        public void Given_that_a_mixture_of_deposits_and_withdrawals_have_been_made_then_the_balance_should_be_correct()
        {
            var customerId = 0;

            Given
                .That
                .BankAccount_has_been_created(account => account.CustomerName = "Dougal")
                .Deposit_has_been_made(100)
                .Withdrawal_has_been_made(50)
                .Deposit_has_been_made(25)
                .UseResult(account => customerId = account.Id);

            When
                .Get($"{ApiBankaccounts}/{customerId}");

            Then
                .Response
                .ShouldBe
                .Ok<BankAccount>()
                .Balance
                .ShouldBe(75);
        }

        [Fact]
        public void Given_that_multiple_deposits_have_been_made_then_the_balance_should_be_correct()
        {
            var customerId = 0;

            Given
                .That
                .BankAccount_has_been_created(account => account.CustomerName = "Dougal")
                .Deposit_has_been_made(50)
                .Deposit_has_been_made(50)
                .Withdrawal_has_been_made(25)
                .UseResult(account => customerId = account.Id);

            When
                .Get($"{ApiBankaccounts}/{customerId}");

            Then
                .Response
                .ShouldBe
                .Ok<BankAccount>()
                .Balance
                .ShouldBe(75);
        }

        [Fact]
        public void Then_the_customer_name_should_be_correct()
        {
            var customerId = 0;

            Given
                .That
                .BankAccount_has_been_created(account => account.CustomerName = "Dougal")
                .UseResult(account => customerId = account.Id);

            When
                .Get($"{ApiBankaccounts}/{customerId}");

            Then
                .Response
                .ShouldBe
                .Ok<BankAccount>()
                .CustomerName
                .ShouldBe("Dougal");
        }
    }
}