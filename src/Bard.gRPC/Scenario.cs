using Bard.gRPC.Internal;
using Bard.Infrastructure;
using Bard.Internal;
using Bard.Internal.Exception;
using Bard.Internal.Given;
using Bard.Internal.Then;
using Bard.Internal.When;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;

namespace Bard.gRPC
{
    internal class Scenario<TGrpcClient> : IScenario<TGrpcClient> where TGrpcClient : ClientBase<TGrpcClient>

    {
        private readonly Then _then;

        internal Scenario(GrpcScenarioOptions<TGrpcClient> options)
        {
            if (options.Client == null)
                throw new BardConfigurationException("Client not set");

            var logWriter = new LogWriter(options.LogMessage);

            var originalClient = options.Client;
            var eventAggregator = new EventAggregator();
            
            var bardClient = HttpClientBuilder
                .GenerateBardClient(originalClient, logWriter, options.BadRequestProvider, eventAggregator);

            GrpcChannelOptions channelOptions = new GrpcChannelOptions
            {
                HttpClient = bardClient
            };

            var channel = GrpcChannel.ForAddress(bardClient.BaseAddress, channelOptions);
            
            TGrpcClient GRpcFactory()
            {
                if (options.GrpcClient == null)
                    throw new BardConfigurationException($"{nameof(options.GrpcClient)} has not been configured.");
                
                return options.GrpcClient.Invoke(channel.Intercept(new BardClientInterceptor(logWriter)));
            }

            var api = new Api(bardClient, options.BadRequestProvider, eventAggregator);
            var pipeline = new PipelineBuilder(logWriter);

            Context = new GrpcScenarioContext<TGrpcClient>(pipeline, api, logWriter,
                options.Services, GRpcFactory);

            var when = new When<TGrpcClient>(GRpcFactory, eventAggregator, api, logWriter,
                () => Context.ExecutePipeline());

            When = when;

            _then = new Then();

            eventAggregator.Subscribe(_then);
            eventAggregator.Subscribe(pipeline);
        }

        protected GrpcScenarioContext<TGrpcClient> Context { get; set; }

        public IWhen<TGrpcClient> When { get; set; }

        public IThen Then => _then;
    }

    internal class Scenario<TGrpcClient, TStoryBook, TStoryData> : Scenario<TGrpcClient>,
        IScenario<TGrpcClient, TStoryBook, TStoryData>
        where TGrpcClient : ClientBase<TGrpcClient>
        where TStoryBook : StoryBook<TStoryData>, new()
        where TStoryData : class, new()
    {
        internal Scenario(GrpcScenarioOptions<TGrpcClient, TStoryBook> options) : base(options)
        {
            var story = options.Story;

            story.Context = new ScenarioContext<TStoryData>(Context);

            Given = new Given<TStoryBook, TStoryData>(story, () => Context.ExecutePipeline());
        }

        public IGiven<TStoryBook, TStoryData> Given { get; }
    }
}