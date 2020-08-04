using System;

namespace Bard
{
    public interface IChapterGiven<TStoryData, out TStoryParams> where TStoryData : class, new() where TStoryParams : new()
    {
        IChapterGivenWhen<TStoryData> When(Func<ScenarioContext<TStoryData>, TStoryParams, TStoryData> execute);
    }
}