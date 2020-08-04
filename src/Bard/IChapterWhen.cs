using System.Runtime.CompilerServices;

namespace Bard
{
    public interface IChapterWhen<TStoryData> where TStoryData : class, new()
    {
        TNextChapter Then<TNextChapter>([CallerMemberName] string memberName = "")
            where TNextChapter : Chapter<TStoryData>, new();

        EndChapter<TStoryData> End([CallerMemberName] string memberName = "");
    }
}