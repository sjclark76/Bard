using System;
using System.Net.Http;

namespace Bard.Configuration
{
    /// <summary>
    ///     ScenarioOptions supplies all the necessary configuration
    ///     necessary to customize and bootstrap a working
    ///     Scenario
    /// </summary>
    public class ScenarioOptions
    {
        internal ScenarioOptions()
        {
            LogMessage = Console.WriteLine;
            BadRequestProvider = new DefaultBadRequestProvider();
        }

        /// <summary>
        ///     The HttpClient that should be used to call the Test API
        /// </summary>
        /// <remarks>Required</remarks>
        public HttpClient? Client { get; set; }

        /// <summary>
        ///     An action that can be used by Bard to log messages to the Test runners output window.
        ///     <remarks>Recommended</remarks>
        /// </summary>
        public Action<string> LogMessage { get; set; }

        /// <summary>
        ///     Override the default BadRequestProvider with your own implementation.
        ///     <remarks>Optional</remarks>
        /// </summary>
        public IBadRequestProvider BadRequestProvider { get; set; }

        /// <summary>
        ///     Service Provider for Dependency Injection withing Bard
        ///     <remarks>Recommended</remarks>
        /// </summary>
        public IServiceProvider? Services { get; set; }
    }

    /// <summary>
    ///     Specialized ScenarioOptions supplies all the necessary configuration
    ///     necessary to customize and bootstrap a working
    ///     Scenario
    /// </summary>
    /// <typeparam name="TStoryBook">The StoryBook implementation to use.</typeparam>
    /// <typeparam name="TStoryData">The StoryData class that the Stories will use</typeparam>
    public class ScenarioOptions<TStoryBook, TStoryData> : ScenarioOptions
        where TStoryBook : StoryBook<TStoryData>, new() where TStoryData : class, new()
    {
        internal ScenarioOptions()
        {
            Story = new TStoryBook();
        }

        internal TStoryBook Story { get; }
    }
}