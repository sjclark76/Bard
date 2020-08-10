# Migration Guide

## Version 2 to 3

### does not contain a definition for 'Then'

```csharp
// BEFORE
.Then<TransferInstructionCreatedChapter>();

// NEW
.ProceedToChapter<TransferInstructionCreatedChapter>();
```

### does not contain a definition for 'That'

```csharp
// BEFORE
Scenario.Given.That
    .An_investor_has_been_created();

// AFTER
Scenario.Given
    .An_investor_has_been_created();
```



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

### Cannot resolve UseResult

```csharp
 .UseResult(clientRegistered => newClient = clientRegistered );
```

refactored to:

```csharp
.GetResult(out NewClientRegistered? newClient);
```

