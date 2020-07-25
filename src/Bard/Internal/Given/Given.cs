using System;

namespace Bard.Internal.Given
{
    internal class Given<TScenario> : IGiven<TScenario> where TScenario : StoryBook
    {
        private readonly Action _resetPipeline;
        private readonly TScenario _that;

        internal Given(TScenario scenario, Action resetPipeline)
        {
            _that = scenario;
            _resetPipeline = resetPipeline;
        }

        public TScenario That
        {
            get
            {
                _resetPipeline();
                return _that;
            }
        }
    }
}