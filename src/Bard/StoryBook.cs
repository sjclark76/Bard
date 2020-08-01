using System;
using Bard.Internal;
using Bard.Internal.Given;

namespace Bard
{
    public abstract class StoryBook
    {
        internal ScenarioContext? Context { get; set; }

        /// <summary>
        ///     Define the action of your story.
        /// </summary>
        /// <param name="story"></param>
        /// <typeparam name="TStoryOutput"></typeparam>
        /// <returns></returns>
        /// <exception cref="BardConfigurationException"></exception>
        protected IBeginWhen<TStoryOutput> When<TStoryOutput>(Func<ScenarioContext, TStoryOutput> story)
            where TStoryOutput : class, new()
        {
            if (Context == null)
                throw new BardConfigurationException($"{nameof(Context)} has not been set.");

            var context = new ScenarioContext<TStoryOutput>(Context);

            return new BeginWhen<TStoryOutput>(context, story);
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