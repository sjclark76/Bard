using System;
using Bard.Internal.Given;

namespace Bard
{
    public abstract class Chapter<TChapterInput> : StoryBookBase where TChapterInput : class, new()
    {
        public void UseResult(Action<TChapterInput> useResult)
        {
            var pipelineResult = Context?.ExecutePipeline();

            if (pipelineResult == null) return;

            var input = (TChapterInput) pipelineResult;

            useResult(input);
        }

        protected IChapterWhen<TOutput> When<TOutput>(Func<ScenarioContext, TChapterInput, TOutput> execute)
            where TOutput : class, new()
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            return new ChapterWhen<TChapterInput, TOutput>(Context, execute);
        }

        protected IChapterGiven<TChapterInput, TRequest> Given<TRequest>(Func<TRequest> createRequest)
            where TRequest : new()
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            return new ChapterGiven<TChapterInput, TRequest>(Context, createRequest);
        }
    }
}