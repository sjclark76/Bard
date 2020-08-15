# When

Execute your tests against your API by calling When on your scenario.

```csharp
When
    .Post("api/bankaccounts/1234/withdrawals", new Deposit {Amount = 100});
```

This is essentially a wrapper over the standard .NET HttpClient but that adds additional logging.



