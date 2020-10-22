using System;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using Bard.Internal.When;
using System.Text.Json;
using Bard.Internal;

namespace Bard.Infrastructure
{
    /// <summary>
    ///     Helper class for logging messages
    /// </summary>
    public class LogWriter
    {
        private const int TotalLength = 100;
        private readonly LogBuffer _logBuffer;

        private readonly EventAggregator _eventAggregator;

        internal LogWriter(LogBuffer logBuffer, EventAggregator eventAggregator)
        {
            _logBuffer = logBuffer;
            _eventAggregator = eventAggregator;
        }

        /// <summary>
        ///     Logs the message output.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogMessage(string message)
        {
            _logBuffer.Push(message);
            _eventAggregator.PublishMessageLogged(new MessageLogged(message));
        }

        /// <summary>
        ///     Takes an object serializes it to JSON and then logs the output
        /// </summary>
        /// <param name="obj">the object to log</param>
        public void LogObject(object? obj)
        {
            LogMessage(
                JsonSerializer.Serialize(obj, new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                })
            );
        }

        internal void WriteHttpResponseToConsole(ApiResult result)
        {
            WriteHttpResponseToConsole(result.ResponseMessage, result.ElapsedTime);
        }

        internal void WriteHttpResponseToConsole(HttpResponseMessage httpResponse, TimeSpan? elapsedTime = null)
        {
            var content = AsyncHelper.RunSync(() => httpResponse.Content.ReadAsStringAsync());
            LogMessage($"Http Status Code:  {httpResponse.StatusCode.ToString()} ({(int) httpResponse.StatusCode})");

            if (elapsedTime != null)
                LogMessage($"Elapsed Time: {Math.Round(elapsedTime.Value.TotalMilliseconds)} (milliseconds)");

            foreach (var (key, value) in httpResponse.Content.Headers)
                LogMessage($"{key}:{string.Join(' ', value)}");

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
                    try
                    {
                        LogObject(JsonDocument.Parse(content).RootElement);
                    }
                    catch (Exception)
                    {
                        LogMessage(content);
                    }
                else if (plainText.Contains(mediaType)) LogMessage(content);
            }
        }

        internal void WriteHttpRequestToConsole(HttpRequestMessage request)
        {
            LogMessage($"REQUEST: {request.Method.Method} {request.RequestUri}");

            foreach (var header in request.Headers)
            foreach (var value in header.Value)
                LogMessage($"Header::{header.Key} {value}");

            if (request.Content != null)
            {
                var content = AsyncHelper.RunSync(() => request.Content.ReadAsStringAsync());

                try
                {
                    LogObject(JsonDocument.Parse(content).RootElement);
                }
                catch (JsonException)
                {
                    LogObject(request);
                    LogMessage(content);
                }
            }

            LogMessage(string.Empty);
        }

        internal void LogHeaderMessage(string message)
        {
            var totalLength = TotalLength;
            var messageLength = message.Length;

            if (messageLength > TotalLength) totalLength = messageLength + 2;

            var envelopeLength = totalLength - 2;

            var messageBuilder = new StringBuilder();
            if (messageLength >= envelopeLength) return;

            decimal whiteSpaceLength = envelopeLength - messageLength;

            var halfWhiteSpace = whiteSpaceLength == 0 ? 0 : whiteSpaceLength / 2;

            var pre = decimal.ToInt32(Math.Floor(halfWhiteSpace));
            var post = decimal.ToInt32(Math.Ceiling(halfWhiteSpace));

            // 27 / 2 = 13.5 13 + 14

            LogLineBreak(totalLength);

            messageBuilder.Append("*");
            messageBuilder.Append((char) 32, pre);
            messageBuilder.Append(message);
            messageBuilder.Append((char) 32, post);
            messageBuilder.Append("*");

            _logBuffer.Push(messageBuilder.ToString());
            LogLineBreak(totalLength);
            BlankLine();
        }

        internal void LogLineBreak(int totalLength = TotalLength)
        {
            var astrixLine = new string('*', totalLength);
            _logBuffer.Push(astrixLine);
        }

        internal void BlankLine()
        {
            _logBuffer.Push(string.Empty);
        }
    }
}