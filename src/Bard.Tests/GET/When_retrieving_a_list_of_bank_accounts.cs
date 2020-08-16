using Bard.Sample.Api.Model;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Bard.Tests.GET
{
    // ReSharper disable once InconsistentNaming
    public class When_retrieving_a_list_of_bank_accounts : BankingTestBase
    {
        public When_retrieving_a_list_of_bank_accounts(ITestOutputHelper output) : base(output)
        {
        }

        // [Fact]
        // public void If_multiple_accounts_are_created_the_count_should_be_correct()
        // {
        //     Given
        //         .BankAccount_has_been_created_from_db(account => account.CustomerName = "Fred")
        //         .BankAccount_has_been_created_from_db();
        //
        //     Given
        //         .BankAccount_has_been_created_from_db(account => account.CustomerName = "Billy")
        //         .BankAccount_has_been_created_from_db();
        //
        //     When
        //         .Get("api/bankaccounts");
        //
        //     Then.Response
        //         .ShouldBe
        //         .Ok<BankAccount[]>()
        //         .Length.ShouldBe(4);
        // }
    }
}