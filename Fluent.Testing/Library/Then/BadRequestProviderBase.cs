using System;
using System.Linq.Expressions;
using Fluent.Testing.Library.Infrastructure;

namespace Fluent.Testing.Library.Then
{
    public abstract class BadRequestProviderBase : IBadRequestProvider
    {
        public IBadRequestProvider ForProperty<TCommand>(Expression<Func<TCommand, object?>> expression)
        {
            var propertyName = PropertyExpressionHelper.GetPropertyName(expression);

            ForProperty(propertyName);

            return this;
        }

        public abstract IBadRequestProvider ForProperty(string propertyName);

        public abstract IBadRequestProvider WithMessage(string message);

        public abstract IBadRequestProvider WithErrorCode(string errorCode);

        public abstract IBadRequestProvider StartsWithMessage(string message);

        public abstract IBadRequestProvider EndsWithMessage(string message);
        
        public string StringContent { get; set; } = string.Empty;
    }
}