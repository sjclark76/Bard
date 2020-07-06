using System;
using System.Net.Http;
using Fluent.Testing.Library.Infrastructure;
using Fluent.Testing.Library.Then;
using Fluent.Testing.Library.Then.v1;
using IShouldBe = Fluent.Testing.Library.Then.v1.IShouldBe;

namespace Fluent.Testing.Library.Configuration
{
    public class RegistryData : IStepOne, IStepTwo, IStepThree
    {
        private readonly HttpClient _httpClient;
        private IBadRequestResponse? _badRequestHandler;
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

        IInternalFluentApiTester<Then.v1.IShouldBe> IStepTwo.Build()
        {
            if (_logMessage == null)
                throw new Exception($"{nameof(Log)} method must be called first.");
        
            if (_logMessage == null)
                throw new Exception($"Then method must be called first.");
        
            var then = new Then<ShouldBe>(new LogWriter(_logMessage));
        
            var fluentApi = new When.When<ShouldBe>(_httpClient, new LogWriter(_logMessage),
                response => then.SetTheResponse(response));
        
            return new FluentApiTester<ShouldBe>(fluentApi, then);
        }

        public IStepThree Use<T>() where T : IBadRequestResponse, new()
        {
            _badRequestHandler = new T();

            return this;
        }

        IInternalFluentApiTester<Then.v2.IShouldBe> IStepThree.Build()
        {
            if (_logMessage == null)
                throw new Exception($"{nameof(Log)} method must be called first.");
        
            if (_logMessage == null)
                throw new Exception($"Then method must be called first.");
        
            var then = new Then<Then.v2.ShouldBe>(new LogWriter(_logMessage));
        
            var fluentApi = new When.When<Then.v2.ShouldBe>(_httpClient, new LogWriter(_logMessage),
                response => then.SetTheResponse(response));
        
            return new FluentApiTester<Then.v2.ShouldBe>(fluentApi, then);
        }
    }
}