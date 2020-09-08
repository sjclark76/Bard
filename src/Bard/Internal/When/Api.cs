using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bard.Infrastructure;
using Bard.Internal.Then;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Bard.Internal.When
{
    internal class Api : IApi
    {
        private readonly IBadRequestProvider _badRequestProvider;
        private readonly EventAggregator _eventAggregator;
        private readonly BardHttpClient _httpClient;
        private readonly LogWriter _logWriter;

        internal Api(BardHttpClient httpClient)
        {
            _httpClient = httpClient;
            _badRequestProvider = httpClient.RequestProvider;
            _eventAggregator = httpClient.EventAggregator;
            _logWriter = httpClient.Writer;
        }

        public IResponse Put<TModel>(string route, TModel model)
        {
            return PostOrPut(route, model, (client, messageContent) => client.PutAsync(route, messageContent));
        }

        public IResponse Post(string route)
        {
            return PostOrPut(route, (client, messageContent) => client.PostAsync(route, messageContent));
        }

        public IResponse Post<TModel>(string route, TModel model)
        {
            return PostOrPut(route, model, (client, messageContent) => client.PostAsync(route, messageContent));
        }

        public IResponse Patch<TModel>(string route, TModel model)
        {
            var messageContent = CreateMessageContent(model);
            var responseMessage = AsyncHelper.RunSync(() => _httpClient.PatchAsync(route, messageContent));

            var responseString = AsyncHelper.RunSync(() => responseMessage.Content.ReadAsStringAsync());
            var apiRequest = new ApiRequest(route, messageContent);
            var apiResult = new ApiResult(apiRequest, responseMessage, responseString);
            var response = new Response(_eventAggregator, apiResult, _badRequestProvider, _logWriter);

            return response;
        }

        public IResponse Get(string uri, string name, string value)
        {
            var url = QueryHelpers.AddQueryString(uri, name, value);

            return Get(url);
        }

        public IResponse Get(string uri, IDictionary<string, string> queryParameters)
        {
            var url = QueryHelpers.AddQueryString(uri, queryParameters);

            return Get(url);
        }

        public IResponse Get(string route)
        {
            var message = AsyncHelper.RunSync(() => _httpClient.GetAsync(route));
            var content = AsyncHelper.RunSync(() => message.Content.ReadAsStringAsync());
            var apiRequest = new ApiRequest(route);
            var apiResult = new ApiResult(apiRequest, message, content);
            var response = new Response(_eventAggregator, apiResult, _badRequestProvider, _logWriter);

            return response;
        }

        public IResponse Delete(string route)
        {
            var message = AsyncHelper.RunSync(() => _httpClient.DeleteAsync(route));

            var content = AsyncHelper.RunSync(() => message.Content.ReadAsStringAsync());
            var apiRequest = new ApiRequest(route);
            var apiResult = new ApiResult(apiRequest, message, content);
            var response = new Response(_eventAggregator, apiResult, _badRequestProvider, _logWriter);

            return response;
        }

        private static StringContent CreateMessageContent(object? message)
        {
            var json = message == null
                ? string.Empty
                : JsonConvert.SerializeObject(message, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private IResponse PostOrPut(string route, Func<HttpClient, StringContent, Task<HttpResponseMessage>> callHttpClient)
        {
            var messageContent = CreateMessageContent(null);

            var responseMessage = AsyncHelper.RunSync(() => callHttpClient(_httpClient, messageContent));

            var responseString = AsyncHelper.RunSync(() => responseMessage.Content.ReadAsStringAsync());
            var apiRequest = new ApiRequest(route, messageContent);
            var apiResult = new ApiResult(apiRequest, responseMessage, responseString);
            var response = new Response(_eventAggregator, apiResult, _badRequestProvider, _logWriter);
            return response;
        }

        private IResponse PostOrPut<TModel>(string route, TModel model,
            Func<HttpClient, StringContent, Task<HttpResponseMessage>> callHttpClient)
        {
            var messageContent = CreateMessageContent(model);
            var responseMessage = AsyncHelper.RunSync(() => callHttpClient(_httpClient, messageContent));

            var responseString = AsyncHelper.RunSync(() => responseMessage.Content.ReadAsStringAsync());
            var apiRequest = new ApiRequest(route, messageContent);
            var apiResult = new ApiResult(apiRequest, responseMessage, responseString);
            var response = new Response(_eventAggregator, apiResult, _badRequestProvider, _logWriter);
            return response;
        }
    }
}
