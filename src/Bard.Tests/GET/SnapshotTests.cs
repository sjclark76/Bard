using Bard.Sample.Api.Model;
using Bard.Tests.Scenario;
using Xunit;
using Xunit.Abstractions;

namespace Bard.Tests.GET
{
    public class SnapshotTests : BankingTestBase
    {
        public SnapshotTests(ITestOutputHelper output) : base(output)
        {
        }

        private const string ApiBankaccounts = "api/bankaccounts";

        [Theory]
        [InlineData("Dougal")]
        [InlineData("Dexter")]
        [InlineData("Fergus")]
        public void Then_the_snapshot_customer_should_be_correct(string customerName)
        {
            Given
                .BankAccount_has_been_created(account => account.CustomerName = customerName)
                .GetResult(out BankingStoryData bankAccount);

            When
                .Get($"{ApiBankaccounts}/{bankAccount.BankAccountId}");

            Then.Response
                .Snapshot<BankAccount>();
        }

        [Fact]
        public void Then_the_snapshot_for_my_scenario_should_be_correct()
        {
            MyScenario
                .Given
                .BankAccount_has_been_created(account => account.CustomerName = "Fred2")
                .GetResult(out BankingStoryData bankAccount);

            MyScenario.When
                .Get($"{ApiBankaccounts}/{bankAccount.BankAccountId}");

            MyScenario.Then.Response
                .Snapshot<BankAccount>(options => options.IgnoreField<int>("Id"));
        }

        [Fact]
        public void Then_the_snapshot_should_be_correct()
        {
            Given
                .BankAccount_has_been_created(account => account.CustomerName = "Fred")
                .GetResult(out BankingStoryData bankAccount);

            When
                .Get($"{ApiBankaccounts}/{bankAccount.BankAccountId}");

            MyScenario.Then.Response
                .Snapshot<BankAccount>(options => options.IgnoreField<int>("Id"));
        }
    }
}