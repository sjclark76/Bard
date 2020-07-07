using System.Net.Http;

namespace Fluent.Testing.Library.Configuration
{
    public class ScenarioHostConfiguration
    {
        public static IHttpClientProvided TheApiUses(HttpClient httpClient)
        {
            return new ScenarioHostBuilder(httpClient);
        }
    }
}