# StoryData

StoryData is a plain old C\# object \(POCO\) of your choice. An instance of this class will be instantiated at the beginning of your test arrangement and is accessible in every story. This means you can enrich your StoryData as your stories progress.

{% hint style="warning" %}
You can only have one instance of StoryData per configured scenario.
{% endhint %}

![Story Data flow](../../.gitbook/assets/story-data-flow.svg)

The story data can be accessed from within a story from the scenario context. 

The example below demonstrates accessing the story data to get the correct id to use in the URL and updating the story data with the result of the API call.

```csharp
 public DepositMade Deposit_has_been_made(Deposit depositRequest)
 {
    return  
           When(context => 
           {
               var response = context.Api.Post($"api/bankaccounts/{context.StoryData?.BankAccountId}/deposits",
                    depositRequest);
                    
               response.ShouldBe.Ok();
               
               context.StoryData.DepositAmount = depositRequest.Amount;
           })
           .ProceedToChapter<DepositMade>();
 }
```

