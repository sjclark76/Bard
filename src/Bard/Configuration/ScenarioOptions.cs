using System;
using System.Net.Http;

namespace Bard.Configuration
{
    public class ScenarioOptions
    {
        public ScenarioOptions()
        {
            LogMessage = Console.WriteLine;
            BadRequestProvider = new DefaultBadRequestProvider();
        }

        public HttpClient? Client { get; set; }
        public Action<string> LogMessage { get; set; }
        public IBadRequestProvider BadRequestProvider { get; set; }
        public IServiceProvider? Services { get; set; }
    }

    public class ScenarioOptions<TStoryBook> : ScenarioOptions where TStoryBook : StoryBook, new()
    {
        public ScenarioOptions()
        {
            Story = new TStoryBook();
        }

        public TStoryBook Story { get; }
    }
}