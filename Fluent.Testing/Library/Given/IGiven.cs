namespace Fluent.Testing.Library.Given
{
    public interface IGiven<out TScenario> where TScenario : BeginAScenario
    {
        TScenario That { get; }
    }
}