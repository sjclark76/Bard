using System.Net.Http;
using Bard.Internal.Then;
using Bard.Internal.When;

namespace Bard.Internal
{
    internal class BardHttpClient : HttpClient //, IObservable<Response>
    {
        private readonly IBadRequestProvider _badRequestProvider;
        private readonly EventAggregator _eventAggregator;

        internal BardHttpClient(EventAggregator eventAggregator, BardApiMessageHandler messageHandler,
            IBadRequestProvider badRequestProvider) : base(
            messageHandler)
        {
            messageHandler.PublishApiResult = NotifyObservers;

            _eventAggregator = eventAggregator;
            _badRequestProvider = badRequestProvider;
        }

        private void NotifyObservers(ApiResult apiResult)
        {
            _eventAggregator.PublishResponse(new Response(apiResult, _badRequestProvider));
        }
    }
}