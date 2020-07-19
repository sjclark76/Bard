using System;
using System.Net.Http;
using Fluent.Testing.Library.Infrastructure;
using Fluent.Testing.Library.Internal;

namespace Fluent.Testing.Library.Configuration
{
    public class FluentScenarioBuilder<T> : IStartingScenarioProvided<T> where T : StoryBook, new()
    {
        private readonly IBadRequestProvider _badRequestProvider;
        private readonly Func<T> _createScenario;
        private readonly HttpClient _httpClient;
        private readonly Action<string>? _logMessage;

        public FluentScenarioBuilder(HttpClient httpClient, Action<string>? logMessage,
            IBadRequestProvider badRequestProvider,
            Func<T> createScenario)
        {
            _httpClient = httpClient;
            _logMessage = logMessage;
            _badRequestProvider = badRequestProvider;
            _createScenario = createScenario;
        }

        public IFluentScenario<T> Build()
        {
            if (_logMessage == null)
                throw new Exception("Log method must be called first.");

            if (_logMessage == null)
                throw new Exception("Then method must be called first.");

            if (_badRequestProvider == null)
                throw new Exception("Use method must be called first.");

            return new FluentScenario<T>(_httpClient, new LogWriter(_logMessage), _badRequestProvider
                , _createScenario);
        }
    }
}