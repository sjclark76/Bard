using System;
using Bard.Internal.Given;

namespace Bard
{
    public interface IFoo<TChapterInput> where TChapterInput : class, new()
    {
        internal object? ExecutePipeline();

        void SetStoryInput(TChapterInput? input);
    }

    public static class ChapterExtensions
    {
        public static TChapter GetResult<TChapter, TChapterInput>(this TChapter chapter, out TChapterInput? useResult)
            where TChapter
            : IFoo<TChapterInput>
            where TChapterInput : class, new()
        {
            useResult = chapter.ExecutePipeline() as TChapterInput;
            
            chapter.SetStoryInput(useResult);
            return chapter;
        }
    }

    public abstract class Chapter<TChapterInput> : IFoo<TChapterInput> where TChapterInput : class, new()
    {
        internal ScenarioContext<TChapterInput>? Context { get; set; }

        object? IFoo<TChapterInput>.ExecutePipeline()
        {
            return Context?.ExecutePipeline();
        }

        public void SetStoryInput(TChapterInput? input) 
        {
            Context?.SetStoryInput(input);
        }

        protected IChapterWhen<TStoryOutput> When<TStoryOutput>(Func<ScenarioContext<TChapterInput>, TStoryOutput> execute)
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