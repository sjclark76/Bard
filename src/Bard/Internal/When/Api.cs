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
        private readonly HttpClient _httpClient;

        internal Api(HttpClient httpClient, IBadRequestProvider badRequestProvider)
        {
            _httpClient = httpClient;
            _badRequestProvider = badRequestProvider;
        }

        public IResponse Put<TModel>(string route, TModel model)
        {
            return PostOrPut(model, (client, messageContent) => client.PutAsync(route, messageContent));
        }

        public IResponse Post<TModel>(string route, TModel model)
        {
            return PostOrPut(model, (client, messageContent) => client.PostAsync(route, messageContent));
        }

        public IResponse Patch<TModel>(string route, TModel model)
        {
            var messageContent = CreateMessageContent(model);
            var responseMessage = AsyncHelper.RunSync(() => PatchAsync(route, messageContent));

            var responseString = AsyncHelper.RunSync(() => responseMessage.Content.ReadAsStringAsync());

            var apiResult = new ApiResult(responseMessage, responseString);
            var response = new Response(apiResult, _badRequestProvider);

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

            var apiResult = new ApiResult(message, content);
            var response = new Response(apiResult, _badRequestProvider);

            return response;
        }

        public IResponse Delete(string route)
        {
            var message = AsyncHelper.RunSync(() => _httpClient.DeleteAsync(route));

            var content = AsyncHelper.RunSync(() => message.Content.ReadAsStringAsync());

            var apiResult = new ApiResult(message, content);
            var response = new Response(apiResult, _badRequestProvider);

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

        private IResponse PostOrPut<TModel>(TModel model,
            Func<HttpClient, StringContent, Task<HttpResponseMessage>> callHttpClient)
        {
            var messageContent = CreateMessageContent(model);
            var responseMessage = AsyncHelper.RunSync(() => callHttpClient(_httpClient, messageContent));

            var responseString = AsyncHelper.RunSync(() => responseMessage.Content.ReadAsStringAsync());

            var apiResult = new ApiResult(responseMessage, responseString);
            var response = new Response(apiResult, _badRequestProvider);
            return response;
        }

        /// <summary>
        ///     HttpClient does not have Patch built in so need to do it ourselves
        /// </summary>
        private async Task<HttpResponseMessage> PatchAsync(string route, HttpContent content)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), route)
            {
                Content = content
            };

            return await _httpClient.SendAsync(request);
        }
    }
}