using System.Net.Http;
using System.Reflection;
using Bard.Infrastructure;
using Bard.Internal.When;

namespace Bard.Internal
{
    internal class HttpClientBuilder
    {
        internal static HttpMessageHandler? GetInstanceField(object instance)
        {
            const BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                                           | BindingFlags.Static;
            
            var field = typeof(HttpMessageInvoker).GetField("_handler", bindFlags);
            
            return field?.GetValue(instance) as HttpMessageHandler;
        }
        
        internal static BardHttpClient GenerateBardClient(HttpClient client, LogWriter logWriter, IBadRequestProvider badRequestProvider)
        {
            var httpMessageHandler = GetInstanceField(client);

            var bardApiMessageHandler = new BardApiMessageHandler(logWriter) {InnerHandler = httpMessageHandler};

            var bardHttpClient = new BardHttpClient(bardApiMessageHandler, badRequestProvider)
            {
                BaseAddress = client.BaseAddress,
                Timeout = client.Timeout,
                MaxResponseContentBufferSize = client.MaxResponseContentBufferSize
            }; 

            return bardHttpClient;
        }
    }
}