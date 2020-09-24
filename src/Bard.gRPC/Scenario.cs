using System.Linq;
using Bard.gRPC.Internal;
using Bard.Infrastructure;
using Bard.Internal;
using Bard.Internal.Exception;
using Bard.Internal.Then;
using Bard.Internal.When;
using Grpc.Core;

namespace Bard.gRPC
{
    internal class Scenario : IScenario

    {
        private readonly Api _api;
        private readonly GrpcClientFactory _clientFactory;
        private readonly EventAggregator _eventAggregator;
        private readonly LogWriter _logWriter;
        private readonly Then _then;
        private readonly IWhen _when;

        internal Scenario(GrpcScenarioOptions options)
        {
            if (options.GrpcClients.Any() == false && options.Client == null)
                throw new BardConfigurationException("Client not set");

            _eventAggregator = new EventAggregator();

            _logWriter = new LogWriter(options.LogMessage, _eventAggregator);

            var originalClient = options.Client ?? throw new BardConfigurationException("client not set.");

            var bardClient = HttpClientBuilder
                .CreateFullLoggingClient(originalClient, _logWriter, options.BadRequestProvider, _eventAggregator);

            _clientFactory = new GrpcClientFactory(options.GrpcClients, bardClient, _logWriter);

            _api = new Api(bardClient);

            var pipeline = new PipelineBuilder(_logWriter);

            Context = new GrpcScenarioContext(pipeline, _api, _logWriter,
                options.Services, _clientFactory);

            _when = new When(_api, _eventAggregator, _logWriter);

            _then = new Then(null, _logWriter);

            _eventAggregator.Subscribe(_then);
            _eventAggregator.Subscribe(pipeline);
        }

        protected GrpcScenarioContext Context { get; }

        IWhen IScenario.When => _when;

        public IGrpc<TGrpcClient> Grpc<TGrpcClient>() where TGrpcClient : ClientBase<TGrpcClient>
        {
            var grpcWhen = new GrpcWhen<TGrpcClient>(_clientFactory, _eventAggregator, _api, _logWriter,
                () => Context.ExecutePipeline());

            return grpcWhen;
        }

        public IThen Then => _then;
    }

    internal class Scenario<TStoryBook, TStoryData> : Scenario,
        IScenario<TStoryBook, TStoryData>
        where TStoryBook : StoryBook<TStoryData>, new()
        where TStoryData : class, new()
    {
        private readonly TStoryBook _given;

        internal Scenario(GrpcScenarioOptions<TStoryBook> options) : base(options)
        {
            var story = options.Story;

            story.Context = new ScenarioContext<TStoryData>(Context);

            _given = story;
        }

        public TStoryBook Given
        {
            get
            {
                Context.ExecutePipeline();
                return _given;
            }
        }
    }
}