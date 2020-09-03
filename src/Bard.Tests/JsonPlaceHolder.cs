using System.Net.Http;
using Bard.Configuration;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Bard.Tests
{
    public class JsonPlaceHolder
    {
        private readonly HttpClient _httpClient;

        public JsonPlaceHolder(ITestOutputHelper output)
        {
            _httpClient = new HttpClient();

            var scenario = ScenarioConfiguration
                .Configure(options =>
                {
                    options.Client = _httpClient;
                    options.LogMessage = output.WriteLine;
                });

            When = scenario.When;
            Then = scenario.Then;
        }

        public IThen Then { get; set; }

        public IWhen When { get; set; }

        [Fact]
        public void Foo()
        {
            When.Get("https://jsonplaceholder.typicode.com/posts/1");

            Then.Response.WriteResponse();
            
            //Then.Response.Header.Should.Include.ContentType();
            Then.Response.Header.ContentType.ShouldBe("application/json; charset=utf-8");
            //Then.Response.Header.ShouldInclude("Date");
            Then.Response.Header.ETag.Tag.ShouldBe("\"124-yiKdLzqO5gfBrJFrcdJ8Yq0LGnU\"");
            
        }
    }
}