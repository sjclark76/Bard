using System;
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

    public class Foo
    {
        public void Baa()
        {
            HttpClient client = new HttpClient();

            var apiTester = FluentApiRegistry
                .For(client)
                .Log(Console.WriteLine)
                .Build();
        }
    }
}