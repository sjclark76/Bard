using Bard.gRPC;
using Bard.gRPCService;

namespace Bard.Tests.Scenario
{
    public class CreditCheckData
    {
    }

    public class CreditCheckStoryBook : StoryBook<CreditCheckData>
    {
        public EndChapter<CreditCheckData> Nothing_much_happens()
        {
            return When(context =>
            {
                var gRpcClient = context.Grpc<CreditRatingCheck.CreditRatingCheckClient>();

                gRpcClient.CheckCreditRequest(
                    new CreditRequest
                    {
                        Credit = 100000000,
                        CustomerId = "this shouldn't be happening..."
                    });
            }).End();
        }
    }
}