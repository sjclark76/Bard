using System;
using System.Net;
using System.Net.Http;
using Bard.Infrastructure;
using Bard.Internal.Exception;
using Bard.Internal.When;
using Newtonsoft.Json;

namespace Bard.Internal.Then
{
    internal class ShouldBe : IShouldBe, IObserver<GrpcResponse>
    {
        private readonly LogWriter _logWriter;
        private readonly string _httpResponseString;
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
        public bool Log { get; set; }

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

            var content = Content<T>();

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

            var content = Content<T>();

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
                _logWriter.LogHeaderMessage($"THEN THE RESPONSE SHOULD BE HTTP {(int) statusCode} {statusCode}");

                if (_grpcResponse != null)
                {
                    _logWriter.LogObject(_grpcResponse);
                }
                else
                {
                    _logWriter.WriteHttpResponseToConsole(_httpResponse);
                }
            }
            
            if (statusCode != httpStatusCode)
                throw new BardException(
                    $"Invalid HTTP Status Code Received \n Expected: {(int) httpStatusCode} {httpStatusCode} \n Actual: {(int) statusCode} {statusCode} \n ");
        }

        private void AssertContentIsNotNull<T>(T content)
        {
            if (content == null)
                throw new BardException(
                    $"Couldn't deserialize the result to a {typeof(T)}. Result was: {_httpResponseString}.");
        }

        public T Content<T>()
        {
            T content = default!;

            try
            {
                if (_grpcResponse != null)
                {
                    content = (T) _grpcResponse;
                }
                else
                {
                    if (_httpResponseString != null)
                        content = JsonConvert.DeserializeObject<T>(_httpResponseString, new JsonSerializerSettings
                        {
                            ContractResolver = new ResolvePrivateSetters()
                        });
                }
            }
            catch (System.Exception)
            {
                // ok..
            }

            return content ?? throw new BardException($"Unable to serialize api response {_httpResponseString}");
        }
    }
}