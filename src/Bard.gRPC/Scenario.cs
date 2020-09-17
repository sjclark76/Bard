using System.Net.Http;
using Bard.gRPC.Internal;
using Bard.Infrastructure;
using Bard.Internal;
using Bard.Internal.Exception;
using Bard.Internal.Then;
using Bard.Internal.When;
using Grpc.Core;
using When = Bard.gRPC.Internal.When;

namespace Bard.gRPC
{
    internal class Scenario : IScenario

    {
        private readonly Then _then;

        internal Scenario(GrpcScenarioOptions options)
        {
            if (options.Client == null)
                throw new BardConfigurationException("Client not set");

            var eventAggregator = new EventAggregator();
            
            var logWriter = new LogWriter(options.LogMessage, eventAggregator);

            var originalClient = options.Client;

            var bardClient = HttpClientBuilder
                .CreateFullLoggingClient(originalClient, logWriter, options.BadRequestProvider, eventAggregator);

            foreach (var grpc in options.GrpcClients)
            {
                
            }
            
            GrpcClientFactory clientFactory = new GrpcClientFactory(bardClient, logWriter);
           
            var api = new Api(bardClient);
           
            var pipeline = new PipelineBuilder(logWriter);

            Context = new GrpcScenarioContext(pipeline, api, logWriter,
                options.Services, clientFactory);

            var when = new When(clientFactory, eventAggregator, api, logWriter,
                () => Context.ExecutePipeline());

            When = when;

            _then = new Then(null, logWriter);

            eventAggregator.Subscribe(_then);
            eventAggregator.Subscribe(pipeline);
        }

        protected GrpcScenarioContext Context { get; set; }

        public IWhen When { get; }

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