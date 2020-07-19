using System;
using System.Net.Http;

namespace Bard.Configuration
{
    public class ScenarioOptions<TStoryBook> where TStoryBook : StoryBook, new()
    {
        public HttpClient? Client { get; private set; }
        public Action<string> LogMessage { get; private set; }
        public IBadRequestProvider BadRequestProvider { get; private set; }
        
        public TStoryBook Story { get; private set; }

        public ScenarioOptions()
        {
            Story = new TStoryBook();
            LogMessage = Console.WriteLine;
            BadRequestProvider = new DefaultBadRequestProvider();
        }

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
}