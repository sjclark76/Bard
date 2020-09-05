using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Bard.Infrastructure;
using Bard.Internal.When;
using Newtonsoft.Json;

namespace Bard.Internal.Then
{
    internal class ShouldBe : IShouldBe, IObserver<GrpcResponse>
    {
        private readonly string _httpResponseString;
        private readonly LogWriter _logWriter;
        private object? _grpcResponse;
        private HttpResponseMessage _httpResponse;

        internal ShouldBe(ApiResult apiResult, IBadRequestProvider badRequestProvider, LogWriter logWriter)
        {
            _logWriter = logWriter;
            badRequestProvider.StringContent = apiResult.ResponseString;
            BadRequest = new BadRequestProviderDecorator(this, badRequestProvider);
            _httpResponse = apiResult.ResponseMessage;
            _httpResponseString = apiResult.ResponseString;
        }

        public bool Log { get; set; }

        public void OnCompleted()
        {
        }

        public void OnError(System.Exception error)
        {
        }

        public void OnNext(GrpcResponse value)
        {
            _grpcResponse = value.Response;
        }

        public IBadRequestProvider BadRequest { get; }

        public void Ok()
        {
            StatusCodeShouldBe(HttpStatusCode.OK);
        }

        public void NoContent()
        {
            StatusCodeShouldBe(HttpStatusCode.NoContent);
        }

        public T Ok<T>()
        {
            Ok();

            var content = DeserializeContent<T>();

            AssertContentIsNotNull(content);

            return content;
        }

        public void Created()
        {
            StatusCodeShouldBe(HttpStatusCode.Created);
        }

        public T Created<T>()
        {
            Created();

            var content = DeserializeContent<T>();

            AssertContentIsNotNull(content);

            return content;
        }

        public void Forbidden()
        {
            StatusCodeShouldBe(HttpStatusCode.Forbidden);
        }

        public void NotFound()
        {
            StatusCodeShouldBe(HttpStatusCode.NotFound);
        }

        public void StatusCodeShouldBe(HttpStatusCode httpStatusCode)
        {
            if (_httpResponse == null)
                throw new BardException($"{nameof(_httpResponse)} property has not been set.");

            var statusCode = _httpResponse.StatusCode;

            if (Log)
            {
                var headerMessage =
                    new StringBuilder($"THEN THE RESPONSE SHOULD BE HTTP {(int) httpStatusCode} {httpStatusCode}");
                
                if (statusCode != httpStatusCode)
                    headerMessage.Append($" BUT WAS HTTP {(int) statusCode} {statusCode}");

                _logWriter.LogHeaderMessage(headerMessage.ToString());

                LogResponse();       
            }

            if (statusCode != httpStatusCode)
                throw new BardException(
                    $"Invalid HTTP Status Code Received \n Expected: {(int) httpStatusCode} {httpStatusCode} \n Actual: {(int) statusCode} {statusCode} \n ");
        }

        public T Content<T>()
        {
            var content = DeserializeContent<T>();

            if (Log)
            {
                _logWriter.LogHeaderMessage($"THEN THE RESPONSE SHOULD BE {typeof(T)}");
                LogResponse();
            }
            
            return content;
        }
        
        private T DeserializeContent<T>()
        {
            T content = default!;

            try
            {
                if (_grpcResponse != null)
                    content = (T) _grpcResponse;
                else
                    content = JsonConvert.DeserializeObject<T>(_httpResponseString, new JsonSerializerSettings
                    {
                        ContractResolver = new ResolvePrivateSetters()
                    });
            }
            catch (System.Exception)
            {
                // ok..
            }

            return content ?? throw new BardException($"Unable to serialize api response {_httpResponseString}");
        }
        
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private void AssertContentIsNotNull<T>(T content)
        {
            if (content == null)
                throw new BardException(
                    $"Couldn't deserialize the result to a {typeof(T)}. Result was: {_httpResponseString}.");
        }
        
        private void LogResponse()
        {
            if (Log)
            {
                if (_grpcResponse != null)
                    _logWriter.LogObject(_grpcResponse);
                else
                    _logWriter.WriteHttpResponseToConsole(_httpResponse);    
            }
        }
    }
}