using System;
using Bard.Configuration;
using Bard.gRPC;
using Bard.Infrastructure;
using Bard.Internal.Exception;
using Bard.Internal.When;
using Grpc.Core;
using Grpc.Net.Client;

namespace Bard.Internal
{
    public static class Foo
    {
        public static void Grpc<TGrpcClient>(this ScenarioContext context, Func<TGrpcClient, object?> execute) where TGrpcClient : ClientBase<TGrpcClient>
        {
            GrpcChannelOptions channelOptions = new GrpcChannelOptions
            {
                HttpClient = context.BardHttpClient
            };

            var channel = GrpcChannel.ForAddress(context.BardHttpClient.BaseAddress, channelOptions);

            var gRpcClient = (TGrpcClient) Activator.CreateInstance(typeof(TGrpcClient), channel);

            execute(gRpcClient);
        }  
    }
    
    
    /// <summary>
    ///     TODO: Public for now..
    /// </summary>
    /// <typeparam name="TGrpcClient"></typeparam>
    public class GrpcFluentScenario<TGrpcClient> where TGrpcClient : ClientBase<TGrpcClient>
    {
        private readonly Then.Then _then;

        public GrpcFluentScenario(GrpcScenarioOptions<TGrpcClient> options)
        {
            if (options.Client == null)
                throw new BardConfigurationException("Client not set");

            var logWriter = new LogWriter(options.LogMessage);

            var originalClient = options.Client;
            var bardClient = HttpClientBuilder
                .GenerateBardClient(originalClient, logWriter, options.BadRequestProvider);

            GrpcChannelOptions channelOptions = new GrpcChannelOptions
            {
                HttpClient = bardClient
            };
            
            var channel = GrpcChannel.ForAddress(bardClient.BaseAddress, channelOptions);

            if (options.GrpcClient == null)
                throw new BardConfigurationException($"{nameof(options.GrpcClient)} has not been configured.");

            var grpcClient = options.GrpcClient.Invoke(channel);

            var api = new Api(bardClient, options.BadRequestProvider);
            var pipeline = new PipelineBuilder(logWriter);

            Context = new GrpcScenarioContext<TGrpcClient>(grpcClient, pipeline, bardClient, api, logWriter, options.Services);

            var when = new GrpcWhen<TGrpcClient>(grpcClient, api, logWriter,
                () => Context.ExecutePipeline());

            When = when;

            _then = new Then.Then();

            _then.Subscribe(bardClient);
            pipeline.Subscribe(bardClient);
        }

        public IGrpcWhen<TGrpcClient> When { get; set; }

        public IThen Then => _then;
        public GrpcScenarioContext<TGrpcClient> Context { get; set; }
    }

    public class GrpcFluentScenario<TGrpcClient, TStoryBook> : GrpcFluentScenario<TGrpcClient> where TGrpcClient : ClientBase<TGrpcClient>
        where TStoryBook : StoryBook, new()
    {
        internal GrpcFluentScenario(GrpcScenarioOptions<TGrpcClient, TStoryBook> options) : base(options)
        {
            var story = options.Story;
            story.Context = Context;
    
            Given = new Given.Given<TStoryBook>(story, () => Context.ExecutePipeline());
        }
    
        public IGiven<TStoryBook> Given { get; }
    }
}