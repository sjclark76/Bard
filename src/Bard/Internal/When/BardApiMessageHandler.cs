using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bard.Infrastructure;

namespace Bard.Internal.When
{
    internal class BardApiMessageHandler : DelegatingHandler
    {
        private readonly LogWriter _logWriter;

        public Action<ApiResult>? PublishApiResult;

        public BardApiMessageHandler(LogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            _logWriter.WriteHttpRequestToConsole(request);
            var response = await base.SendAsync(request, cancellationToken);

            var responseString = AsyncHelper.RunSync(() => response.Content.ReadAsStringAsync());
            var apiResult = new ApiResult(response, responseString);

            PublishApiResult?.Invoke(apiResult);

            _logWriter.WriteHttpResponseToConsole(response);
            response.Version = request.Version;

            return response;
        }
    }
}