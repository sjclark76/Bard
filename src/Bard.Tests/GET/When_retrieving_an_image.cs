using Xunit;
using Xunit.Abstractions;

namespace Bard.Tests.GET
{
    public class When_retrieving_an_image : BankingTestBase
    {
        public When_retrieving_an_image(ITestOutputHelper output) : base(output)
        {
        }


        [Fact]
        public void Then_the_response_should_be_ok()
        {
            When
                .Get("api/image");

            Then.Response
                .ShouldBe
                .Ok();
        }
    }
}