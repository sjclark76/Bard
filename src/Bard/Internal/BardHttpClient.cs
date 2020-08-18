using System.Net.Http;
using Bard.Infrastructure;
using Bard.Internal.Then;
using Bard.Internal.When;

namespace Bard.Internal
{
    internal class BardHttpClient : HttpClient 
    {
        private readonly IBadRequestProvider _badRequestProvider;
        private readonly LogWriter _logWriter;
        private readonly EventAggregator _eventAggregator;

        internal BardHttpClient(EventAggregator eventAggregator, BardResponsePublisher messageHandler,
            IBadRequestProvider badRequestProvider, LogWriter logWriter) : base(
            messageHandler)
        {
            messageHandler.PublishApiResult = NotifyObservers;

            _eventAggregator = eventAggregator;
            _badRequestProvider = badRequestProvider;
            _logWriter = logWriter;
        }

        private void NotifyObservers(ApiResult apiResult)
        {
            _eventAggregator.PublishResponse(new Response(_eventAggregator, apiResult, _badRequestProvider, _logWriter));
        }
    }
}