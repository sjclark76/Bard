using System;
using System.Collections.Generic;
using System.Net.Http;
using Fluent.Testing.Library.Infrastructure;
using Fluent.Testing.Library.Then;

namespace Fluent.Testing.Library.When
{
    public class When : IWhen
    {
        private readonly IBadRequestProvider _badRequestProvider;
        private readonly Action _onCalled;
        private readonly Action<IResponse> _onResponsePublished;
        private readonly Api _api;

        internal When(HttpClient httpClient, LogWriter logWriter,
            IBadRequestProvider badRequestProvider,
            Action onCalled,
            Action<IResponse> onResponsePublished)
        {
            _api = new Api(httpClient, logWriter);
            _badRequestProvider = badRequestProvider;
            _onCalled = onCalled;

            _onResponsePublished = onResponsePublished;
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

        private IResponse CallApi(Func<ApiResult> callApi)
        {
            _onCalled();

            var apiResult = callApi();

            var response = new Response(apiResult, _badRequestProvider);

            _onResponsePublished(response);

            return response;
        }
    }
}