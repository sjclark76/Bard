using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Bard.Internal.When
{
    internal class GrpcMessageHandler : DelegatingHandler
    {
        public GrpcMessageHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            
            response.Version = request.Version;

            return response;
        }
    }
}