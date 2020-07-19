namespace Fluent.Testing.Library.Internal.Given
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