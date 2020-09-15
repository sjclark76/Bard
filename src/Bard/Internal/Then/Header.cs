using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Bard.Infrastructure;
using Bard.Internal.When;

namespace Bard.Internal.Then
{
    internal class Headers : IHeaders, IHeadersShould, IInclude , IObserver<Func<IResponse>>
    {
        private readonly LogWriter _logWriter;
        private readonly HttpResponseMessage _responseMessage;
        private readonly ApiResult _apiResult;
        private Func<IResponse>? _apiRequest;
        private PerformanceMonitor _performanceMonitor;

        public Headers(ApiResult apiResult, LogWriter logWriter)
        {
            _logWriter = logWriter;
            _apiResult = apiResult;
            _responseMessage = apiResult.ResponseMessage;
            _performanceMonitor = new PerformanceMonitor(_logWriter);
        }

        internal int? MaxElapsedTime { get; set; }

        public IHeaders ShouldInclude(string headerName, string? headerValue = null)
        {
            var headerMessage =
                new StringBuilder($"THEN THE RESPONSE SHOULD INCLUDE THE HTTP HEADER '{headerName}'");

            if (headerValue != null)
                headerMessage.Append($":{headerValue}");

            _logWriter.LogHeaderMessage(headerMessage.ToString());

            _logWriter.WriteHttpResponseToConsole(_apiResult);

            var contentHeaders = _responseMessage.Content.Headers.Select(pair => pair);
            var headers = _responseMessage.Headers.Select(pair => pair);

            var allHeaders = contentHeaders.Concat(headers).ToList();

            var (headerKey, headerValues) = allHeaders.FirstOrDefault(pair =>
                string.Equals(pair.Key, headerName, StringComparison.CurrentCultureIgnoreCase));

            if (headerKey == null)
                throw new BardException($"Header '{headerName} not present.");

            if (headerValue != null)
            {
                var lowerCase = headerValue.ToLower();

                if (headerValues.Select(hv => hv.ToLower()).Contains(lowerCase) == false)
                    throw new BardException($"Header Value'{headerValue} not present.");
            }

            _performanceMonitor.AssertElapsedTime(_apiRequest, _apiResult, MaxElapsedTime);
            
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

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(System.Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Func<IResponse> apiRequest)
        {
            _apiRequest = apiRequest;
        }
    }
}