using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Bard.Infrastructure;

[assembly: InternalsVisibleTo("Bard.Grpc")]

namespace Bard.Internal.When
{
    internal class When : IWhen
    {
        private readonly Api _api;
        private readonly EventAggregator _eventAggregator;
        private readonly LogWriter _logWriter;
        protected readonly Action? PreApiCall;

        internal When(Api api, EventAggregator eventAggregator, LogWriter logWriter)
        {
            _api = api;
            _eventAggregator = eventAggregator;
            _logWriter = logWriter;
        }

        internal When(Api api, EventAggregator eventAggregator, LogWriter logWriter,
            Action preApiCall)
        {
            _api = api;
            _eventAggregator = eventAggregator;
            _logWriter = logWriter;
            PreApiCall = preApiCall;
        }

        public IResponse Delete(string route, Action<HttpRequestMessage>? requestSetup = null)
        {
            return CallApi(() => _api.Delete(route, requestSetup));
        }

        public IResponse Patch<TModel>(string route, TModel model, Action<HttpRequestMessage>? requestSetup = null)
        {
            return CallApi(() => _api.Patch(route, model, requestSetup));
        }

        public IResponse Put<TModel>(string route, TModel model, Action<HttpRequestMessage>? requestSetup = null)
        {
            return CallApi(() => _api.Put(route, model, requestSetup));
        }

        public IResponse Post(string route, Action<HttpRequestMessage>? requestSetup = null)
        {
            return CallApi(() => _api.Post(route, requestSetup));
        }

        public IResponse Post<TModel>(string route, TModel model, Action<HttpRequestMessage>? requestSetup = null)
        {
            return CallApi(() => _api.Post(route, model, requestSetup));
        }

        public IResponse Get(string uri, string name, string value, Action<HttpRequestMessage>? requestSetup = null)
        {
            return CallApi(() => _api.Get(uri, name, value, requestSetup));
        }

        public IResponse Get(string uri, IDictionary<string, string> queryParameters,
            Action<HttpRequestMessage>? requestSetup = null)
        {
            return CallApi(() => _api.Get(uri, queryParameters, requestSetup));
        }

        public IResponse Get(string route, Action<HttpRequestMessage>? requestSetup = null)
        {
            return CallApi(() => _api.Get(route, requestSetup));
        }

        private IResponse CallApi(Func<IResponse> callApi)
        {
            PreApiCall?.Invoke();

            WriteHeader();

            var response = callApi();

            _eventAggregator.PublishApiRequest(callApi);

            return response;
        }

        protected void WriteHeader()
        {
            _logWriter.LogHeaderMessage("WHEN");
        }
    }
}