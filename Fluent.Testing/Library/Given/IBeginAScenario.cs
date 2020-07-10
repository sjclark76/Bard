namespace Fluent.Testing.Library.Given
{
    public abstract class BeginAScenario
    {
        protected BeginAScenario()
        {
            PipelineBuilder = new PipelineBuilder();
        }

        public PipelineBuilder PipelineBuilder { get; set; }
    }
  
}