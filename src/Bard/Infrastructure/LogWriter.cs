using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Bard.Infrastructure
{
    /// <summary>
    ///     Helper class for logging messages
    /// </summary>
    public class LogWriter
    {
        private readonly Action<string> _logMessage;
        private readonly EventAggregator _eventAggregator;
      
        internal LogWriter(Action<string> logMessage, EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _logMessage = logMessage;
        }

        /// <summary>
        ///     Logs the message output.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogMessage(string message)
        {
            _logMessage(message);
            _eventAggregator.PublishMessageLogged(new MessageLogged(message));
        }

        /// <summary>
        ///     Takes an object serializes it to JSON and then logs the output
        /// </summary>
        /// <param name="obj">the object to log</param>
        public void LogObject(object? obj)
        {
            LogMessage(JsonConvert.SerializeObject(
                obj,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                }));
        }

        internal void WriteHttpResponseToConsole(HttpResponseMessage httpResponse)
        {
            var content = AsyncHelper.RunSync(() => httpResponse.Content.ReadAsStringAsync());
            LogMessage(
                $"RESPONSE: Http Status Code:  {httpResponse.StatusCode.ToString()} ({(int) httpResponse.StatusCode})");
            if (httpResponse.Headers.Contains("Location"))
                LogMessage($"Header::Location {httpResponse.Headers.Location.OriginalString}");

            if (string.IsNullOrEmpty(content)) return;

            try
            {
                var jsonFormatted = JToken.Parse(content).ToString(Formatting.Indented);
                LogMessage(jsonFormatted);
            }
            catch (Exception)
            {
                LogMessage(content);
            }
        }

        internal void WriteHttpRequestToConsole(HttpRequestMessage request)
        {
            LogMessage($"REQUEST: {request.Method.Method} {request.RequestUri}");

            if (request.Content != null)
            {
                var content = AsyncHelper.RunSync(() => request.Content.ReadAsStringAsync());

                try
                {
                    var jsonFormatted = JToken.Parse(content).ToString(Formatting.Indented);
                    LogMessage(jsonFormatted);
                }
                catch (JsonReaderException)
                {
                    LogObject(request);
                    LogMessage(content);
                }
            }
        }
    }
}