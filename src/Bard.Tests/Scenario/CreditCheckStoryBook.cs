using Bard.gRPC;
using Bard.gRPCService;

namespace Bard.Tests.Scenario
{
    public class CreditCheckData
    {
    }

    public class CreditCheckStoryBook : StoryBook<CreditCheckData>
    {
        public NothingMuchHappens Call_credit_check_service()
        {
            return When(context =>
            {
                var gRpcClient = context.Grpc<CreditRatingCheck.CreditRatingCheckClient>();

               var response = gRpcClient.CheckCreditRequest(
                    new CreditRequest
                    {
                        Credit = 100000000,
                        CustomerId = "this shouldn't be happening..."
                    });
                
                context.Writer.LogObject(response);
            }).ProceedToChapter<NothingMuchHappens>();
        }
    }

    public class NothingMuchHappens : Chapter<CreditCheckData>
    {
        public EndChapter<CreditCheckData> Call_banking_service()
        {
            return When(context =>
            {
                var gRpcClient = context.Grpc<BankAccountService.BankAccountServiceClient>();

                gRpcClient.GetBankAccount(new BankAccountRequest());
            }).End();
        }
    }
}