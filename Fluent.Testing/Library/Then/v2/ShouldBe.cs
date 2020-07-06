namespace Fluent.Testing.Library.Then.v2
{
    public class ShouldBe : ShouldBeBase, IShouldBe 
    {
        public ShouldBe()
        {
            
        }

        public ShouldBe(IBadRequestResponse badRequest)
        {
            BadRequest = badRequest;
        }

        public IBadRequestResponse? BadRequest { get; }
    }
}