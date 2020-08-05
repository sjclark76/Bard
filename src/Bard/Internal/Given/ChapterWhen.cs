using System;
using System.Runtime.CompilerServices;

namespace Bard.Internal.Given
{
    internal class ChapterWhen<TStoryData> : IChapterWhen<TStoryData>
        where TStoryData : class, new()
    {
        private readonly ScenarioContext<TStoryData> _context;
        private readonly Action<ScenarioContext<TStoryData>> _execute;

        internal ChapterWhen(ScenarioContext<TStoryData> context, Action<ScenarioContext<TStoryData>> execute)
        {
            _context = context;
            _execute = execute;
        }

        public TNextStep Then<TNextStep>([CallerMemberName] string memberName = "")
            where TNextStep : Chapter<TStoryData>, new()
        {
            _context.AddPipelineStep(memberName, input =>
            {
                //_context.SetStoryData(input as TStoryData);
                _execute(_context);
            });

            //TODO: maybe just pass ScenarioContext
            var nextStep = new TNextStep {Context = _context};

            return nextStep;
        }

        public EndChapter<TStoryData> End(string memberName = "")
        {
            _context.AddPipelineStep(memberName, input => _execute(_context));

            var nextStep = new EndChapter<TStoryData> {Context = _context};

            return nextStep;
        }
    }
}