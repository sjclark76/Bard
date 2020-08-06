using System.Runtime.CompilerServices;

namespace Bard
{
    public interface IBeginWhen<TStoryData> where TStoryData : class, new()
    {
        TNextStep Then<TNextStep>([CallerMemberName] string memberName = "")
            where TNextStep : Chapter<TStoryData>, new();

        EndChapter<TStoryData> End([CallerMemberName] string memberName = "");
    }
}