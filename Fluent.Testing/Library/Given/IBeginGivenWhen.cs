using System.Runtime.CompilerServices;

namespace Fluent.Testing.Library.Given
{
    public interface IBeginGivenWhen<TOutput> where TOutput : class, new()
    {
        TNextStep Then<TNextStep>([CallerMemberName] string memberName = "")
            where TNextStep : ScenarioStep<TOutput>, new();
    }
}