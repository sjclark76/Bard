using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Bard.Internal.When
{
    internal class BardResponsePublisher : DelegatingHandler
    {
        public Action<ApiResult>? PublishApiResult;
        private readonly Stopwatch _stopwatch;

        public BardResponsePublisher(HttpMessageHandler innerHandler) : base(innerHandler)
        {
            _stopwatch = new Stopwatch();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            _stopwatch.Reset();
            _stopwatch.Start();
            
            var response = await base.SendAsync(request, cancellationToken);
            
            _stopwatch.Stop();
            var responseString = await response.Content.ReadAsStringAsync();
            var apiResult = new ApiResult(response, responseString, _stopwatch.Elapsed);

            PublishApiResult?.Invoke(apiResult);

            return response;
        }
    }
}