using Fluent.Testing.Library.Infrastructure;
using Fluent.Testing.Library.Then.v1;

namespace Fluent.Testing.Library.Then
{
    public class Then<TShouldBe> : IThen<TShouldBe> where TShouldBe : ShouldBeBase, new()
    {
        private readonly LogWriter _logWriter;

        public Then(LogWriter logWriter)
        {
            _logWriter = logWriter;
            Response = new EmptyResponse<TShouldBe>();
        }
       
        public void SetTheResponse(IResponse<TShouldBe> response)
        {
            Response = response;
        }
        
         public IResponse<TShouldBe> Response { get; set; }
    }
}