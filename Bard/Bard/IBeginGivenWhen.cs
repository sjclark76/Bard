using System.Runtime.CompilerServices;

namespace Bard
{
    public interface IBeginGivenWhen<TOutput> where TOutput : class, new()
    {
        TNextStep Then<TNextStep>([CallerMemberName] string memberName = "")
            where TNextStep : Chapter<TOutput>, new();
    }
}