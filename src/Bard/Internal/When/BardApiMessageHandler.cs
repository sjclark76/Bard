using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bard.Infrastructure;

namespace Bard.Internal.When
{
    public class BardApiMessageHandler : DelegatingHandler
    {
        private readonly LogWriter _logWriter;

        public BardApiMessageHandler(LogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            _logWriter.WriteHttpRequestToConsole(request);
            var response = await base.SendAsync(request, cancellationToken);

            _logWriter.WriteHttpResponseToConsole(response);
            response.Version = request.Version;

            return response;
        }
    }
}