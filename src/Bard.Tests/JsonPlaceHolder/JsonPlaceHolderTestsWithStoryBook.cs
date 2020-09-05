using System;
using System.Net.Http;
using Bard.Configuration;
using Xunit.Abstractions;

namespace Bard.Tests.JsonPlaceHolder
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class JsonPlaceHolderStoryData
    {
        public int PostId { get; set; }
    }

    public class JsonPlaceHolderStoryBook : StoryBook<JsonPlaceHolderStoryData>
    {
        public EndChapter<JsonPlaceHolderStoryData> A_post_has_been_created()
        {
            return When(context =>
                {
                    var response = context.Api.Post("https://jsonplaceholder.typicode.com/posts",
                        new Post {Name = "Test Post"});

                    var myPost = response.Content<Post>();

                    context.StoryData.PostId = myPost.Id;
                })
                .End();
        }
    }

    // ReSharper disable once InconsistentNaming
    public class JsonPlaceHolderTestsWithStoryBook : IDisposable
    {
        private const string URL = "https://jsonplaceholder.typicode.com/posts/";

        private readonly HttpClient _httpClient;

        public JsonPlaceHolderTestsWithStoryBook(ITestOutputHelper output)
        {
            _httpClient = new HttpClient();
            var scenario = ScenarioConfiguration
                .WithStoryBook<JsonPlaceHolderStoryBook, JsonPlaceHolderStoryData>()
                .Configure(options =>
                {
                    options.Client = _httpClient;
                    options.LogMessage = output.WriteLine;
                });

            Given = scenario.Given;
            When = scenario.When;
            Then = scenario.Then;
        }

        public JsonPlaceHolderStoryBook Given { get; set; }

        public IThen Then { get; set; }

        public IWhen When { get; set; }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        // [Fact]
        // public void For_a_customer_that_does_not_exist()
        // {
        //     Given
        //         .A_post_has_been_created()
        //         .GetResult(out JsonPlaceHolderStoryData result);
        //     
        //     When
        //         .Get($"{URL}{result.PostId}");
        //
        //     Then.Response
        //         .ShouldBe
        //         .Ok<Post>()
        //         .Name.ShouldBe("Test Post");
        // }
    }
}