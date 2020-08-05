using System;
using Bard.Internal;

namespace Bard.Configuration
{
    public class ScenarioConfiguration
    {
        public static IScenario Configure(Action<ScenarioOptions> configure)
        {
            var options = new ScenarioOptions();

            configure(options);

            return new Scenario(options);
        }

        public static IScenario<TStoryBook, TStoryData> Configure<TStoryBook, TStoryData>(Action<ScenarioOptions<TStoryBook, TStoryData>> configure) where TStoryBook : StoryBook<TStoryData>, new() where TStoryData : class, new()
        {
            var options = new ScenarioOptions<TStoryBook, TStoryData>();

            configure(options);

            return new Scenario<TStoryBook, TStoryData>(options);
        }
    }
}