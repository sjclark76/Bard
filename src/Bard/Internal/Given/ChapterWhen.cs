using System;
using System.Runtime.CompilerServices;

namespace Bard.Internal.Given
{
    internal class ChapterWhen<TStoryData> : IChapterWhen<TStoryData>
        where TStoryData : class, new()
    {
        private readonly ScenarioContext _context;
        private readonly Func<ScenarioContext<TStoryData>, TStoryData> _execute;

        internal ChapterWhen(ScenarioContext<TStoryData> context, Func<ScenarioContext<TStoryData>, TStoryData> execute)
        {
            _context = context;
            _execute = execute;
        }

        public TNextStep Then<TNextStep>([CallerMemberName] string memberName = "")
            where TNextStep : Chapter<TStoryData>, new()
        {
            var nextContext = new ScenarioContext<TStoryData>(_context);

            _context.AddPipelineStep(memberName, input =>
            {
                nextContext.SetStoryData(input as TStoryData);
                return _execute(nextContext);
            });

            //TODO: maybe just pass ScenarioContext
            var nextStep = new TNextStep {Context = new ScenarioContext<TStoryData>(_context)};

            return nextStep;
        }

        public EndChapter<TStoryData> End(string memberName = "")
        {
            var nextContext = new ScenarioContext<TStoryData>(_context);

            _context.AddPipelineStep(memberName, input =>
            {
                nextContext.SetStoryData(input as TStoryData);
                return _execute(nextContext);
            });

            var nextStep = new EndChapter<TStoryData> {Context = new ScenarioContext<TStoryData>(_context)};

            return nextStep;
        }
    }
}