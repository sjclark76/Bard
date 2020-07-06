using System;
using System.Net.Http;
using Fluent.Testing.Library.Infrastructure;

namespace Fluent.Testing.Library.Configuration
{
    public class RegistryData : IStepOne, IStepTwo, IStepThree
    {
        private readonly HttpClient _httpClient;
        private Action<string>? _logMessage;

        public RegistryData(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IStepTwo Log(Action<string> logMessage)
        {
            _logMessage = logMessage;
            return this;
        }

        public IFluentApiTester Build()
        {
            if (_logMessage == null)
                throw new Exception($"{nameof(Log)} method must be called first.");

            if (_logMessage == null)
                throw new Exception($"{nameof(Then.Then)} method must be called first.");

            var then = new Then.Then(new LogWriter(_logMessage));

            var fluentApi = new When.When(_httpClient, new LogWriter(_logMessage),
                response => then.SetTheResponse(response));

            return new FluentApiTester(fluentApi, then);
        }
    }
}