using Bard;
using Bard.gRPCService;
using Bard.Internal;
using Grpc.Net.Client;

namespace Fluent.Testing.Library.Tests.Scenario
{
 
    public class CreditCheckStoryBook : StoryBook
    {
        public EndChapter<object> Nothing_much_happens()
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
                    
                context.Writer.WriteStringToConsole("nothing much has happened..");
                //context.
                return new object();
            }).End();

        }
    }
}