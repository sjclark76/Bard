using System;
using System.Collections.Generic;
using System.Net.Http;
using Bard.Infrastructure;

namespace Bard.Internal.When
{
    internal class When : IWhen, IApi
    {
        private readonly Api _api;
        private readonly Action _onCalled;
        private readonly Action<IResponse> _onResponsePublished;

        internal When(HttpClient httpClient, LogWriter logWriter,
            IBadRequestProvider badRequestProvider,
            Action onCalled,
            Action<IResponse> onResponsePublished)
        {
            _api = new Api(httpClient, logWriter, badRequestProvider);
            _onCalled = onCalled;

            _onResponsePublished = onResponsePublished;
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
            _onCalled();

            var response = callApi();

            _onResponsePublished(response);

            return response;
        }
    }
}