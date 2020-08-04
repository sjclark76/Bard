using System;
using Bard.Internal.Given;

namespace Bard
{
    public abstract class Chapter<TStoryData> : ISimpleChapter<TStoryData> where TStoryData : class, new()
    {
        internal ScenarioContext<TStoryData>? Context { get; set; }

        object? ISimpleChapter<TStoryData>.ExecutePipeline()
        {
            return Context?.ExecutePipeline();
        }

        void ISimpleChapter<TStoryData>.SetStoryInput(TStoryData? input)
        {
            Context?.SetStoryData(input);
        }

        protected IChapterWhen<TStoryData> When(
            Func<ScenarioContext<TStoryData>, TStoryData> execute)
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            var context = new ScenarioContext<TStoryData>(Context);

            return new ChapterWhen<TStoryData>(context, execute);
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