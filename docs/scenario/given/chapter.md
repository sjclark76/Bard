# Chapter

A `Chapter` is used to navigate through our `Scenario`. It contains Stories. It allows us to guide the test author what they can do next.

The important thing to remember is that the `Chapter` can receive the output of the previous `Story` as an input via the `StoryData`. We do that by inheriting from the base Class `Chapter` and specify our StoryData class as the Generic Type. Our class should look like this.

```csharp
public class BankAccountHasBeenCreated : Chapter<MyStoryData>
{
}
```

Now we have our Chapter we can add our Stories. 

