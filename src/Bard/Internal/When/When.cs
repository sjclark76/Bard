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
        protected readonly LogWriter LogWriter;
        private readonly Action _preApiCall;

        internal When(Api api, LogWriter logWriter,
            Action preApiCall)
        {
            _api = api;
            LogWriter = logWriter;
            _preApiCall = preApiCall;
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
            _preApiCall();

            WriteHeader();
            
            var response = callApi();

            return response;
        }

        protected void WriteHeader()
        {
            LogWriter.WriteStringToConsole("");
            LogWriter.WriteStringToConsole("****************************************");
            LogWriter.WriteStringToConsole("*             WHEN                     *");
            LogWriter.WriteStringToConsole("****************************************");
            LogWriter.WriteStringToConsole("");
        }
    }
}