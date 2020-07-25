using System;
using Bard.Internal;
using Bard.Internal.Given;

namespace Bard
{
    public abstract class Chapter<TChapterInput> : StoryBookBase where TChapterInput : class, new()
    {
        public void UseResult(Action<TChapterInput> useResult)
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");
            
            var pipelineResult = ((ScenarioContext) Context).ExecutePipeline();

            if (pipelineResult == null) return;

            var input = (TChapterInput) pipelineResult;

            useResult(input);
        }

        protected IChapterWhen<TOutput> When<TOutput>(Func<IScenarioContext, TChapterInput, TOutput> execute)
            where TOutput : class, new()
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            return new ChapterWhen<TChapterInput, TOutput>((ScenarioContext) Context, execute);
        }

        protected IChapterGiven<TChapterInput, TRequest> Given<TRequest>(Func<TRequest> createRequest)
            where TRequest : new()
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            return new ChapterGiven<TChapterInput, TRequest>((ScenarioContext) Context, createRequest);
        }
    }
}