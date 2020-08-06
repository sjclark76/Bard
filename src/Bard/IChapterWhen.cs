using System.Runtime.CompilerServices;

namespace Bard
{
    /// <summary>
    /// Interface to help the fluent interface story builder
    /// </summary>
    /// <typeparam name="TStoryData"></typeparam>
    public interface IChapterWhen<TStoryData> where TStoryData : class, new()
    {
        /// <summary>
        /// Then. This is the next Chapter we will go to after the story has completed
        /// </summary>
        /// <param name="memberName">used internally for logging the method name. Can override if you want to provide a different name in the logs.</param>
        /// <typeparam name="TNextChapter">The next chapter we will go to after this story.</typeparam>
        /// <returns>An instance of the next chapter</returns>
        TNextChapter Then<TNextChapter>([CallerMemberName] string memberName = "")
            where TNextChapter : Chapter<TStoryData>, new();
     
        /// <summary>
        /// End. This is the end of the story. No further stories may be executed
        /// </summary>
        /// <param name="memberName">used internally for logging the method name in the logs. Can override if you want to provide a different name in the logs.</param>
        /// <returns>An instance an end chapter which allows executes the allows access to the story data</returns>
        EndChapter<TStoryData> End([CallerMemberName] string memberName = "");
    }
}