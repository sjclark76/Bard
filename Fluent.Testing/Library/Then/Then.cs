using Fluent.Testing.Library.Infrastructure;

namespace Fluent.Testing.Library.Then
{
    public class Then : IThen
    {
        private readonly LogWriter _logWriter;

        public Then(LogWriter logWriter)
        {
            _logWriter = logWriter;
            Response = new TheResponse();
        }

        public IResponse Response { get; private set; }

        public void SetTheResponse(IResponse response)
        {
            Response = response;
        }
    }

    public interface IThen
    {
        IResponse Response { get; }
    }
}