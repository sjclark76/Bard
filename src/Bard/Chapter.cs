using System;
using Bard.Internal;
using Bard.Internal.Given;

namespace Bard
{
    public abstract class Chapter<TChapterInput> : ChapterBase where TChapterInput : class, new()
    {
        internal ScenarioContext<TChapterInput>? Context { get; set; }
        
        public void UseResult(Action<TChapterInput> useResult)
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            var pipelineResult = Context.ExecutePipeline();

            if (pipelineResult == null) return;

            var input = (TChapterInput) pipelineResult;

            useResult(input);
        }

        protected IChapterWhen<TOutput> When<TOutput>(Func<ScenarioContext<TChapterInput>, TOutput> execute)
            where TOutput : class, new()
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            var context = new ScenarioContext<TOutput>(Context);
            
            return new ChapterWhen<TChapterInput, TOutput>(context, execute);
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