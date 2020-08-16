using Bard.Sample.Api.Model;
using Bard.Tests.Scenario;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Bard.Tests.GET
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

            Then.Response
                .ShouldBe
                .NotFound();
        }

        [Fact]
        public void Given_that_a_bank_account_has_been_created()
        {
            Given
                .BankAccount_has_been_created(account => account.CustomerName = "Fred")
                .GetResult(out BankingStoryData bankAccount);

            When
                .Get($"{ApiBankaccounts}/{bankAccount?.BankAccountId}");

            Then.Response
                .ShouldBe
                .Ok<BankAccount>();
        }

        [Fact]
        public void Given_that_a_mixture_of_deposits_and_withdrawals_have_been_made_then_the_balance_should_be_correct()
        {
            Given
                .BankAccount_has_been_created(account => account.CustomerName = "Fred")
                .DepositHasBeenMade(() => new Deposit {Amount = 100})
                .Withdrawal_has_been_made(50)
                .Deposit_has_been_made(25)
                .GetResult(out BankingStoryData bankAccount);

            When
                .Get($"{ApiBankaccounts}/{bankAccount?.BankAccountId}");

            Then.Response
                .ShouldBe
                .Ok<BankAccount>()
                .Balance
                .ShouldBe(75);
        }

        [Fact]
        public void Given_that_multiple_deposits_have_been_made_then_the_balance_should_be_correct()
        {
            Given
                .BankAccount_has_been_created(account => account.CustomerName = "Fred")
                .DepositHasBeenMade(() => new Deposit {Amount = 50})
                .Deposit_has_been_made(50)
                .Withdrawal_has_been_made(25)
                .GetResult(out BankingStoryData bankAccount);

            When
                .Get($"{ApiBankaccounts}/{bankAccount?.BankAccountId}");

            Then.Response
                .ShouldBe
                .Ok<BankAccount>()
                .Balance
                .ShouldBe(75);
        }

        [Fact]
        public void If_a_bank_account_has_been_updated_then_the_customer_name_should_be_correct()
        {
            Given
                .BankAccount_has_been_created(account => account.CustomerName = "Fred")
                .BankAccount_has_been_updated(() => new BankAccount {CustomerName = "Fergus"})
                .GetResult(out BankingStoryData bankAccount);

            When
                .Get($"{ApiBankaccounts}/{bankAccount?.BankAccountId}");

            Then.Response
                .ShouldBe
                .Ok<BankAccount>()
                .CustomerName
                .ShouldBe("Fergus");
        }

        [Fact]
        public void Then_the_customer_name_should_be_correct()
        {
            Given
                .BankAccount_has_been_created(account => account.CustomerName = "Fred")
                .GetResult(out BankingStoryData bankAccount);

            When
                .Get($"{ApiBankaccounts}/{bankAccount?.BankAccountId}");

            Then.Response
                .ShouldBe
                .Ok<BankAccount>()
                .CustomerName
                .ShouldBe("Fred");
        }
    }
}