using System;

namespace Bard.Internal.Given
{
    internal class Given<TStoryBook, TStoryData> : IGiven<TStoryBook, TStoryData> where TStoryBook : StoryBook<TStoryData> where TStoryData : class, new()
    {
        private readonly Action _resetPipeline;
        private readonly TStoryBook _that;

        internal Given(TStoryBook scenario, Action resetPipeline)
        {
            _that = scenario;
            _resetPipeline = resetPipeline;
        }

        public TStoryBook That
        {
            get
            {
                _resetPipeline();
                return _that;
            }
        }
    }
}