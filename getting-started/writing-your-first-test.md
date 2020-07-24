# Writing Your First Test

This section will describe how to configure our scenario to write a very simple test against our API. This is the simplest way using Bard and does not use some of the more advanced features of using StoryBooks which we will cover in more detail in this section of the [documentation](../scenario/given/) 

## Configure our Scenario

So before we can start we need to configure out Scenario. We are going to need two things

* A **System.Net.Http.HttpClient** to call our API
* A function to tell our Scenario how it can log. This is optional but important because Bard provides detailed logs to the test output windows to help with writing and debugging test failures.

```csharp
var scenario = ScenarioConfiguration
                .Configure(options =>
                {
                    options.UseHttpClient(httpClient);
                    options.Log(output.WriteLine);
                });
```

{% hint style="info" %}
 The GitHub repository has some example tests you can see a working example [here](https://github.com/sjclark76/bard/blob/515c7eb9c4490018af6cf4042d0b1fd48c32bd76/Bard/Bard.Tests/BankingTestBase.cs#L14-L32).
{% endhint %}

## Write Our First Test

We've configured our Scenario now. Assuming our API is running now we can start by calling it.

So lets give it a spin by writing a test that calls our fictitious Banking API and creating a new bank account. We expect our API to return a 201 Created response.

```csharp
[Fact]
public void When_creating_a_bank_account()
{
      Scenario
              .When
              .Post("/api/bankaccount/, new BankAccount
                {
                    CustomerName = "Kilian Jornet"
                });

      Scenario
              .Then
              .Response
              .ShouldBe
              .Created();
}
```

And we're done. If we look at the output from our Test we should see some nice logging that includes the request and the response.

```text
****************************************
*             WHEN                     *
****************************************

REQUEST: POST api/bankaccounts
{
  "id": null,
  "isActive": false,
  "customerName": "Kilian Jornet",
  "balance": 0.0
}

RESPONSE: Http Status Code:  Created (201)
Header::Location http://localhost/api/bankaccounts/1
{
  "id": 1,
  "isActive": false,
  "customerName": "Kilian Jornet",
  "balance": 0.0
}```
```



