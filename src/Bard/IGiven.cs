namespace Bard
{
    public interface IGiven<out TScenario> where TScenario : StoryBook
    {
        TScenario That { get; }
    }
}