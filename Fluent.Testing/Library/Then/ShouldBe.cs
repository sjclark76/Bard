using System;
using System.Net;
using System.Net.Http;
using Fluent.Testing.Library.Infrastructure;
using Fluent.Testing.Library.When;
using Newtonsoft.Json;
using Shouldly;

namespace Fluent.Testing.Library.Then
{
    public class ShouldBe : IShouldBe
    {
        private readonly string _httpResponseString;
        private HttpResponseMessage _httpResponse;

        public ShouldBe(ApiResult apiResult, IBadRequestProvider badRequestProvider)
        {
            badRequestProvider.StringContent = apiResult.ResponseString;
            BadRequest = new BadRequestProviderDecorator(this, badRequestProvider);
            _httpResponse = apiResult.ResponseMessage;
            _httpResponseString = apiResult.ResponseString;
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

            var content = Content<T>();

            content.ShouldNotBeNull(
                $"Couldn't deserialize the result to a {typeof(T)}. Result was: {_httpResponseString}.");

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

            content.ShouldNotBeNull(
                $"Couldn't deserialize the result to a {typeof(T)}. Result was: {_httpResponseString}.");

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
                throw new Exception($"{nameof(_httpResponse)} property has not been set.");

            var statusCode = _httpResponse.StatusCode;
            
            statusCode.ShouldBe(httpStatusCode,
                $"Status code mismatch, response was {_httpResponse.StatusCode}");
        }

        public T Content<T>()
        {
            T content = default!;

            try
            {
                if (_httpResponseString != null)
                    content = JsonConvert.DeserializeObject<T>(_httpResponseString, new JsonSerializerSettings
                    {
                        ContractResolver = new ResolvePrivateSetters()
                    });
            }
            catch (Exception)
            {
                // ok..
            }

            return content ?? throw new Exception($"Unable to serialize api response {_httpResponseString}");
        }
    }
}