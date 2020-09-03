using System;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
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
            LogMessage($"Http Status Code:  {httpResponse.StatusCode.ToString()} ({(int) httpResponse.StatusCode})");

            foreach (var header in httpResponse.Content.Headers)
            {
                LogMessage($"{header.Key}:{string.Join(' ', header.Value)}");
            }

            if (httpResponse.Headers.Contains("Location"))
                LogMessage($"Header::Location {httpResponse.Headers.Location.OriginalString}");

            var plainText = new[]
            {
                MediaTypeNames.Application.Soap,
                MediaTypeNames.Application.Xml,
                MediaTypeNames.Text.Html,
                MediaTypeNames.Text.Plain,
                MediaTypeNames.Text.Xml,
                MediaTypeNames.Text.RichText
            };
            
            if (!string.IsNullOrEmpty(content))
            {
                var mediaType = httpResponse.Content.Headers.ContentType.MediaType;

                if (mediaType == MediaTypeNames.Application.Json || mediaType == "application/problem+json")
                {
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
                else if (plainText.Contains(mediaType))
                {
                    LogMessage(content);
                }
            }
        }

        internal void WriteHttpRequestToConsole(HttpRequestMessage request)
        {
            LogMessage($"REQUEST: {request.Method.Method} {request.RequestUri}");

            foreach (var header in request.Headers)
            {
                foreach (var value in header.Value)
                {
                    LogMessage($"Header::{header.Key} {value}");
                }
            }
            
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
            
            LogMessage(string.Empty);
        }

        internal void LogHeaderMessage(string message)
        {
            var totalLength = 100;
            var messageLength = message.Length;
            
            if (messageLength > totalLength)
            {
                totalLength = messageLength + 2;
            }
            
            var envelopeLength = totalLength - 2;

            var messageBuilder = new StringBuilder();
            if (messageLength >= envelopeLength) return;
            
            decimal whiteSpaceLength = envelopeLength - messageLength;

            var halfWhiteSpace = whiteSpaceLength == 0 ? 0 : whiteSpaceLength / 2;

            var pre = decimal.ToInt32(Math.Floor(halfWhiteSpace));
            var post = decimal.ToInt32(Math.Ceiling(halfWhiteSpace));
                
            // 27 / 2 = 13.5 13 + 14
                
            var astrixLine = new string('*', totalLength);
            _logMessage(astrixLine);
            messageBuilder.Append("*");
            messageBuilder.Append((char) 32, pre);
            messageBuilder.Append(message);
            messageBuilder.Append((char) 32, post);
            messageBuilder.Append("*");

            _logMessage(messageBuilder.ToString());
            _logMessage(astrixLine);
            _logMessage(string.Empty);
        }
    }
}