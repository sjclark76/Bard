using System;
using System.Linq.Expressions;
using System.Net;

namespace Fluent.Testing.Library.Then
{
    public class BadRequestResponseDecorator : IBadRequestResponse
    {
        private readonly IShouldBeBase _shouldBe;
        private readonly IBadRequestResponse _toDecorate;

        public BadRequestResponseDecorator(IShouldBeBase shouldBe, IBadRequestResponse decorate)
        {
            _shouldBe = shouldBe;
            _toDecorate = decorate;
        }

        public IBadRequestResponse ForProperty<TCommand>(Expression<Func<TCommand, object?>> expression)
        {
            EnsureIsBadRequest();

            return _toDecorate.ForProperty(expression);
        }

        public IBadRequestResponse ForProperty(string propertyName)
        {
            EnsureIsBadRequest();

            return _toDecorate.ForProperty(propertyName);
        }

        public IBadRequestResponse WithMessage(string message)
        {
            EnsureIsBadRequest();

            return _toDecorate.WithMessage(message);
        }

        public IBadRequestResponse WithErrorCode(string errorCode)
        {
            EnsureIsBadRequest();

            return _toDecorate.WithErrorCode(errorCode);
        }

        public IBadRequestResponse StartsWithMessage(string message)
        {
            EnsureIsBadRequest();

            return _toDecorate.StartsWithMessage(message);
        }

        public IBadRequestResponse EndsWithMessage(string message)
        {
            EnsureIsBadRequest();

            return _toDecorate.EndsWithMessage(message);
        }

        private void EnsureIsBadRequest()
        {
            _shouldBe.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        }
    }
}