using System.Threading.Tasks;
using Bard.gRPCService;
using Grpc.Net.Client;
using Xunit;

namespace Fluent.Testing.Library.Tests.gRPC
{
    public class gRPCTests
    {
        [Fact]
        public async Task Foo()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client =  new CreditRatingCheck.CreditRatingCheckClient(channel);
            var creditRequest = new CreditRequest { CustomerId = "id0201", Credit = 7000};
            var reply = await client.CheckCreditRequestAsync(creditRequest);

        }
    }
}