using System;
using Bard.Internal.Exception;
using Bard.Internal.Given;

namespace Bard
{
    /// <summary>
    /// Abstract StoryBook class to inherit from when creating a StoryBook
    /// </summary>
    /// <typeparam name="TStoryData">The Story Data that will flow through the stories.</typeparam>
    public abstract class StoryBook<TStoryData> where TStoryData : class, new()
    {
        internal ScenarioContext<TStoryData>? Context { get; set; }

        /// <summary>
        ///     Define the action of your story.
        /// </summary>
        /// <param name="story"></param>
        /// <typeparam name="TStoryData"></typeparam>
        /// <returns></returns>
        /// <exception cref="BardConfigurationException"></exception>
        protected IChapterWhen<TStoryData> When(Action<ScenarioContext<TStoryData>> story)
        {
            if (Context == null)
                throw new BardConfigurationException($"{nameof(Context)} has not been set.");

            var context = new ScenarioContext<TStoryData>(Context);
            
            context.SetStoryData(new TStoryData());

            return new ChapterWhen<TStoryData>(context, story);
        }

        /// <summary>
        ///     Define the parameters of your story.
        /// </summary>
        /// <param name="storyParameter"></param>
        /// <typeparam name="TRequest"></typeparam>
        /// <returns></returns>
        /// <exception cref="BardConfigurationException"></exception>
        protected IBeginGiven<TRequest> Given<TRequest>(Func<TRequest> storyParameter)
        {
            if (Context == null)
                throw new BardConfigurationException($"{nameof(Context)} has not been set.");

            return new BeginGiven<TRequest>(Context, storyParameter);
        }
    }
}