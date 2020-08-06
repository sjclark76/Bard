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

        public TNextChapter Then<TNextChapter>([CallerMemberName] string memberName = "")
            where TNextChapter : Chapter<TStoryData>, new()
        {
            _context.AddPipelineStep(memberName, () =>
                _execute(_context)
            );

            var nextStep = new TNextChapter {Context = _context};

            return nextStep;
        }

        public EndChapter<TStoryData> End(string memberName = "")
        {
            _context.AddPipelineStep(memberName, () => _execute(_context));

            var nextStep = new EndChapter<TStoryData> {Context = _context};

            return nextStep;
        }
    }
}