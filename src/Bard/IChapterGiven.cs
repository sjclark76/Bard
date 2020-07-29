using System;
using Bard.Internal;

namespace Bard
{
    public interface IChapterGiven<TInput, out TRequest> where TInput : class, new() where TRequest : new()
    {
        IChapterGivenWhen<TOutput> When<TOutput>(Func<ScenarioContext<TInput>, TRequest, TOutput> execute)
            where TOutput : class, new();
    }
}