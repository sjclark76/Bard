using System.Runtime.CompilerServices;

namespace Bard
{
    public interface IChapterGivenWhen<TOutput> where TOutput : class, new()
    {
        TNextChapter Then<TNextChapter>([CallerMemberName] string memberName = "") where TNextChapter : Chapter<TOutput>, new();
    }
}