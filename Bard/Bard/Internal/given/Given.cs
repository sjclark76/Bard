namespace Bard.Internal.given
{
    internal class Given<TScenario> : IGiven<TScenario> where TScenario : StoryBook
    {
        public Given(TScenario scenario)
        {
            That = scenario;
        }

        public TScenario That { get; }
    }
}