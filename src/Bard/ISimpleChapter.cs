namespace Bard
{
    public interface ISimpleChapter<in TChapterInput> where TChapterInput : class, new()
    {
        internal object? ExecutePipeline();

        void SetStoryInput(TChapterInput? input);
    }
}