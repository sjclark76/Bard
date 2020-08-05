using System;

namespace Bard
{
    public interface IChapterGiven<TStoryData, out TStoryParams> where TStoryData : class, new() where TStoryParams : new()
    {
        IChapterGivenWhen<TStoryData> When(Action<ScenarioContext<TStoryData>, TStoryParams> execute);
    }
}