using System;
using System.Net.Http;

namespace Bard.Internal.When
{
    public class ApiRequest
    {
        private readonly HttpRequestMessage? _request;
        public string? Route { get; }
        public StringContent? MessageContent { get; }

        public ApiRequest(string route, StringContent messageContent)
        {
            MessageContent = messageContent;
        }

        public ApiRequest(string route)
        {
            Route = route;
        }

        public ApiRequest(HttpRequestMessage request)
        {
            _request = request;
        }

        public string Hash()
        {
            if (_request != null)
            {
                _request.Content.GetMD5Hash()
            }
        }
    }
}