using System.Runtime.CompilerServices;

namespace Fluent.Testing.Library
{
    public interface IChapterWhen<TOutput> where TOutput : class, new()
    {
        TNextChapter Then<TNextChapter>([CallerMemberName] string memberName = "") where TNextChapter : Chapter<TOutput>, new();
    }
}