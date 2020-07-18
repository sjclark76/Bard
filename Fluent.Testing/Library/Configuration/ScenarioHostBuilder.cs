using System;
using System.Net.Http;

namespace Fluent.Testing.Library.Configuration
{
    public class ScenarioHostBuilder : IHttpClientProvided, ILoggerProvided, ICustomErrorProviderSupplied

    {
        private readonly HttpClient _httpClient;
        private IBadRequestProvider _badRequestProvider;
        private Action<string>? _logMessage;

        public ScenarioHostBuilder(HttpClient httpClient)
        {
            _badRequestProvider = new DefaultBadRequestProvider();
            _httpClient = httpClient;
        }

        public ILoggerProvided Log(Action<string> logMessage)
        {
            _logMessage = logMessage;
            return this;
        }

        public ICustomErrorProviderSupplied Use<T>() where T : IBadRequestProvider, new()
        {
            _badRequestProvider = new T();
            return this;
        }

        public IStartingScenarioProvided<TScenario> AndBeginsWithScenario<TScenario>(Func<TScenario> createScenario)
            where TScenario : BeginAScenario, new()
        {
            return new FluentScenarioBuilder<TScenario>(_httpClient, _logMessage, _badRequestProvider, createScenario);
        }
    }
}