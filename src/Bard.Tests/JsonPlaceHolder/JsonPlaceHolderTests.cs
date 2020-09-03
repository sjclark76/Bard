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
        private const string URL = "https://jsonplaceholder.typicode.com/posts/1";

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

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        private readonly HttpClient _httpClient;

        public IThen Then { get; set; }

        public IWhen When { get; set; }

        [Fact]
        public void For_a_customer_that_does_not_exist()
        {
            When
                .Get(URL);

            Then.Response
                .ShouldBe
                .Ok();
        }

        [Fact]
        public void Should_included_content_type_header()
        {
            When.Get(URL);

            Then.Response.Headers.ShouldInclude("Content-Type");
            
            // Alternative way
            Then.Response.Headers.Should.Include.ContentType();
        }

        [Fact]
        public void Should_included_content_type_header_with_correct_value()
        {
            When.Get(URL);

            Then.Response.Headers.ShouldInclude("Content-Type", "application/json; charset=utf-8");
            
            // Alternative way
            Then.Response.Headers.Should.Include.ContentType("application/json; charset=utf-8");
        }
        
        [Fact]
        public void Should_included_content_length_header()
        {
            When.Get(URL);

            // Chained..
            Then.Response.Headers
                .ShouldInclude("Content-Length")
                .Should.Include.ContentLength();
        }

        [Fact]
        public void Should_included_content_length_header_with_correct_value()
        {
            When.Get(URL);

            Then.Response.Headers.ShouldInclude("Content-Length", "292");
            Then.Response.Headers.Should.Include.ContentLength("292");
        }
    }
}