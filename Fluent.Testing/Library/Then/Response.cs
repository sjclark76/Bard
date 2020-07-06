using System.Net;
using System.Net.Http;
using Fluent.Testing.Library.Then.v1;

namespace Fluent.Testing.Library.Then
{
    public class Response<TShouldBe> : IResponse<TShouldBe> where TShouldBe : ShouldBeBase, new()
    {

        public Response(HttpResponseMessage httpResponse, string httpContent)
        {
            ShouldBe = new TShouldBe();
            ShouldBe.SetResponse(httpResponse, httpContent);
        }

        // public IBadRequestResponse EndsWithMessage(string message)
        // {
        //     var content = Content<ErrorResponse>();
        //     content.Errors.ShouldContain(error => error.Message != null && error.Message.EndsWith(message));
        //     return this;
        // }

        //public IShouldBe ShouldBe => this;

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

        public TShouldBe ShouldBe { get; }

        public void StatusCodeShouldBe(HttpStatusCode statusCode)
        {
            ShouldBe.StatusCodeShouldBe(statusCode);
        }

        public T Content<T>()
        {
            return ShouldBe.Content<T>();
        }
    }
}