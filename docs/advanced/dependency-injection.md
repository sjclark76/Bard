# Dependency Injection

In most of the examples in our Stories we have called our API directly to perform the test arrangement. Sometimes this isn't good enough, we may need to simulate an incoming message on a Message Queue or want to insert some data directly into our API Database.

If this is the case we can tell Bard how to resolve services when we configure the Scenario and access them within the Story through the ScenarioContext. This is particularly easy if you are testing a .NET API.

## Dependency Injection Configuration

```csharp
var hostBuilder = new HostBuilder()
    .ConfigureWebHost(builder =>
        builder
            .UseStartup<Startup>()
            .UseTestServer()
            .UseEnvironment("development"));

var host = hostBuilder.Start();

var httpClient = host.GetTestClient();

Scenario = ScenarioConfiguration
    .Configure(options =>
    {
        options.Client = httpClient;
        options.LogMessage = output.WriteLine;
        options.Services = host.Services;
    });
```

## Using ServiceProvider In A Story

```csharp
public BankAccountHasBeenCreated BankAccount_has_been_created()
{
    return When(context =>
        {
            var bankAccountMessage = new BankAccount
            {
                CustomerName = "Ranulph Fiennes"
            };

            var serviceBus = context.Services.GetService<IServiceBus>()
            
            serviceBus.AddMessage(message);
            
        })
        .ProceedToChapter<BankAccountHasBeenCreated>();
}
```

