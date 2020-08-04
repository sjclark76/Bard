using System;
using System.Runtime.CompilerServices;

namespace Bard.Internal.Given
{
    internal class BeginGivenWhen<TStoryParams, TStoryData> : IBeginGivenWhen<TStoryData> where TStoryData : class, new()
    {
        private readonly ScenarioContext _context;
        private readonly Func<TStoryParams> _createRequest;
        private readonly Func<ScenarioContext, TStoryParams, TStoryData> _execute;

        internal BeginGivenWhen(ScenarioContext<TStoryData> context, Func<TStoryParams> createRequest,
            Func<ScenarioContext, TStoryParams, TStoryData> execute)
        {
            _context = context;
            _createRequest = createRequest;
            _execute = execute;
        }

        public TNextStep Then<TNextStep>([CallerMemberName] string memberName = "")
            where TNextStep : Chapter<TStoryData>, new()
        {
            var request = _createRequest();
            _context.AddPipelineStep(memberName, input => _execute(_context, request));

            var nextContext = new ScenarioContext<TStoryData>(_context);
            var nextStep = new TNextStep {Context = nextContext};

            return nextStep;
        }
    }
}