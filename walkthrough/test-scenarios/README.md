# Test Scenarios

## Create Your StoryBook

The first thing that we need to do is to configure our Scenario.

Before we can do that we will need to create the entry point for our Scenario. In Bard we call that concept a [StoryBook](../../scenario/given/storybook.md).

So add a new Class file in your test solution, we will call it _`BankingStory`_and make sure you inherit from the the `Bard.StoryBook` class.

```csharp
public class BankingStory : StoryBook
{
    // Add our chapters here later..
}
```

We'll leave the class empty for now but we'll use it for the next test scenario.

```csharp
var scenario = ScenarioConfiguration
                .Configure<BankingStory>(options =>
                {
                    options.UseHttpClient(httpClient);
                    options.Log(output.WriteLine);
                });
```



