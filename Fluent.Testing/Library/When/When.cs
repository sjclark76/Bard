using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Fluent.Testing.Library.Infrastructure;
using Fluent.Testing.Library.Then;
using Fluent.Testing.Library.Then.v1;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Fluent.Testing.Library.When
{
    public class When<TShouldBe> : IWhen<TShouldBe> where TShouldBe : ShouldBeBase, new()
    {
        private readonly HttpClient _httpClient;
        private readonly LogWriter _logWriter;
        private readonly Action<IResponse<TShouldBe>> _setTheResponse;

        internal When(HttpClient httpClient, LogWriter logWriter, Action<IResponse<TShouldBe>> setTheResponse)
        {
            _httpClient = httpClient;
            _logWriter = logWriter;
            _setTheResponse = setTheResponse;
        }

        public IResponse<TShouldBe> Put<TModel>(string route, TModel model)
        {
            return PostOrPut(route, model, (client, messageContent) => client.PutAsync(route, messageContent));
        }

        public IResponse<TShouldBe> Post<TModel>(string route, TModel model)
        {
            return PostOrPut(route, model, (client, messageContent) => client.PostAsync(route, messageContent));
        }

        public IResponse<TShouldBe> Get(string uri, string name, string value)
        {
            var url = QueryHelpers.AddQueryString(uri, name, value);

            return Get(url);
        }

        public IResponse<TShouldBe> Get(string uri, IDictionary<string, string> queryParameters)
        {
            var url = QueryHelpers.AddQueryString(uri, queryParameters);

            return Get(url);
        }

        public IResponse<TShouldBe> Get(string route)
        {
            _logWriter.WriteStringToConsole($"GET {route}");

            var message = AsyncHelper.RunSync(() => _httpClient.GetAsync(route));
            var content = AsyncHelper.RunSync(() => message.Content.ReadAsStringAsync());
            var response = new TheResponse<TShouldBe>(message, content);

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

        private IResponse<TShouldBe> PostOrPut<TModel>(string route, TModel model,
            Func<HttpClient, StringContent, Task<HttpResponseMessage>> callHttpClient)
        {
            var messageContent = CreateMessageContent(model!);
            var message = AsyncHelper.RunSync(() => callHttpClient(_httpClient, messageContent));

            _logWriter.WriteStringToConsole($"REQUEST: {message.RequestMessage.Method.Method} {route}");
            _logWriter.WriteObjectToConsole(model);

            var response = AsyncHelper.RunSync(() => message.Content.ReadAsStringAsync());

            _logWriter.WriteHttpResponseToConsole(message);

            var validator = new TheResponse<TShouldBe>(message, response);
            _setTheResponse.Invoke(validator);

            return validator;
        }
    }
}