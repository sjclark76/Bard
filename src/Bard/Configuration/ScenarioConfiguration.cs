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
        ///     Configure an advancedScenario, with a story book.
        /// </summary>
        /// <param name="configure">An action to configure the ScenarioOptions</param>
        /// <returns>An advanced IScenario</returns>
        public static IScenario<TStoryBook, TStoryData>
            Configure<TStoryBook, TStoryData>(Action<ScenarioOptions<TStoryBook, TStoryData>> configure)
            where TStoryBook : StoryBook<TStoryData>, new() where TStoryData : class, new()
        {
            var options = new ScenarioOptions<TStoryBook, TStoryData>();

            configure(options);

            return new Scenario<TStoryBook, TStoryData>(options);
        }
    }
}