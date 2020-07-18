using System;

namespace Fluent.Testing.Library.Given
{
    public interface IBeginGiven<out TRequest>
    {
        IBeginGivenWhen<TOutput> When<TOutput>(Func<ScenarioContext, TRequest, TOutput> execute) where TOutput : class, new();
    }
}