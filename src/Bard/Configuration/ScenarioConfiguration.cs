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

        public static IScenario<TStoryBook> Configure<TStoryBook>(Action<ScenarioOptions<TStoryBook>> configure)
            where TStoryBook : StoryBook, new()
        {
            var options = new ScenarioOptions<TStoryBook>();

            configure(options);

            return new Scenario<TStoryBook>(options);
        }
    }
}