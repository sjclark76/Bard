using System;
using System.Net.Http;
using Fluent.Testing.Library.Given;
using Fluent.Testing.Library.Infrastructure;
using Fluent.Testing.Library.Then;
using Fluent.Testing.Library.Then.Advanced;
using Fluent.Testing.Library.Then.Basic;
using IShouldBe = Fluent.Testing.Library.Then.Advanced.IShouldBe;

namespace Fluent.Testing.Library.Configuration
{
    public class RegistryData : IHttpClientProvided, ILoggerProvided, ICustomErrorProviderSupplied, IStartingScenarioProvided
    {
        private readonly HttpClient _httpClient;
        private IBadRequestResponse? _badRequestHandler;
        private Action<string>? _logMessage;

        public RegistryData(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public ILoggerProvided Log(Action<string> logMessage)
        {
            _logMessage = logMessage;
            return this;
        }

        public IInternalFluentApiTester<IShouldBe> Build()
        {
            if (_logMessage == null)
                throw new Exception($"{nameof(Log)} method must be called first.");

            if (_logMessage == null)
                throw new Exception("Then method must be called first.");

            if (_badRequestHandler == null)
                throw new Exception("Use method must be called first.");
            
            return new FluentApiTester<AdvancedShouldBe>(_httpClient, new LogWriter(_logMessage),
                result => new Response<AdvancedShouldBe>(new AdvancedShouldBe(result, _badRequestHandler)));
        }

        IInternalFluentApiTester<Then.Basic.IShouldBe> ILoggerProvided.Build()
        {
            if (_logMessage == null)
                throw new Exception($"{nameof(Log)} method must be called first.");

            if (_logMessage == null)
                throw new Exception("Then method must be called first.");

            return new FluentApiTester<BasicShouldBe>(_httpClient, new LogWriter(_logMessage),
                result => new Response<BasicShouldBe>(new BasicShouldBe(result)));
        }

        public ICustomErrorProviderSupplied Use<T>() where T : IBadRequestResponse, new()
        {
            _badRequestHandler = new T();

            return this;
        }

        public IStartingScenarioProvided AndBeginsWithScenario<TScenario>() where TScenario : IBeginAScenario, new()
        {
            throw new NotImplementedException();
        }
    }
}