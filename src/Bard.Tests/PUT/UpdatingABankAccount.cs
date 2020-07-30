using Fluent.Testing.Sample.Api.Model;
using Xunit;
using Xunit.Abstractions;

namespace Fluent.Testing.Library.Tests.PUT
{
    public class UpdatingABankAccount : BankingTestBase
    {
        public UpdatingABankAccount(ITestOutputHelper output) : base(output)
        {
        }
        
        [Fact]
        public void When_updating_a_bank_account_the_response_should_be_no_content()
        {
            BankAccount bankAccount = new BankAccount();
            
            Given.That
                .BankAccount_has_been_created()
                .UseResult(account => bankAccount = account);
            
            When
                .Put($"api/bankaccounts/{bankAccount.Id}", new BankAccount
                {
                    CustomerName = "New Name"
                });

            Then.Response
                .ShouldBe
                .NoContent();
        }
    }
}