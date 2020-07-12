using System;

namespace Fluent.Testing.Library.Given
{
    public abstract class ScenarioBase
    {
        private ScenarioContext? _context;

        protected ScenarioContext Context
        {
            get
            {
                if (_context == null)
                    throw new ApplicationException(
                        $"{nameof(BeginAScenario)} not constructed correctly. {nameof(SetContext)} method should be called");

                return _context;
            }
        }

        public void SetContext(ScenarioContext context)
        {
            _context = context;
        }

        public void AddMessage(string message)
        {
            Context.AddPipelineStep(message);
        }
    }
}