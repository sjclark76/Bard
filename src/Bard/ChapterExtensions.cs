namespace Bard
{
    public static class ChapterExtensions
    {
        /// <summary>
        /// Get the latest result from the test pipeline and continue on with the test.
        /// </summary>
        /// <param name="chapter"></param>
        /// <param name="useResult">an out parameter to set the result to.</param>
        /// <typeparam name="TChapter"></typeparam>
        /// <typeparam name="TChapterInput"></typeparam>
        /// <returns>The next chapter to go to</returns>
        public static TChapter GetResult<TChapter, TChapterInput>(this TChapter chapter, out TChapterInput? useResult)
            where TChapter
            : ISimpleChapter<TChapterInput>
            where TChapterInput : class, new()
        {
            useResult = chapter.ExecutePipeline() as TChapterInput;
            
            chapter.SetStoryInput(useResult);
            
            return chapter;
        }
    }
}