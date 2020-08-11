# Writing A Test

```csharp
var creditRequest = new CreditRequest {CustomerId = "id0201", Credit = 7000};

When
   .Grpc(client => client.CheckCreditRequest(creditRequest));

Then.Response.ShouldBe.Ok();
```

