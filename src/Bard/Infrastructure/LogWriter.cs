using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Bard.Infrastructure
{
    public class LogWriter
    {
        private readonly Action<string> _logMessage;

        public LogWriter(Action<string> logMessage)
        {
            _logMessage = logMessage;
        }

        public void WriteStringToConsole(string message)
        {
            _logMessage(message);
        }

        public void WriteObjectToConsole(object? obj)
        {
            _logMessage(JsonConvert.SerializeObject(
                obj,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                }));
        }

        public void WriteHttpResponseToConsole(HttpResponseMessage httpResponse)
        {
            var content = AsyncHelper.RunSync(() => httpResponse.Content.ReadAsStringAsync());
            _logMessage(
                $"RESPONSE: Http Status Code:  {httpResponse.StatusCode.ToString()} ({(int) httpResponse.StatusCode})");
            if (httpResponse.Headers.Contains("Location"))
                WriteStringToConsole($"Header::Location {httpResponse.Headers.Location.OriginalString}");

            if (string.IsNullOrEmpty(content)) return;

            try
            {
                var jsonFormatted = JToken.Parse(content).ToString(Formatting.Indented);
                _logMessage(jsonFormatted);
            }
            catch (Exception)
            {
                _logMessage(content);
            }
        }

        public void WriteHttpRequestToConsole(HttpRequestMessage request)
        {
            WriteStringToConsole($"REQUEST: {request.Method.Method} {request.RequestUri}");

            if (request.Content != null)
            {
                var content = AsyncHelper.RunSync(() => request.Content.ReadAsStringAsync());

                try
                {
                    var jsonFormatted = JToken.Parse(content).ToString(Formatting.Indented);
                    _logMessage(jsonFormatted);
                }
                catch (JsonReaderException e)
                {
                    WriteObjectToConsole(request);
                    _logMessage(content);
                }
            }
        }
    }
}