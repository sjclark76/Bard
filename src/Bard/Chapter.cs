using System;
using Bard.Internal;
using Bard.Internal.Given;

namespace Bard
{
    public abstract class Chapter<TChapterInput> where TChapterInput : class, new()
    {
        internal ScenarioContext<TChapterInput>? Context { get; set; }

        public TChapterInput? Result
        {
            get
            {
                if (Context == null)
                    throw new ApplicationException($"{nameof(Context)} has not been set.");

                var pipelineResult = Context.ExecutePipeline();

                return pipelineResult as TChapterInput;
            }
        }
        
        public void UseResult(Action<TChapterInput> useResult)
        {
            if (Result == null)
                throw new BardException("Result is null");
            
            useResult(Result);
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