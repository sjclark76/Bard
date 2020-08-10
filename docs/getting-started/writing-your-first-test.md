# Writing Your First Test

This section will describe how to configure Bard in order to write your first API test. This example demonstrates the simplest way to use Bard and does not use the more advanced StoryBook feature which we will cover in more detail in this section of the [documentation](../scenario/given/).

## Configure The Scenario

So before we can start we need to configure our Scenario. To do this we are going to need provide two things.

* A `System.Net.Http.HttpClient` in order to call our API
* A function to tell our Scenario how to log. This is optional but important because Bard provides detailed logs to the test output windows to help with writing and debugging test failures. If not supplied Bard will ouptut all log messages to the Console window which may not appear in your test ouput.

```csharp
var scenario = ScenarioConfiguration
                .Configure(options =>
                {
                     options.Client = httpClient;
                     options.LogMessage = output.WriteLine;
                });
```

{% hint style="info" %}
 The GitHub repository has some example tests you can see a working example [here](https://github.com/sjclark76/bard/blob/515c7eb9c4490018af6cf4042d0b1fd48c32bd76/Bard/Bard.Tests/BankingTestBase.cs#L14-L32).
{% endhint %}

## Write The First Test

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

```javascript
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
}
```



