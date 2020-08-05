using Bard.Infrastructure;
using Bard.Internal;
using Bard.Internal.Exception;
using Bard.Internal.Given;
using Bard.Internal.Then;
using Bard.Internal.When;
using Grpc.Core;
using Grpc.Net.Client;

namespace Bard.gRPC
{
    /// <summary>
    ///     TODO: Public for now..
    /// </summary>
    /// <typeparam name="TGrpcClient"></typeparam>
    internal class Scenario<TGrpcClient> where TGrpcClient : ClientBase<TGrpcClient>
    {
        private readonly Then _then;

        protected Scenario(GrpcScenarioOptions<TGrpcClient> options)
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

            TGrpcClient GRpcFactory()
            {
                return options.GrpcClient.Invoke(channel);
            }

            var api = new Api(bardClient, options.BadRequestProvider);
            var pipeline = new PipelineBuilder(logWriter);

            Context = new GrpcScenarioContext<TGrpcClient>(pipeline, api, logWriter,
                options.Services, GRpcFactory);

            var when = new GrpcWhen<TGrpcClient>(GRpcFactory, api, logWriter,
                () => Context.ExecutePipeline());

            When = when;

            _then = new Then();

            _then.Subscribe(bardClient);
            pipeline.Subscribe(bardClient);
        }

        public IGrpcWhen<TGrpcClient> When { get; set; }

        public IThen Then => _then;

        protected GrpcScenarioContext<TGrpcClient> Context { get; set; }
    }

    internal class Scenario<TGrpcClient, TStoryBook, TStoryData> : Scenario<TGrpcClient>, IScenario<TGrpcClient, TStoryBook, TStoryData> where TGrpcClient : ClientBase<TGrpcClient>
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