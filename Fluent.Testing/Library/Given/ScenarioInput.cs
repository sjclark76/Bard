using System;

namespace Fluent.Testing.Library.Given
{
    public class ScenarioInput<TInput> where TInput : class
    {
        public ScenarioContext? Context { get; set; }

        public void UseResult(Action<TInput> useResult)
        {
            var input = Context.ExecutePipeline() as TInput;
            useResult(input);
        }
    }
}