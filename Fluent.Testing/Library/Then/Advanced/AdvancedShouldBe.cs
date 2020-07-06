using Fluent.Testing.Library.When;

namespace Fluent.Testing.Library.Then.Advanced
{
    public class AdvancedShouldBe : ShouldBeBase, IShouldBe
    {
        public AdvancedShouldBe(ApiResult apiResult, IBadRequestResponse badRequest) : base(apiResult)
        {
            BadRequest = new BadRequestResponseDecorator(this, badRequest);
        }

        public IBadRequestResponse BadRequest { get; }
    }
}