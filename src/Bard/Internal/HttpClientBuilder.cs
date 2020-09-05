using System.Net.Http;
using System.Reflection;
using Bard.Infrastructure;
using Bard.Internal.When;

namespace Bard.Internal
{
    internal class HttpClientBuilder
    {
        private static HttpMessageHandler? GetInstanceField(object instance)
        {
            const BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                                           | BindingFlags.Static;

            var field = typeof(HttpMessageInvoker).GetField("_handler", bindFlags);

            return field?.GetValue(instance) as HttpMessageHandler;
        }

        internal static BardHttpClient CreateRequestLoggingClient(HttpClient client, LogWriter logWriter,
            IBadRequestProvider badRequestProvider, EventAggregator eventAggregator)
        {
            var httpMessageHandler = GetInstanceField(client);
            
            if (httpMessageHandler == null)
                throw new BardException("Cannot find client handler");

            var grpcMessageHandler = new GrpcMessageHandler(httpMessageHandler);
            var requestLoggerMessageHandler = new RequestLoggerMessageHandler(logWriter, grpcMessageHandler);
            var bardResponsePublisher = new BardResponsePublisher(requestLoggerMessageHandler);

            var bardHttpClient = CloneHttpClient(client, logWriter, badRequestProvider, eventAggregator, bardResponsePublisher);

            return bardHttpClient;
        }

        private static BardHttpClient CloneHttpClient(HttpClient client, LogWriter logWriter,
            IBadRequestProvider badRequestProvider, EventAggregator eventAggregator,
            BardResponsePublisher bardResponsePublisher)
        {
            var bardHttpClient = new BardHttpClient(eventAggregator, bardResponsePublisher, badRequestProvider, logWriter)
            {
                BaseAddress = client.BaseAddress,
                Timeout = client.Timeout,
                MaxResponseContentBufferSize = client.MaxResponseContentBufferSize,
                DefaultRequestVersion = client.DefaultRequestVersion
            };

            foreach (var (key, value) in client.DefaultRequestHeaders)
            {
                bardHttpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, value);
            }
            
            return bardHttpClient;
        }

        internal static BardHttpClient CreateFullLoggingClient(HttpClient client, LogWriter logWriter,
            IBadRequestProvider badRequestProvider, EventAggregator eventAggregator)
        {
            var httpMessageHandler = GetInstanceField(client);
            
            if (httpMessageHandler == null)
                throw new BardException("Cannot find client handler");

            var grpcMessageHandler = new GrpcMessageHandler(httpMessageHandler);
            var responseLoggerMessageHandler = new ResponseLoggerMessageHandler(logWriter, grpcMessageHandler);
            var requestLoggerMessageHandler = new RequestLoggerMessageHandler(logWriter, responseLoggerMessageHandler);
            var bardResponsePublisher = new BardResponsePublisher(requestLoggerMessageHandler);

            var bardHttpClient = CloneHttpClient(client, logWriter, badRequestProvider, eventAggregator, bardResponsePublisher);

            return bardHttpClient;
        }
        
        internal static HttpClient CreateGrpcClient(HttpClient client)
        {
            var httpMessageHandler = GetInstanceField(client);
            
            if (httpMessageHandler == null)
                throw new BardException("Cannot find client handler");

            var grpcMessageHandler = new GrpcMessageHandler(httpMessageHandler);

            var gRpcHttpClient = new HttpClient(grpcMessageHandler)
            {
                BaseAddress = client.BaseAddress,
                Timeout = client.Timeout,
                MaxResponseContentBufferSize = client.MaxResponseContentBufferSize
            };

            return gRpcHttpClient;
        }
    }
}