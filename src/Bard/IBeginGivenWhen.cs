using System.Runtime.CompilerServices;

namespace Bard
{
    public interface IBeginGivenWhen<TStoryData> where TStoryData : class, new()
    {
        TNextStep Then<TNextStep>([CallerMemberName] string memberName = "")
            where TNextStep : Chapter<TStoryData>, new();
    }
}