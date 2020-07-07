namespace Fluent.Testing.Library.Given
{
    public class GivenFactory
    {
        public static GivenBuilder<T> StartsWith<T>() where T : IBeginAScenario, new()
        {
            return new GivenBuilder<T>();
        }
    }
}