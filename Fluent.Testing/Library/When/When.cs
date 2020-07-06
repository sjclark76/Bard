using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Fluent.Testing.Library.Infrastructure;
using Fluent.Testing.Library.Then;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Fluent.Testing.Library.When
{
    public class When : IWhen
    {
        private readonly HttpClient _httpClient;
        private readonly LogWriter _logWriter;
        private readonly Action<TheResponse> _setTheResponse;

        internal When(HttpClient httpClient, LogWriter logWriter, Action<TheResponse> setTheResponse)
        {
            _httpClient = httpClient;
            _logWriter = logWriter;
            _setTheResponse = setTheResponse;
        }

        public IResponse Put<TModel>(string route, TModel model)
        {
            return PostOrPut(route, model, (client, messageContent) => client.PutAsync(route, messageContent));
        }

        public IResponse Post<TModel>(string route, TModel model)
        {
            return PostOrPut(route, model, (client, messageContent) => client.PostAsync(route, messageContent));
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
            _logWriter.WriteStringToConsole($"GET {route}");

            var message = AsyncHelper.RunSync(() => _httpClient.GetAsync(route));
            var content = AsyncHelper.RunSync(() => message.Content.ReadAsStringAsync());
            var response = new TheResponse(message, content);

            _logWriter.WriteHttpResponseToConsole(message);

            _setTheResponse.Invoke(response);

            return response;
        }

        private static StringContent CreateMessageContent(object message)
        {
            var json = JsonConvert.SerializeObject(message, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private TheResponse PostOrPut<TModel>(string route, TModel model,
            Func<HttpClient, StringContent, Task<HttpResponseMessage>> callHttpClient)
        {
            var messageContent = CreateMessageContent(model!);
            var message = AsyncHelper.RunSync(() => callHttpClient(_httpClient, messageContent));

            _logWriter.WriteStringToConsole($"REQUEST: {message.RequestMessage.Method.Method} {route}");
            _logWriter.WriteObjectToConsole(model);

            var response = AsyncHelper.RunSync(() => message.Content.ReadAsStringAsync());

            _logWriter.WriteHttpResponseToConsole(message);

            var validator = new TheResponse(message, response);
            _setTheResponse.Invoke(validator);

            return validator;
        }

        // public ResponseValidator Post(string route)
        // {
        //     _logWriter.WriteStringToConsole($"POST {route}");
        //
        //     var message = AsyncHelper.RunSync(() => _httpClient.PostAsync(route, null));
        //     var response = AsyncHelper.RunSync(() => message.Content.ReadAsStringAsync());
        //
        //     _logWriter.WriteHttpResponseToConsole(message);
        //
        //     var validator = new ResponseValidator(message, response);
        //     _setTheResponse.Invoke(validator);
        //
        //     return validator;
        // }
        //
        // public ResponseValidator Patch<TModel>(string route, TModel model)
        // {
        //     _logWriter.WriteStringToConsole($"PATCH {route}");
        //
        //     var messageContent = CreateMessageContent(model!);
        //     var message = AsyncHelper.RunSync(() => PatchAsync(route, messageContent));
        //
        //     _logWriter.WriteHttpResponseToConsole(message);
        //
        //     var response = AsyncHelper.RunSync(() => message.Content.ReadAsStringAsync());
        //     var validator = new ResponseValidator(message, response);
        //
        //     _setTheResponse.Invoke(validator);
        //
        //     return validator;
        // }

        // public ResponseValidator Delete(string route)
        // {
        //     _logWriter.WriteStringToConsole($"DELETE {route}");
        //
        //     var message = AsyncHelper.RunSync(() => _httpClient.DeleteAsync(route));
        //
        //     _logWriter.WriteHttpResponseToConsole(message);
        //
        //     var content = AsyncHelper.RunSync(() => message.Content.ReadAsStringAsync());
        //     var response = new ResponseValidator(message, content);
        //
        //     _setTheResponse.Invoke(response);
        //
        //     return response;
        // }

        // /// <summary>
        // /// HttpClient does not have Patch built in so need to do it ourselves
        // /// </summary>
        // private async Task<HttpResponseMessage> PatchAsync(string route, HttpContent content)
        // {
        //     var request = new HttpRequestMessage(new HttpMethod("PATCH"), route)
        //     {
        //         Content = content
        //     };
        //
        //     return await _httpClient.SendAsync(request);
        // }
    }
}