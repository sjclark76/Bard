using System;

namespace Bard
{
    public interface IChapterGiven<TChapterInput, out TRequest>
        where TChapterInput : class, new() where TRequest : new()
    {
        IChapterGivenWhen<TOutput> When<TOutput>(Func<ScenarioContext<TChapterInput>, TRequest, TOutput> execute)
            where TOutput : class, new();
    }
}