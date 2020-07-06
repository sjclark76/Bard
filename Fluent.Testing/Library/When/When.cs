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
    public class When<TShouldBe> : IWhen<TShouldBe> where TShouldBe : ShouldBeBase
    {
        private readonly HttpClient _httpClient;
        private readonly LogWriter _logWriter;
        private readonly Func<ApiResult, IResponse<TShouldBe>> _responseFactory;

        internal When(FluentApiTester<TShouldBe> parent, HttpClient httpClient, LogWriter logWriter, Func<ApiResult, IResponse<TShouldBe>> responseFactory)
        {
            Parent = parent;
            _httpClient = httpClient;
            _logWriter = logWriter;
            _responseFactory = responseFactory;
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
            //var response = new Response<TShouldBe>(message, content);

            _logWriter.WriteHttpResponseToConsole(message);

            var apiResult = new ApiResult(message, content);

            Publish(apiResult);
            
            return _responseFactory(apiResult);

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

        private IResponse<TShouldBe> PostOrPut<TModel>(string route, TModel model,
            Func<HttpClient, StringContent, Task<HttpResponseMessage>> callHttpClient)
        {
            var messageContent = CreateMessageContent(model);
            var responseMessage = AsyncHelper.RunSync(() => callHttpClient(_httpClient, messageContent));

            _logWriter.WriteStringToConsole($"REQUEST: {responseMessage.RequestMessage.Method.Method} {route}");
            _logWriter.WriteObjectToConsole(model);

            var responseString = AsyncHelper.RunSync(() => responseMessage.Content.ReadAsStringAsync());

            _logWriter.WriteHttpResponseToConsole(responseMessage);

            var apiResult = new ApiResult(responseMessage, responseString);
            
            Publish(apiResult);
            
            return _responseFactory(apiResult);
        }

        private void Publish(ApiResult apiResult)
        {
            Parent.Publish(apiResult);
        }

        internal FluentApiTester<TShouldBe> Parent { get; set; }
    }
}