using System;
using System.Runtime.CompilerServices;

namespace Fluent.Testing.Library.Given
{
    public interface IStageTwo<TOutput> where TOutput : class, new()
    {
        TNextStep GoToNextStep<TNextStep>([CallerMemberName] string memberName = "") where TNextStep : ScenarioStep<TOutput>, new();
    }

    public class StageTwo<TInput, TRequest, TOutput> : IStageTwo<TOutput> where TRequest : new() where TOutput : class, new() where TInput : new()
    {
        public Func<ScenarioContext, TInput, TRequest, TOutput> Execute { get; }

        public StageTwo(StageOne<TInput, TRequest> stageOne, Func<ScenarioContext, TInput, TRequest, TOutput> execute)
        {
            Context = stageOne.Context;
            ModifyRequest = stageOne.ModifyRequest;
            Execute = execute;
        }

        private Action<TRequest>? ModifyRequest { get; }

        private ScenarioContext Context { get; }

        public TNextStep GoToNextStep<TNextStep>([CallerMemberName] string memberName = "") where TNextStep : ScenarioStep<TOutput>, new()
        {
            var request = new TRequest();

            ModifyRequest?.Invoke(request);

            Context.AddPipelineStep(memberName, input => input == null
                ? Execute(Context, new TInput(), request)
                : Execute(Context, (TInput) input, request));

            var nextStep = new TNextStep {Context = Context};

            return nextStep;
        }
    }
}