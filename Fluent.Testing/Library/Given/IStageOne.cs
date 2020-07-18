using System;

namespace Fluent.Testing.Library.Given
{
    public interface IStageOne<out TInput, out TRequest> where TInput : new() where TRequest : new()
    {
        IStageTwo<TOutput> When<TOutput>(Func<ScenarioContext, TInput, TRequest, TOutput> execute) where TOutput : class, new();
    }
}