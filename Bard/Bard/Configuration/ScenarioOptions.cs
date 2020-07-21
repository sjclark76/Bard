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

        public HttpClient? Client { get; private set; }
        public Action<string> LogMessage { get; private set; }
        public IBadRequestProvider BadRequestProvider { get; private set; }

        public void UseHttpClient(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public void Log(Action<string> logMessage)
        {
            LogMessage = logMessage;
        }

        public void Use<T>() where T : IBadRequestProvider, new()
        {
            BadRequestProvider = new T();
        }
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