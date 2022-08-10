using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bard.Infrastructure;

namespace Bard.Internal.When
{
    internal class ResponseLoggerMessageHandler : DelegatingHandler
    {
        private readonly LogWriter _logWriter;

        public ResponseLoggerMessageHandler(LogWriter logWriter, HttpMessageHandler innerHandler) : base(innerHandler)
        {
            _logWriter = logWriter;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (request.Content?.Headers.ContentType?.MediaType != "application/grpc")
            {
                _logWriter.WriteHttpResponseToConsole(response);
            }

            return response;
        }
    }
}