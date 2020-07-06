namespace Fluent.Testing.Library
{
    public class Then : IThen
    {
        private readonly LogWriter _logWriter;

        public Then(LogWriter logWriter)
        {
            _logWriter = logWriter;
            TheResponse = new Response();
        }

        public ITheResponse TheResponse { get; private set; }

        public void SetTheResponse(ITheResponse theResponse)
        {
            TheResponse = theResponse;
        }
    }

    public interface IThen
    {
        ITheResponse TheResponse { get; }
    }
}