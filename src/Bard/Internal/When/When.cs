using System;
using System.Collections.Generic;
using Bard.Infrastructure;

namespace Bard.Internal.When
{
    internal class When : IWhen
    {
        private readonly Api _api;
        private readonly LogWriter _logWriter;
        private readonly Action _onCalled;

        internal When(Api api, LogWriter logWriter,
            Action onCalled)
        {
            _api = api;
            _logWriter = logWriter;
            _onCalled = onCalled;
        }

        public IResponse Delete(string route)
        {
            return CallApi(() => _api.Delete(route));
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
            WriteHeader();

            _onCalled();

            var response = callApi();

            return response;
        }

        private void WriteHeader()
        {
            _logWriter.WriteStringToConsole("");
            _logWriter.WriteStringToConsole("****************************************");
            _logWriter.WriteStringToConsole("*             WHEN                     *");
            _logWriter.WriteStringToConsole("****************************************");
            _logWriter.WriteStringToConsole("");
        }
    }
}