using System;
using System.Net;
using System.Net.Http;
using Fluent.Testing.Library.Infrastructure;
using Newtonsoft.Json;
using Shouldly;

namespace Fluent.Testing.Library.Then
{
    public class Response : ITheResponse, IShouldBe, IBadRequestResponse
    {
        private readonly string? _httpContent;
        private readonly HttpResponseMessage? _httpResponse;

        public Response()
        {
        }

        public Response(HttpResponseMessage httpResponse, string httpContent)
        {
            _httpResponse = httpResponse;
            _httpContent = httpContent;
        }

        public ITheResponse TheResponse => this;

        // public IBadRequestResponse ForProperty<TCommand>(Expression<Func<TCommand, object?>> expression)
        // {
        //     var propertyName = PropertyExpressionHelper.GetPropertyName(expression);
        //
        //     ForProperty(propertyName);
        //
        //     return this;
        // }

        // public IBadRequestResponse ForProperty(string propertyName)
        // {
        //     var content = Content<ErrorResponse>();
        //
        //     content.Errors.ShouldContain(error => error.Property != null && error.Property.Contains(propertyName));
        //
        //     return this;
        // }
        //
        // public IBadRequestResponse StartsWithMessage(string message)
        // {
        //     var content = Content<ErrorResponse>();
        //     content.Errors.ShouldContain(error => error.Message != null && error.Message.StartsWith(message));
        //     return this;
        // }
        //
        // public IBadRequestResponse WithErrorCode(string errorCode)
        // {
        //     Content<ErrorResponse>()
        //         .Errors.ShouldContain(error => error.ErrorCode == errorCode,
        //             $"An error with the ErrorCode:'{errorCode}' could not be found.");
        //
        //     return this;
        // }
        //
        // public IBadRequestResponse WithMessage(string message)
        // {
        //     Content<ErrorResponse>()
        //         .Errors.ShouldContain(error => error.Message == message,
        //             $"An error with the message:'{message}' could not be found.");
        //
        //     return this;
        // }

        public IBadRequestResponse BadRequest
        {
            get
            {
                _httpResponse?.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

                return this;
            }
        }

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

            content.ShouldNotBeNull($"Couldn't deserialize the result to a {typeof(T)}. Result was: {_httpContent}.");

            return content;
        }

        public T Created<T>()
        {
            Created();

            var content = Content<T>();

            content.ShouldNotBeNull($"Couldn't deserialize the result to a {typeof(T)}. Result was: {_httpContent}.");

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

        // public IBadRequestResponse EndsWithMessage(string message)
        // {
        //     var content = Content<ErrorResponse>();
        //     content.Errors.ShouldContain(error => error.Message != null && error.Message.EndsWith(message));
        //     return this;
        // }

        public IShouldBe ShouldBe => this;

        public T Content<T>()
        {
            T content = default!;

            try
            {
                if (_httpContent != null)
                    content = JsonConvert.DeserializeObject<T>(_httpContent, new JsonSerializerSettings
                    {
                        ContractResolver = new ResolvePrivateSetters()
                    });
            }
            catch (Exception)
            {
                // ok..
            }

            return content ?? throw new Exception($"Unable to serialize to {typeof(T).FullName}");
        }

        public void Created()
        {
            StatusCodeShouldBe(HttpStatusCode.Created);
        }

        private void StatusCodeShouldBe(HttpStatusCode statusCode)
        {
            _httpResponse?.StatusCode.ShouldBe(statusCode,
                $"Status code mismatch, response was {_httpResponse.StatusCode}");
        }
    }
}