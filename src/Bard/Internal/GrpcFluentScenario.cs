using System;
using System.Net.Http;
using System.Reflection;
using Bard.Configuration;
using Bard.gRPC;
using Bard.Infrastructure;
using Bard.Internal.When;
using Grpc.Core;
using Grpc.Net.Client;

namespace Bard.Internal
{
    public class HttpClientBuilder
    {
        internal static HttpMessageHandler GetInstanceField(object instance)
        {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                                     | BindingFlags.Static;
            
            FieldInfo? field = (typeof(HttpMessageInvoker)).GetField("_handler", bindFlags);
            
            return (HttpMessageHandler) field?.GetValue(instance);
        }
        public static HttpClient GenerateBardClient(HttpClient client, LogWriter logWriter)
        {
            var httpMessageHandler = GetInstanceField(client);

            var bardApiMessageHandler = new BardApiMessageHandler(logWriter) {InnerHandler = httpMessageHandler};


            var bardHttpClient = new HttpClient(bardApiMessageHandler)
            {
                BaseAddress = client.BaseAddress,
                Timeout = client.Timeout,
                MaxResponseContentBufferSize = client.MaxResponseContentBufferSize
            }; 

            return bardHttpClient;

        }
    }
    /// <summary>
    /// TODO: Public for now..
    /// </summary>
    /// <typeparam name="TGrpcClient"></typeparam>
    public class GrpcFluentScenario<TGrpcClient> where TGrpcClient : ClientBase<TGrpcClient>
    {
        public GrpcFluentScenario(GrpcScenarioOptions<TGrpcClient> options)
        {
            if (options.Client == null)
                throw new Exception("Client not set");
        
            
            
            var logWriter = new LogWriter(options.LogMessage);
            
            var originalClient = options.Client;
            var bardClient = HttpClientBuilder.GenerateBardClient(originalClient, logWriter);
            
            GrpcChannelOptions channelOptions = new GrpcChannelOptions()
            {
                HttpClient = bardClient
            };
            var channel = GrpcChannel.ForAddress(bardClient.BaseAddress, channelOptions);

            var grpcClient = options.GrpcClient(channel);
            
            var api = new Api(bardClient, logWriter, options.BadRequestProvider);
            var pipeline = new PipelineBuilder(logWriter);

            Context = new GrpcScenarioContext<TGrpcClient>(grpcClient, pipeline, api, logWriter, options.Services);

            var when = new GrpcWhen<TGrpcClient>(grpcClient, api, logWriter, 
                () => Context.ExecutePipeline());
            
            When = when;
            
            // _then = new Then.Then();
            
            // _then.Subscribe(api);
            pipeline.Subscribe(api);
        }

        public IGrpcWhen<TGrpcClient> When { get; set; }

        public GrpcScenarioContext<TGrpcClient> Context { get; set; }
    }
}