namespace Fluent.Testing.Library
{
    public abstract class ScenarioBase
    {
        public ScenarioContext? Context { get; set; }

        public void AddMessage(string message)
        {
            Context?.AddPipelineStep(message);
        }
    }
}