using System;

namespace Bard
{
    /// <summary>
    ///     Interface to help the fluent interface story builder
    /// </summary>
    /// <typeparam name="TStoryData">The StoryData</typeparam>
    /// <typeparam name="TStoryParams">The Story Parameters</typeparam>
    public interface IChapterGiven<TStoryData, out TStoryParams>
        where TStoryData : class, new() where TStoryParams : new()
    {
        /// <summary>
        /// </summary>
        /// <param name="executeStory"></param>
        /// <returns></returns>
        IChapterGivenWhen<TStoryData> When(Action<ScenarioContext<TStoryData>, TStoryParams> executeStory);
    }
}