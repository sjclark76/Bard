using System;
using System.Linq.Expressions;
using Fluent.Testing.Library.Infrastructure;
using Newtonsoft.Json;

namespace Fluent.Testing.Library.Then
{
    public abstract class BadRequestResponse<TErrorMessage> : IBadRequestResponse
    {
        private readonly string _httpResponseString = string.Empty;

        public IBadRequestResponse ForProperty<TCommand>(Expression<Func<TCommand, object?>> expression)
        {
            var propertyName = PropertyExpressionHelper.GetPropertyName(expression);

            ForProperty(propertyName);

            return this;
        }

        public abstract IBadRequestResponse ForProperty(string propertyName);

        public abstract IBadRequestResponse WithMessage(string message);

        public abstract IBadRequestResponse WithErrorCode(string errorCode);

        public abstract IBadRequestResponse StartsWithMessage(string message);

        public abstract IBadRequestResponse EndsWithMessage(string message);

        protected string ContentAsString()
        {
            return _httpResponseString;
        }

        protected TErrorMessage Content()
        {
            TErrorMessage content = default!;

            try
            {
                if (_httpResponseString != null)
                    content = JsonConvert.DeserializeObject<TErrorMessage>(_httpResponseString,
                        new JsonSerializerSettings
                        {
                            ContractResolver = new ResolvePrivateSetters()
                        });
            }
            catch (Exception)
            {
                // ok..
            }

            return content ?? throw new Exception($"Unable to serialize to {typeof(TErrorMessage).FullName}");
        }
    }
}