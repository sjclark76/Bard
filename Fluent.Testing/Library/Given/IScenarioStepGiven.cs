using System;

namespace Fluent.Testing.Library.Given
{
    public interface IScenarioStepGiven<out TInput, out TRequest> where TInput : new() where TRequest : new()
    {
        IScenarioStepGivenWhen<TOutput> When<TOutput>(Func<ScenarioContext, TInput, TRequest, TOutput> execute) where TOutput : class, new();
    }
}