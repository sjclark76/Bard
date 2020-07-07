using System;
using System.Net.Http;
using Fluent.Testing.Library.Given;
using Fluent.Testing.Library.Infrastructure;
using Fluent.Testing.Library.Then;

namespace Fluent.Testing.Library.Configuration
{
    public class ScenarioHostBuilder : IHttpClientProvided, ILoggerProvided, ICustomErrorProviderSupplied,
        IStartingScenarioProvided
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

        public IInternalFluentApiTester Build()
        {
            if (_logMessage == null)
                throw new Exception($"{nameof(Log)} method must be called first.");

            if (_logMessage == null)
                throw new Exception("Then method must be called first.");

            if (_badRequestProvider == null)
                throw new Exception("Use method must be called first.");

            return new ScenarioHost(_httpClient, new LogWriter(_logMessage),
                result => new Response(result, _badRequestProvider));
        }

        public ICustomErrorProviderSupplied Use<T>() where T : IBadRequestProvider, new()
        {
            _badRequestProvider = new T();

            return this;
        }

        public IStartingScenarioProvided AndBeginsWithScenario<TScenario>() where TScenario : IBeginAScenario, new()
        {
            throw new NotImplementedException();
        }
    }
}