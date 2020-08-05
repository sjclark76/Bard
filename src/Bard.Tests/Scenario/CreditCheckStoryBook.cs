using Bard;
using Bard.gRPC;
using Bard.gRPCService;

namespace Fluent.Testing.Library.Tests.Scenario
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
                context.Grpc<CreditRatingCheck.CreditRatingCheckClient>(checkClient =>
                {
                    return checkClient.CheckCreditRequest(new CreditRequest
                    {
                        Credit = 100000000,
                        CustomerId = "this shouldn't be happening..."
                    });
                });
            }).End();
        }
    }
}