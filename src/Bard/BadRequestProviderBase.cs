using System;
using System.Linq.Expressions;
using Bard.Infrastructure;

namespace Bard
{
    /// <inheritdoc />
    public abstract class BadRequestProviderBase : IBadRequestProvider
    {
        /// <inheritdoc />
        public IBadRequestProvider ForProperty<TCommand>(Expression<Func<TCommand, object?>> expression)
        {
            var propertyName = PropertyExpressionHelper.GetPropertyName(expression);

            ForProperty(propertyName);

            return this;
        }

        /// <inheritdoc />
        public abstract IBadRequestProvider ForProperty(string propertyName);

        /// <inheritdoc />
        public abstract IBadRequestProvider WithMessage(string message);

        /// <inheritdoc />
        public abstract IBadRequestProvider WithErrorCode(string errorCode);

        /// <inheritdoc />
        public abstract IBadRequestProvider StartsWithMessage(string message);

        /// <inheritdoc />
        public abstract IBadRequestProvider EndsWithMessage(string message);

        /// <inheritdoc />
        public string StringContent { get; set; } = string.Empty;
    }
}