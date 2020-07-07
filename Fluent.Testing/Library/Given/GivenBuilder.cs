using Fluent.Testing.Library.Configuration;

namespace Fluent.Testing.Library.Given
{
    public class GivenBuilder<TScenario> where TScenario: IBeginAScenario, new()
    {
        public Given<TScenario> Build()
        {
            return new Given<TScenario>(new TScenario());
        }
    }
}