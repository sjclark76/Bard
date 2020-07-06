using System.Net.Http;

namespace Fluent.Testing.Library.Configuration
{
    public class FluentApiRegistry
    {
        public static IStepOne For(HttpClient httpClient)
        {
            return new RegistryData(httpClient);
        }
    }
}