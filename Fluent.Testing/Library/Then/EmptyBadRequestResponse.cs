using System;
using System.Linq.Expressions;

namespace Fluent.Testing.Library.Then
{
    public class EmptyBadRequestResponse : IBadRequestResponse
    {
        public IBadRequestResponse ForProperty<TCommand>(Expression<Func<TCommand, object?>> expression)
        {
            throw new NotImplementedException();
        }

        public IBadRequestResponse ForProperty(string propertyName)
        {
            throw new NotImplementedException();
        }

        public IBadRequestResponse WithMessage(string message)
        {
            throw new NotImplementedException();
        }

        public IBadRequestResponse WithErrorCode(string errorCode)
        {
            throw new NotImplementedException();
        }

        public IBadRequestResponse StartsWithMessage(string message)
        {
            throw new NotImplementedException();
        }

        public IBadRequestResponse EndsWithMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}