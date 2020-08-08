using System.Threading.Tasks;
using Grpc.Core;

namespace Bard.gRPCService.Services
{
    public class BankingService : BankAccountService.BankAccountServiceBase
    {
        // public override Task<BankAccountResponse> CreateBankAccount(CreateBankAccountRequest request, ServerCallContext context)
        // {
        //     return base.CreateBankAccount(request, context);
        // }

        public override Task<BankAccountResponse> GetBankAccount(BankAccountRequest request, ServerCallContext context)
        {
            return Task.FromResult(new BankAccountResponse
            {
                Id = 1,
                CustomerName = "mr blobby"

            });
        }

        // public override Task<BankAccountResponse> UpdateBankAccount(UpdateBankAccountRequest request, ServerCallContext context)
        // {
        //     return base.UpdateBankAccount(request, context);
        // }
    }
}