using System.Runtime.CompilerServices;

namespace Bard
{
    public interface IChapterGivenWhen<TStoryData> where TStoryData : class, new()
    {
        TNextChapter Then<TNextChapter>([CallerMemberName] string memberName = "")
            where TNextChapter : Chapter<TStoryData>, new();
    }
}