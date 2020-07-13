using System;
using System.Linq.Expressions;
using System.Net;

namespace Fluent.Testing.Library.Then
{
    public class BadRequestProviderDecorator : IBadRequestProvider
    {
        private readonly IShouldBe _shouldBe;
        private readonly IBadRequestProvider _toDecorate;

        public BadRequestProviderDecorator(IShouldBe shouldBe, IBadRequestProvider decorate)
        {
            _shouldBe = shouldBe;
            _toDecorate = decorate;
        }

        public IBadRequestProvider ForProperty<TCommand>(Expression<Func<TCommand, object?>> expression)
        {
            EnsureIsBadRequest();

            return _toDecorate.ForProperty(expression);
        }

        public IBadRequestProvider ForProperty(string propertyName)
        {
            EnsureIsBadRequest();

            return _toDecorate.ForProperty(propertyName);
        }

        public IBadRequestProvider WithMessage(string message)
        {
            EnsureIsBadRequest();

            return _toDecorate.WithMessage(message);
        }

        public IBadRequestProvider WithErrorCode(string errorCode)
        {
            EnsureIsBadRequest();

            return _toDecorate.WithErrorCode(errorCode);
        }

        public IBadRequestProvider StartsWithMessage(string message)
        {
            EnsureIsBadRequest();

            return _toDecorate.StartsWithMessage(message);
        }

        public IBadRequestProvider EndsWithMessage(string message)
        {
            EnsureIsBadRequest();

            return _toDecorate.EndsWithMessage(message);
        }

        public string StringContent
        {
            get => _toDecorate.StringContent;
            set => _toDecorate.StringContent = value;
        }

        private void EnsureIsBadRequest()
        {
            _shouldBe.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        }
    }
}