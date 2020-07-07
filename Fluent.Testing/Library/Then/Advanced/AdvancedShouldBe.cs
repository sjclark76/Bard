using Fluent.Testing.Library.When;

namespace Fluent.Testing.Library.Then.Advanced
{
    public class AdvancedShouldBe : ShouldBeBase, IShouldBe
    {
        public AdvancedShouldBe(ApiResult apiResult, IBadRequestProvider badRequestProvider) : base(apiResult)
        {
            badRequestProvider.StringContent = apiResult.ResponseString;
            BadRequest = new BadRequestProviderDecorator(this, badRequestProvider);
        }

        public IBadRequestProvider BadRequest { get; }
    }
}  