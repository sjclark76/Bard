namespace Fluent.Testing.Library.Given
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