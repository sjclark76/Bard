using System;
using Bard.Internal;
using Bard.Internal.Given;

namespace Bard
{
    public abstract class StoryBook : ChapterBase
    {
        /// <summary>
        /// Define the action of your story.
        /// </summary>
        /// <param name="story"></param>
        /// <typeparam name="TOutput"></typeparam>
        /// <returns></returns>
        /// <exception cref="BardConfigurationException"></exception>
        protected IBeginWhen<TOutput> When<TOutput>(Func<IScenarioContext, TOutput> story)
            where TOutput : class, new()
        {
            if (Context == null)
                throw new BardConfigurationException($"{nameof(Context)} has not been set.");

            return new BeginWhen<TOutput>(Context, story);
        }

        /// <summary>
        /// Define the parameters of your story.
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