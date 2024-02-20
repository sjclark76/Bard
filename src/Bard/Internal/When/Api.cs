using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bard.Infrastructure;
using Bard.Internal.Then;
using Microsoft.AspNetCore.WebUtilities;

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

        public IResponse Put<TModel>(string route, TModel model, Action<HttpRequestMessage>? requestSetup = null)
        {
            using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, route);
            return PostOrPut(model, (client, messageContent) =>
            {
                httpRequestMessage.Content = messageContent;

                requestSetup?.Invoke(httpRequestMessage);

                return client.SendAsync(httpRequestMessage);
            });

        }

        public IResponse Post(string route, Action<HttpRequestMessage>? requestSetup = null)
        {
            using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, route);
            return PostOrPut((client, messageContent) =>
            {
                httpRequestMessage.Content = messageContent;
                
                requestSetup?.Invoke(httpRequestMessage);
                
                return client.SendAsync(httpRequestMessage);
            });
        }

        public IResponse Post<TModel>(string route, TModel model, Action<HttpRequestMessage>? requestSetup = null)
        {
            using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, route);
            return PostOrPut(model, (client, messageContent) =>
            {
                httpRequestMessage.Content = messageContent;
                
                requestSetup?.Invoke(httpRequestMessage);
                
                return client.SendAsync(httpRequestMessage);
            });
        }

        public IResponse Patch<TModel>(string route, TModel model, Action<HttpRequestMessage>? requestSetup = null)
        {
            var messageContent = CreateMessageContent(model);
            using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, route);
            var responseMessage = AsyncHelper.RunSync(() =>
            {
                httpRequestMessage.Content = messageContent;
                
                requestSetup?.Invoke(httpRequestMessage);
                
                return _httpClient.SendAsync(httpRequestMessage);
            });

            AsyncHelper.RunSync(() => responseMessage.Content.ReadAsStringAsync());

            var response = new Response(_eventAggregator, _httpClient.Result, _badRequestProvider, _logWriter);

            return response;
        }

        public IResponse Get(string uri, string name, string value, Action<HttpRequestMessage>? requestSetup = null)
        {
            var url = QueryHelpers.AddQueryString(uri, name, value);

            return Get(url, requestSetup);
        }

        public IResponse Get(string uri, IDictionary<string, string> queryParameters, Action<HttpRequestMessage>? requestSetup = null)
        {
            var url = QueryHelpers.AddQueryString(uri, queryParameters);

            return Get(url, requestSetup);
        }

        public IResponse Get(string route, Action<HttpRequestMessage>? requestSetup = null)
        {
            using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, route);
            var message = AsyncHelper.RunSync(() =>
            {
                requestSetup?.Invoke(httpRequestMessage);
                
                return _httpClient.SendAsync(httpRequestMessage);
            });
            
            AsyncHelper.RunSync(() => message.Content.ReadAsStringAsync());

            var response = new Response(_eventAggregator, _httpClient.Result, _badRequestProvider, _logWriter);

            return response;
        }

        public IResponse Delete(string route, Action<HttpRequestMessage>? requestSetup = null)
        {
            using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, route);
            var message = AsyncHelper.RunSync(() =>
            {
                requestSetup?.Invoke(httpRequestMessage);

                return _httpClient.SendAsync(httpRequestMessage);
            });

            AsyncHelper.RunSync(() => message.Content.ReadAsStringAsync());

            var response = new Response(_eventAggregator, _httpClient.Result, _badRequestProvider, _logWriter);

            return response;
        }

        private StringContent CreateMessageContent(object? message)
        {
            var json = message == null
                ? string.Empty
                : _logWriter.Serializer.Serialize(message);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private IResponse PostOrPut(Func<BardHttpClient, StringContent, Task<HttpResponseMessage>> callHttpClient)
        {
            var messageContent = CreateMessageContent(null);

            AsyncHelper.RunSync(() => callHttpClient(_httpClient, messageContent));

            var response = new Response(_eventAggregator, _httpClient.Result, _badRequestProvider, _logWriter);

            return response;
        }

        private IResponse PostOrPut<TModel>(TModel model,
            Func<BardHttpClient, StringContent, Task<HttpResponseMessage>> callHttpClient)
        {
            var messageContent = CreateMessageContent(model);
            AsyncHelper.RunSync(() => callHttpClient(_httpClient, messageContent));

            var response = new Response(_eventAggregator, _httpClient.Result, _badRequestProvider, _logWriter);

            return response;
        }
    }
}