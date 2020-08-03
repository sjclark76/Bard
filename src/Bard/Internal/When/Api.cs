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
    internal class Api : IApi, IObservable<Response>
    {
        private readonly IBadRequestProvider _badRequestProvider;
        private readonly HttpClient _httpClient;
        private readonly LogWriter _logWriter;
        private readonly List<IObserver<Response>> _observers;

        internal Api(HttpClient httpClient, LogWriter logWriter, IBadRequestProvider badRequestProvider)
        {
            _httpClient = httpClient;
            _logWriter = logWriter;
            _badRequestProvider = badRequestProvider;
            _observers = new List<IObserver<Response>>();
        }

        public IResponse Put<TModel>(string route, TModel model)
        {
            return PostOrPut(route, model, (client, messageContent) => client.PutAsync(route, messageContent));
        }

        public IResponse Post<TModel>(string route, TModel model)
        {
            return PostOrPut(route, model, (client, messageContent) => client.PostAsync(route, messageContent));
        }

        public IResponse Patch<TModel>(string route, TModel model)
        {
            var messageContent = CreateMessageContent(model);
            var responseMessage = AsyncHelper.RunSync(() => PatchAsync(route, messageContent));
           
            _logWriter.WriteStringToConsole($"REQUEST: {responseMessage.RequestMessage.Method.Method} {route}");
            _logWriter.WriteObjectToConsole(model);

            var responseString = AsyncHelper.RunSync(() => responseMessage.Content.ReadAsStringAsync());

            _logWriter.WriteHttpResponseToConsole(responseMessage);

            var apiResult = new ApiResult(responseMessage, responseString);
            var response = new Response(apiResult, _badRequestProvider);
            PublishApiResponse(response);
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
            _logWriter.WriteStringToConsole($"GET {route}");

            var message = AsyncHelper.RunSync(() => _httpClient.GetAsync(route));
            var content = AsyncHelper.RunSync(() => message.Content.ReadAsStringAsync());

            _logWriter.WriteHttpResponseToConsole(message);

            var apiResult = new ApiResult(message, content);
            var response = new Response(apiResult, _badRequestProvider);
            
            PublishApiResponse(response);
            return response;
        }

        public IResponse Delete(string route)
        {
            _logWriter.WriteStringToConsole($"DELETE {route}");

            var message = AsyncHelper.RunSync(() => _httpClient.DeleteAsync(route));

            _logWriter.WriteHttpResponseToConsole(message);

            var content = AsyncHelper.RunSync(() => message.Content.ReadAsStringAsync());

            var apiResult = new ApiResult(message, content);
            var response = new Response(apiResult, _badRequestProvider);
            
            PublishApiResponse(response);
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

        private IResponse PostOrPut<TModel>(string route, TModel model,
            Func<HttpClient, StringContent, Task<HttpResponseMessage>> callHttpClient)
        {
            var messageContent = CreateMessageContent(model);
            var responseMessage = AsyncHelper.RunSync(() => callHttpClient(_httpClient, messageContent));
           
            _logWriter.WriteStringToConsole($"REQUEST: {responseMessage.RequestMessage.Method.Method} {route}");
            _logWriter.WriteObjectToConsole(model);

            var responseString = AsyncHelper.RunSync(() => responseMessage.Content.ReadAsStringAsync());

            _logWriter.WriteHttpResponseToConsole(responseMessage);

            var apiResult = new ApiResult(responseMessage, responseString);
            var response = new Response(apiResult, _badRequestProvider);
            PublishApiResponse(response);
            return response;
        }

        public IDisposable Subscribe(IObserver<Response> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber(_observers, observer);
        }

        public void PublishApiResponse(Response? apiResponse)
        {
            foreach (var observer in _observers)
            {
                if (apiResponse == null)
                {
                    //observer.OnError(new LocationUnknownException());
                }
                else
                {
                    observer.OnNext(apiResponse);
                }
            }
        }
        
        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<Response>> _observers;
            private readonly IObserver<Response> _observer;

            public Unsubscriber(List<IObserver<Response>> observers, IObserver<Response> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
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