# Writing A Test

```aspnet
var creditRequest = new CreditRequest {CustomerId = "id0201", Credit = 7000};

scenario.When.Grpc(client => client.CheckCreditRequest(creditRequest));

            scenario.Then.Response.ShouldBe.Ok();
```

