﻿using System;
using Bard.Configuration;
using Bard.Infrastructure;
using Bard.Internal;
using Bard.Internal.Exception;
using Microsoft.Extensions.DependencyInjection;

namespace Bard
{
    public class ScenarioContext
    {
        private IServiceProvider? _services;

        internal ScenarioContext(IPipelineBuilder pipelineBuilder, BardHttpClient bardHttpClient, IApi api, LogWriter logWriter,
            IServiceProvider? services, Func<object>? createGrpcClient = null)
        {
            Builder = pipelineBuilder;
            BardHttpClient = bardHttpClient;
            Api = api;
            Writer = logWriter;
            CreateGrpcClient = createGrpcClient;

            if (services != null)
                Services = services.CreateScope().ServiceProvider;
        }

        internal IPipelineBuilder Builder { get; }
        
        internal BardHttpClient BardHttpClient { get; }

        internal Func<object>? CreateGrpcClient { get; set; }
        
        public IServiceProvider? Services
        {
            get
            {
                if (_services == null)
                    throw new BardConfigurationException(
                        $"Error Accessing {nameof(ScenarioContext)} {nameof(Services)} property. It has not been set in {nameof(ScenarioConfiguration)} {nameof(ScenarioConfiguration.Configure)} method.");

                return _services;
            }
            set => _services = value;
        }

        public IApi Api { get; }
        public LogWriter Writer { get; }

        internal object? ExecutePipeline()
        {
            return Builder.Execute();
        }

        internal void AddPipelineStep(string stepName, Func<object?, object?> func)
        {
            Builder.AddStep(stepName, func);
        }
    }

    public class ScenarioContext<TStoryInput> : ScenarioContext where TStoryInput : class, new()
    {
        private TStoryInput? _storyInput;

        internal ScenarioContext(ScenarioContext context) : base(context.Builder, context.BardHttpClient, context.Api, context.Writer,
            context.Services, context.CreateGrpcClient)
        {
        }

        public TStoryInput StoryInput
        {
            get
            {
                if (_storyInput == null)
                    throw new BardException($"{nameof(StoryInput)} has not been set.");

                return _storyInput;
            }
        }

        internal void SetStoryInput(TStoryInput? input)
        {
            _storyInput = input;
        }
    }
}