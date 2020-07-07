namespace Fluent.Testing.Library.Given
{
    public class Given<TScenario> : IGiven<TScenario> where TScenario : IBeginAScenario
    {
        public Given(TScenario scenario)
        {
            That = scenario;
        }

        public TScenario That { get; }
    }
}