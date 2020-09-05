namespace Bard
{
    /// <summary>
    ///     Extension class for working with Chapters.
    /// </summary>
    public static class ChapterExtensions
    {
        /// <summary>
        ///     Get the latest result from the test pipeline and continue on with the test.
        /// </summary>
        /// <param name="chapter"></param>
        /// <param name="storyData">an out parameter to set the result to.</param>
        /// <typeparam name="TChapter"></typeparam>
        /// <typeparam name="TStoryData"></typeparam>
        /// <returns>The next chapter to go to</returns>
        public static TChapter GetResult<TChapter, TStoryData>(this TChapter chapter, out TStoryData storyData)
            where TChapter
            : ISimpleChapter<TStoryData>
            where TStoryData : class, new()
        {
            chapter.ExecutePipeline();

            var original = chapter.GetStoryData();
            
            var inst = typeof(TStoryData).GetMethod("MemberwiseClone", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            var clone = inst?.Invoke(original, null);
            
            // ReSharper disable once JoinNullCheckWithUsage
            if (clone == null)
                throw new BardException("Unable to clone Story Data");
            
            storyData = (TStoryData) clone;

            return chapter;
        }
    }
}