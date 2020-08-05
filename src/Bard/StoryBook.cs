using System;
using Bard.Internal.Exception;
using Bard.Internal.Given;

namespace Bard
{
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
        protected IBeginWhen<TStoryData> When(Action<ScenarioContext<TStoryData>> story)
        {
            if (Context == null)
                throw new BardConfigurationException($"{nameof(Context)} has not been set.");

            var context = new ScenarioContext<TStoryData>(Context);
            
            context.SetStoryData(new TStoryData());

            return new BeginWhen<TStoryData>(context, story);
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