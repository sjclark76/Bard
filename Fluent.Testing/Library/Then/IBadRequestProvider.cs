using System;
using System.Linq.Expressions;

namespace Fluent.Testing.Library.Then
{
    public interface IBadRequestProvider
    {
        IBadRequestProvider ForProperty<TCommand>(Expression<Func<TCommand, object?>> expression);

        IBadRequestProvider ForProperty(string propertyName);

        IBadRequestProvider WithMessage(string message);

        IBadRequestProvider WithErrorCode(string errorCode);

        IBadRequestProvider StartsWithMessage(string message);

        IBadRequestProvider EndsWithMessage(string message);
        
        string StringContent { get; set; }
    }
}