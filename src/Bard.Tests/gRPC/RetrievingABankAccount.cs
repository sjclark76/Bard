using Bard.gRPCService;
using Xunit;
using Xunit.Abstractions;

namespace Bard.Tests.gRPC
{
    public class RetrievingABankAccount : BankingTestBase
    {
        public RetrievingABankAccount(ITestOutputHelper output) : base(output)
        {
        }

        [Fact(Skip = "Just for now.")]
        public void Foo()
        {
            Scenario
                .Grpc<BankAccountService.BankAccountServiceClient>()
                .When(client => client.GetBankAccount(new BankAccountRequest()));

            Scenario.Then.Response.ShouldBe.Ok<BankAccountResponse>();
        }
    }
}