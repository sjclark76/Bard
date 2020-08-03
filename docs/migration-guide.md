# Migration Guide

## Version 1 to 2

### \[CS0246\] The type or namespace name 'IScenarioContext' could not be found

Replace all references of IScenarioContext with ScenarioContext

### The type arguments for method 'IChapterGiven&lt;..... cannot be inferred 

Example this code:

```csharp
public static readonly Func<ScenarioContext, StoryInput, StoryParameters, StoryOuput> AccountTransactionCreated =
    (context, input, parameters) =>
    {        
        StoryOutput output = // Do Some work here
        return ouput;
    };
```

Should change to:

```csharp
public static readonly Func<ScenarioContext<StoryInput>, StoryParameters, StoryOuput> AccountTransactionCreated =
    (context, parameters) =>
    {       
        // Story Input is now accessed through the Scenario Context
        var storyInput = context.StoryInput; 
        // Do work here
        StoryOutput output = // Do Some work here
        return ouput;
    };
```



