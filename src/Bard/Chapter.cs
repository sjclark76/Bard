using System;
using Bard.Internal.Given;

namespace Bard
{
    public abstract class Chapter<TChapterInput> : ISimpleChapter<TChapterInput> where TChapterInput : class, new()
    {
        internal ScenarioContext<TChapterInput>? Context { get; set; }

        object? ISimpleChapter<TChapterInput>.ExecutePipeline()
        {
            return Context?.ExecutePipeline();
        }

        void ISimpleChapter<TChapterInput>.SetStoryInput(TChapterInput? input)
        {
            Context?.SetStoryInput(input);
        }

        protected IChapterWhen<TStoryOutput> When<TStoryOutput>(
            Func<ScenarioContext<TChapterInput>, TStoryOutput> execute)
            where TStoryOutput : class, new()
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            var context = new ScenarioContext<TStoryOutput>(Context);

            return new ChapterWhen<TChapterInput, TStoryOutput>(context, execute);
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