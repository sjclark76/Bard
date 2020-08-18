using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Bard.Infrastructure;

[assembly: InternalsVisibleTo("Bard.Grpc")]

namespace Bard.Internal.When
{
    internal class When : IWhen
    {
        private readonly Api _api;
        private readonly LogWriter _logWriter;
        protected readonly Action? PreApiCall;

        internal When(Api api, LogWriter logWriter)
        {
            _api = api;
            _logWriter = logWriter;
        }
        
        internal When(Api api, LogWriter logWriter,
            Action preApiCall)
        {
            _api = api;
            _logWriter = logWriter;
            PreApiCall = preApiCall;
        }

        public IResponse Delete(string route)
        {
            return CallApi(() => _api.Delete(route));
        }

        public IResponse Patch<TModel>(string route, TModel model)
        {
            return CallApi(() => _api.Patch(route, model));
        }

        public IResponse Put<TModel>(string route, TModel model)
        {
            return CallApi(() => _api.Put(route, model));
        }

        public IResponse Post<TModel>(string route, TModel model)
        {
            return CallApi(() => _api.Post(route, model));
        }

        public IResponse Get(string uri, string name, string value)
        {
            return CallApi(() => _api.Get(uri, name, value));
        }

        public IResponse Get(string uri, IDictionary<string, string> queryParameters)
        {
            return CallApi(() => _api.Get(uri, queryParameters));
        }

        public IResponse Get(string route)
        {
            return CallApi(() => _api.Get(route));
        }

        private IResponse CallApi(Func<IResponse> callApi)
        {
            PreApiCall?.Invoke();

            WriteHeader();
            
            var response = callApi();

            return response;
        }

        protected void WriteHeader()
        {
            _logWriter.LogHeaderMessage("WHEN");
        }
    }
}