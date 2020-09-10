using System;
using System.Diagnostics;
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
        public Stopwatch Stopwatch;

        public BardResponsePublisher(HttpMessageHandler innerHandler) : base(innerHandler)
        {
            Stopwatch = new Stopwatch();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Stopwatch.Reset();
            Stopwatch.Start();
            
            var response = await base.SendAsync(request, cancellationToken);
            
            Stopwatch.Stop();
            var responseString = await response.Content.ReadAsStringAsync();
            var apiResult = new ApiResult(response, responseString, Stopwatch.Elapsed);

            PublishApiResult?.Invoke(apiResult);

            return response;
        }
    }
}