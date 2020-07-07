using System.Net.Http;

namespace Fluent.Testing.Library.Configuration
{
    public class FluentApiRegistry
    {
        public static IHttpClientProvided TheApiUses(HttpClient httpClient)
        {
            return new RegistryData(httpClient);
        }
    }
}