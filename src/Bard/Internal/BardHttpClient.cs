using System.Net.Http;
using Bard.Infrastructure;
using Bard.Internal.Then;
using Bard.Internal.When;

namespace Bard.Internal
{
    internal class BardHttpClient : HttpClient
    {
        internal BardHttpClient(EventAggregator eventAggregator, BardResponsePublisher messageHandler,
            IBadRequestProvider badRequestProvider, LogWriter logWriter) : base(
            messageHandler)
        {
            messageHandler.PublishApiResult = NotifyObservers;

            EventAggregator = eventAggregator;
            RequestProvider = badRequestProvider;
            Writer = logWriter;
        }

        internal IBadRequestProvider RequestProvider { get; }
        internal LogWriter Writer { get; }
        internal EventAggregator EventAggregator { get; }

        private void NotifyObservers(ApiResult apiResult)
        {
            EventAggregator.PublishResponse(new Response(EventAggregator, apiResult, RequestProvider, Writer));
        }
    }
}