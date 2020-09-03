using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bard.Infrastructure;

namespace Bard.Internal.When
{
    internal class RequestLoggerMessageHandler : DelegatingHandler
    {
        private readonly LogWriter _logWriter;

        public RequestLoggerMessageHandler(LogWriter logWriter, HttpMessageHandler innerHandler) : base(innerHandler)
        {
            _logWriter = logWriter;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content?.Headers.ContentType.MediaType != "application/grpc")
                _logWriter.WriteHttpRequestToConsole(request);
         
            return base.SendAsync(request, cancellationToken);
        }
    }
}