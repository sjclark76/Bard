using System.Runtime.CompilerServices;

namespace Bard
{
    public interface IBeginWhen<TStoryInput> where TStoryInput : class, new()
    {
        TNextStep Then<TNextStep>([CallerMemberName] string memberName = "") where TNextStep : Chapter<TStoryInput>, new();
        EndChapter<TStoryInput> End(string memberName = "");
    }
}