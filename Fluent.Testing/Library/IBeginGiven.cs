using System;

namespace Fluent.Testing.Library
{
    public interface IBeginGiven<out TRequest>
    {
        IBeginGivenWhen<TOutput> When<TOutput>(Func<ScenarioContext, TRequest, TOutput> execute) where TOutput : class, new();
    }
}