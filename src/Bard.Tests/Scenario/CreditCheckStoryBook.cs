using Bard;
using Bard.gRPC;
using Bard.gRPCService;

namespace Fluent.Testing.Library.Tests.Scenario
{
    public class CreditCheckStoryBook : StoryBook<GrpcScenarioContext<CreditRatingCheck.CreditRatingCheckClient>>
    {
        public EndChapter<object> Nothing_much_happens()
        {
            return When(context =>
            {
                return new object();
            }).End();
        }
    }
}