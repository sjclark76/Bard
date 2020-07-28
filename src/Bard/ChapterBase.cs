using System;
using Bard.Internal;

namespace Bard
{
    public abstract class ChapterBase
    {
        internal ScenarioContext? Context { get; set; }

        public void AddMessage(string message)
        {
            if (Context == null)
                throw new ApplicationException($"{nameof(Context)} has not been set.");

            Context.AddPipelineStep(message);
        }
    }
}