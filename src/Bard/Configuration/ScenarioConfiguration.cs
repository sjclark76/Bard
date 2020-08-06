using System;
using Bard.Internal;

namespace Bard.Configuration
{
    /// <summary>
    ///     Configuration helper to configure your Scenario
    /// </summary>
    public class ScenarioConfiguration
    {
        /// <summary>
        ///     Configure a basic Scenario, without a story book.
        /// </summary>
        /// <remarks>Does not have a Story Book</remarks>
        /// <param name="configure">An action to configure the ScenarioOptions</param>
        /// <returns>A basic IScenario</returns>
        public static IScenario Configure(Action<ScenarioOptions> configure)
        {
            var options = new ScenarioOptions();

            configure(options);

            return new Scenario(options);
        }

        /// <summary>
        ///     Indicates to the configuration builder which StoryBook to use for the Scenario
        /// </summary>
        /// <typeparam name="TStoryBook">The Story Book</typeparam>
        /// <typeparam name="TStoryData">The Story Data</typeparam>
        /// <returns></returns>
        public static StoryBookOptions<TStoryBook, TStoryData> WithStoryBook<TStoryBook, TStoryData>()
            where TStoryBook : StoryBook<TStoryData>, new()
            where TStoryData : class, new()
        {
            return new StoryBookOptions<TStoryBook, TStoryData>();
        }
    }

    /// <summary>
    /// StoryBookOptions
    /// </summary>
    /// <typeparam name="TStoryBook">StoryBook</typeparam>
    /// <typeparam name="TStoryData">StoryData</typeparam>
    public class StoryBookOptions<TStoryBook, TStoryData> where TStoryBook : StoryBook<TStoryData>, new()
        where TStoryData : class, new()
    {
        /// <summary>
        ///     Configure an advancedScenario, with a story book.
        /// </summary>
        /// <param name="configure">An action to configure the ScenarioOptions</param>
        /// <returns>An advanced IScenario</returns>
        public IScenario<TStoryBook, TStoryData>
            Configure(Action<ScenarioOptions<TStoryBook, TStoryData>> configure)

        {
            var options = new ScenarioOptions<TStoryBook, TStoryData>();

            configure(options);

            return new Scenario<TStoryBook, TStoryData>(options);
        }
    }
}