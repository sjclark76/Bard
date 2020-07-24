# Creating a bank account

Lets start nice and simple and start by testing our [Create Bank Account](../../sample-banking-api/creating-a-bank-account.md) API endpoint.

### Testing for a created bank account \(201\)

So looking at our specification our POST /api/bankaccounts endpoint returns a 201 for successfully created 

```csharp
    [Fact]
    public void When_creating_a_bank_account()
    {
        When
            .Post(ApiBankaccounts, new BankAccount
            {
                CustomerName = "Ranulph Fiennes"
            });

        Then
            .Response
            .ShouldBe
            .Created();
    }
```

If we run the test and check the output we should see something like this.

```csharp
****************************************
*             WHEN                     *
****************************************

REQUEST: POST api/bankaccounts
{
  "id": null,
  "isActive": false,
  "customerName": "Ranulph Fiennes",
  "balance": 0.0
}

RESPONSE: Http Status Code:  Created (201)
Header::Location http://localhost/api/bankaccounts/1
{
  "id": 1,
  "isActive": false,
  "customerName": "Ranulph Fiennes",
  "balance": 0.0
}
```

### 

