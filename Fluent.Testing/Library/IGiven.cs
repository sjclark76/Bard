namespace Fluent.Testing.Library
{
    public interface IGiven<out TScenario> where TScenario : StoryBook
    {
        TScenario That { get; }
    }
}