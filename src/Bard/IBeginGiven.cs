using System;

namespace Bard
{
    public interface IBeginGiven<out TRequest>
    {
        IBeginGivenWhen<TOutput> When<TOutput>(Func<IScenarioContext, TRequest, TOutput> execute)
            where TOutput : class, new();
    }
}