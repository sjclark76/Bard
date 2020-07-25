using System;

namespace Bard
{
    public interface IChapterGiven<out TInput, out TRequest> where TInput : new() where TRequest : new()
    {
        IChapterGivenWhen<TOutput> When<TOutput>(Func<IScenarioContext, TInput, TRequest, TOutput> execute)
            where TOutput : class, new();
    }
}