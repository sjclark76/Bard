using System;
using Bard.Internal.Given;

namespace Bard
{
    /// <summary>
    ///     A Chapter defines the group of stories that can be enacted.
    /// </summary>
    /// <typeparam name="TStoryData">The Data for the story</typeparam>
    public abstract class Chapter<TStoryData> : ISimpleChapter<TStoryData> where TStoryData : class, new()
    {
        internal ScenarioContext<TStoryData>? Context { get; set; }

        void ISimpleChapter<TStoryData>.ExecutePipeline()
        {
            Context?.ExecutePipeline();
        }

        TStoryData ISimpleChapter<TStoryData>.GetStoryData()
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            return Context.StoryData;
        }

        /// <summary>
        ///     Define what happens in your story through an Action.
        /// </summary>
        /// <param name="execute">The action that will be executed when the test is run.</param>
        /// <returns>IChapterWhen</returns>
        /// <exception cref="BardException">If something has gone wrong internally.</exception>
        protected IChapterWhen<TStoryData> When(
            Action<ScenarioContext<TStoryData>> execute)
        {
            if (Context == null)
                throw new BardException($"{nameof(Context)} has not been set.");

            return new ChapterWhen<TStoryData>(Context, execute);
        }
 
        /// <summary>
        ///     Define the parameters that the story will received through an Action.
        /// </summary>
        /// <param name="storyParams">The function that will create the parameters for the story.</param>
        /// <returns>IChapterGiven</returns>
        /// <exception cref="BardException">If something has gone wrong internally.</exception>
        protected IChapterGiven<TStoryData, TStoryParams> Given<TStoryParams>(Func<TStoryData, TStoryParams> storyParams)
            where TStoryParams : new()
        {
            if (Context == null)
                throw new BardException($"{nameof(Context)} has not been set.");

            return new ChapterGiven<TStoryData, TStoryParams>(Context, storyParams);
        }
    }
}