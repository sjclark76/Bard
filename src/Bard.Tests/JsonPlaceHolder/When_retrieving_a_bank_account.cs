using System;
using System.Net.Http;
using Bard.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace Bard.Tests.JsonPlaceHolder
{
    // ReSharper disable once InconsistentNaming
    public class JsonPlaceHolderTests : IDisposable
    {
        private readonly HttpClient _httpClient;

        public JsonPlaceHolderTests(ITestOutputHelper output)
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
        public void For_a_customer_that_does_not_exist()
        {
            When
                .Get("https://jsonplaceholder.typicode.com/posts/1");

            Then.Response
                .ShouldBe
                .Ok();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}