using System;
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

    internal class BardResponsePublisher : DelegatingHandler
    {
        public Action<ApiResult>? PublishApiResult;

        public BardResponsePublisher(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            var responseString = await response.Content.ReadAsStringAsync();
            var apiResult = new ApiResult(response, responseString);

            PublishApiResult?.Invoke(apiResult);

            return response;
        }
    }
}