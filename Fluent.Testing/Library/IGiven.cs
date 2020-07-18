namespace Fluent.Testing.Library
{
    public interface IGiven<out TScenario> where TScenario : BeginAScenario
    {
        TScenario That { get; }
    }
}