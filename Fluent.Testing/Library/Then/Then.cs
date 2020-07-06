using Fluent.Testing.Library.Infrastructure;

namespace Fluent.Testing.Library.Then.v1
{
    public class Then<TShouldBe> : IThen<TShouldBe> where TShouldBe : IShouldBeBase
    {
        private readonly LogWriter _logWriter;

        public Then(LogWriter logWriter)
        {
            _logWriter = logWriter;
            Response = new TheResponse<TShouldBe>();
        }
       
        public void SetTheResponse(IResponse<TShouldBe> response)
        {
            Response = response;
        }
        
        public IResponse<TShouldBe> Response { get; set; }
    }
}