using System;
using Bard.Internal.Given;

namespace Bard
{
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

        protected IChapterWhen<TStoryData> When(
            Action<ScenarioContext<TStoryData>> execute)
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            return new ChapterWhen<TStoryData>(Context, execute);
        }

        protected IChapterGiven<TStoryData, TRequest> Given<TRequest>(Func<TRequest> createRequest)
            where TRequest : new()
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            return new ChapterGiven<TStoryData, TRequest>(Context, createRequest);
        }
    }
}