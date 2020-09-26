using Xunit;
using Xunit.Abstractions;

namespace Bard.Tests.GET
{
    // ReSharper disable once InconsistentNaming
    public class When_retrieving_an_image : BankingTestBase
    {
        public When_retrieving_an_image(ITestOutputHelper output) : base(output)
        {
        }

        [Fact(Skip = "Just for now.")]
        public void Then_the_response_should_be_ok()
        {
            When
                .Get("api/misc/image");

            Then.Response
                .ShouldBe
                .Ok();
        }

        [Fact(Skip = "Just for now.")]
        public void When_calling_the_slow_running_endpoint_a_bard_exception_should_be_thrown()
        {
            When
                .Get("api/misc/slow");

            Assert.Throws<BardException>(() =>
            {
                Then.Response
                    .Time
                    .LessThan(2000);
            });
        }
    }
}