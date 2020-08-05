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
        /// <typeparam name="TStoryData"></typeparam>
        /// <returns>The next chapter to go to</returns>
        public static TChapter GetResult<TChapter, TStoryData>(this TChapter chapter, out TStoryData? useResult)
            where TChapter
            : ISimpleChapter<TStoryData>
            where TStoryData : class, new()
        {
            chapter.ExecutePipeline();

            useResult = chapter.GetStoryData();
            
            return chapter;
        }
    }
}