using System;
using Bard.Infrastructure;

namespace Bard
{
    public interface IScenarioContext
    {
        IServiceProvider? Services { get; set; }
        IApi Api { get; }
        LogWriter Writer { get; }
    }

    public interface IScenarioContext<TStoryInput> where TStoryInput : class, new()
    {
        TStoryInput? StoryInput { get; set; }
    }
}