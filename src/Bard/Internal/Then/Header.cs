using System.Linq;
using System.Net.Http;
using System.Text;
using Bard.Infrastructure;
using Bard.Internal.Exception;
using Bard.Internal.When;

namespace Bard.Internal.Then
{
    internal class Headers : IHeaders, IHeadersShould, IInclude
    {
        private readonly LogWriter _logWriter;
        private readonly HttpResponseMessage _responseMessage;

        public Headers(ApiResult apiResult, LogWriter logWriter)
        {
            _logWriter = logWriter;
            _responseMessage = apiResult.ResponseMessage;
        }

        public IHeaders ShouldInclude(string headerName, string? headerValue = null)
        {
            var headerMessage =
                new StringBuilder($"THEN THE RESPONSE SHOULD INCLUDE THE HTTP HEADER '{headerName}'");

            if (headerValue != null)
                headerMessage.Append($":{headerValue}");

            _logWriter.LogHeaderMessage(headerMessage.ToString());

            _logWriter.WriteHttpResponseToConsole(_responseMessage);

            var contentHeaders = _responseMessage.Content.Headers.Select(pair => pair);
            var headers = _responseMessage.Headers.Select(pair => pair);

            var allHeaders = contentHeaders.Concat(headers).ToList();

            var (headerKey, headerValues) = allHeaders.FirstOrDefault(pair => pair.Key == headerName);

            if (headerKey == null)
                throw new BardException($"Header '{headerName} not present.");

            if (headerValue != null && headerValues.Contains(headerValue) == false)
                throw new BardException($"Header Value'{headerValue} not present.");
            
            return this;
        }

        public IHeadersShould Should => this;

        public IInclude Include => this;

        public IHeaders ContentType(string? headerValue = null)
        {
            ShouldInclude("Content-Type", headerValue);

            return this;
        }

        public IHeaders ContentLength(string? headerValue = null)
        {
            ShouldInclude("Content-Length", headerValue);

            return this;
        }
    }
}