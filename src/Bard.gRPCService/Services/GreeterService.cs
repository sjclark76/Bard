using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Bard.gRPCService.Services
{
    public class CreditRatingCheckService : CreditRatingCheck.CreditRatingCheckBase
    {
        private static readonly Dictionary<string, int> CustomerTrustedCredit = new Dictionary<string, int>
        {
            {"id0201", 10000},
            {"id0417", 5000},
            {"id0306", 15000}
        };

        private readonly ILogger<CreditRatingCheckService> _logger;

        public CreditRatingCheckService(ILogger<CreditRatingCheckService> logger)
        {
            _logger = logger;
        }

        public override Task<CreditReply> CheckCreditRequest(CreditRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CreditReply
            {
                IsAccepted = IsEligibleForCredit(request.CustomerId, request.Credit)
            });
        }

        private bool IsEligibleForCredit(string customerId, int credit)
        {
            var isEligible = false;

            if (CustomerTrustedCredit.TryGetValue(customerId, out var maxCredit))
            {
                isEligible = credit <= maxCredit;
            }

            return isEligible;
        }
    }
}