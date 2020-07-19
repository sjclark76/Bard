using System.Runtime.CompilerServices;

namespace Fluent.Testing.Library
{
    public interface IBeginGivenWhen<TOutput> where TOutput : class, new()
    {
        TNextStep Then<TNextStep>([CallerMemberName] string memberName = "")
            where TNextStep : Chapter<TOutput>, new();
    }
}