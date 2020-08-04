using System;

namespace Bard
{
    public interface IBeginGiven<out TStoryParams>
    {
        IBeginGivenWhen<TOutput> When<TOutput>(Func<ScenarioContext, TStoryParams, TOutput> execute)
            where TOutput : class, new();
    }
}