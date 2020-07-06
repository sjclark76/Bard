using System;
using System.Net.Http;

namespace Fluent.Testing.Library.Configuration
{
    public class RegistryData : IStepOne, IStepTwo, IStepThree
    {
        private readonly HttpClient _httpClient;
        private Action<string>? _logMessage;
        private Action<Response>? _setTheResponse;

        public RegistryData(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IStepTwo Log(Action<string> logMessage)
        {
            _logMessage = logMessage;
            return this;
        }

        public IStepThree Then(IThen then)
        {
            then.SetTheResponse()

            return this;
        }
    }
}