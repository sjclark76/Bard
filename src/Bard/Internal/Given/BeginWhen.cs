using System;
using System.Runtime.CompilerServices;

namespace Bard.Internal.Given
{
    internal class BeginWhen<TStoryData> : IBeginWhen<TStoryData> where TStoryData : class, new()
    {
        private readonly ScenarioContext _context;
        private readonly Func<ScenarioContext, TStoryData> _execute;

        internal BeginWhen(ScenarioContext<TStoryData> context, Func<ScenarioContext, TStoryData> execute)
        {
            _context = context;
            _execute = execute;
        }

        public TNextStep Then<TNextStep>([CallerMemberName] string memberName = "")
            where TNextStep : Chapter<TStoryData>, new()
        {
            _context.AddPipelineStep(memberName, input => _execute(_context));

            var nextContext = new ScenarioContext<TStoryData>(_context);
            var nextStep = new TNextStep {Context = nextContext};

            return nextStep;
        }
        
        public EndChapter<TStoryData> End([CallerMemberName] string memberName = "")
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