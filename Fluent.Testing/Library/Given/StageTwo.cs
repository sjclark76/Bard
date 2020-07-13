using System;

namespace Fluent.Testing.Library.Given
{
    public interface IStageTwo<TOutput> where TOutput : class, new()
    {
        TNextStep ContinueTo<TNextStep>() where TNextStep : ScenarioStep<TOutput>, new();
    }

    public class StageTwo<TInput, TRequest, TOutput> : IStageTwo<TOutput> where TRequest : new() where TOutput : class, new() where TInput : new()
    {
        public Func<ScenarioContext, TInput, TRequest, TOutput> Execute { get; }

        public StageTwo(StageOne<TInput, TRequest> stageOne, Func<ScenarioContext, TInput, TRequest, TOutput> execute)
        {
            Context = stageOne.Context;
            MemberName = stageOne.MemberName;
            ModifyRequest = stageOne.ModifyRequest;
            Execute = execute;
        }

        private Action<TRequest>? ModifyRequest { get; }

        private string MemberName { get; }

        private ScenarioContext Context { get; }

        public TNextStep ContinueTo<TNextStep>() where TNextStep : ScenarioStep<TOutput>, new()
        {
            var request = new TRequest();

            ModifyRequest?.Invoke(request);

            Context.AddPipelineStep(MemberName, input => input == null
                ? Execute(Context, new TInput(), request)
                : Execute(Context, (TInput) input, request));

            var nextStep = new TNextStep {Context = Context};

            return nextStep;
        }
    }
}