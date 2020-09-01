using System;
using Bard.Internal.Exception;
using Bard.Internal.Given;

namespace Bard
{
    /// <summary>
    ///     Abstract StoryBook class to inherit from when creating a StoryBook
    /// </summary>
    /// <typeparam name="TStoryData">The Story Data that will flow through the stories.</typeparam>
    public abstract class StoryBook<TStoryData> where TStoryData : class, new()
    {
        internal ScenarioContext<TStoryData>? Context { get; set; }

        /// <summary>
        ///     Define the action of your story.
        /// </summary>
        /// <param name="story"></param>
        /// <returns></returns>
        /// <exception cref="BardConfigurationException"></exception>
        protected IChapterWhen<TStoryData> When(Action<ScenarioContext<TStoryData>> story)
        {
            if (Context == null)
                throw new BardConfigurationException($"{nameof(Context)} has not been set.");
           
            return new ChapterWhen<TStoryData>(Context, story);
        }

        /// <summary>
        ///     Define the parameters of your story.
        /// </summary>
        /// <param name="storyParameter"></param>
        /// <typeparam name="TStoryParams"></typeparam>
        /// <returns></returns>
        /// <exception cref="BardConfigurationException"></exception>
        protected IChapterGiven<TStoryData, TStoryParams> Given<TStoryParams>(Func<TStoryData, TStoryParams> storyParameter)
            where TStoryParams : new()
        {
            if (Context == null)
                throw new BardConfigurationException($"{nameof(Context)} has not been set.");

            return new ChapterGiven<TStoryData, TStoryParams>(Context, storyParameter);
        }
    }
}