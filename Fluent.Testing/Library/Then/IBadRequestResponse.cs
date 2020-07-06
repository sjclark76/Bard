using System;
using System.Linq.Expressions;

namespace Fluent.Testing.Library.Then
{
    public interface IBadRequestResponse
    {
        IBadRequestResponse ForProperty<TCommand>(Expression<Func<TCommand, object?>> expression);

        IBadRequestResponse ForProperty(string propertyName);

        IBadRequestResponse WithMessage(string message);

        IBadRequestResponse WithErrorCode(string errorCode);

        IBadRequestResponse StartsWithMessage(string message);

        IBadRequestResponse EndsWithMessage(string message);
    }
}