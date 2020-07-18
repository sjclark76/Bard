using Fluent.Testing.Library.Given;

namespace Fluent.Testing.Library.Internal.Given
{
    internal class Given<TScenario> : IGiven<TScenario> where TScenario : BeginAScenario
    {
        public Given(TScenario scenario)
        {
            That = scenario;
        }

        public TScenario That { get; }
    }
}